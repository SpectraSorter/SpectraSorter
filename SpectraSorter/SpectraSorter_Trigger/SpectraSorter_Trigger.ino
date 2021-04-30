/*
  Receives a trigger from a PC and reacts.
*/

// Macro for fast setting of given bit (pin)
#define _BV(bit) (1 << (bit))

// Commands
#define RESET_COUNTER   0
#define START           1
#define TRIGGER         2
#define QUERY_COUNTER   3
#define STOP            4
#define SET_PIN         5
#define QUERY_PIN       6
#define SET_DELAY       7
#define QUERY_DELAY     8

// Constant
const int PACKET_LENGTH = 9;

// Keep a counter of all the times the trigger command
// was received and executed.
unsigned long TRIGGER_COUNTER = 0L;

// Initialize the PIN to be LED_BUILTIN 
unsigned long PIN = LED_BUILTIN;

// Delay in microseconds between triggering and turn-off
unsigned long TURN_OFF_DELAY = 50L;

// Keep track of sevetal key times in micros
unsigned long EXPERIMENT_START_TIME_MICROS = 0L;
unsigned long CURRENT_TIME_MICROS = 0L;
unsigned long LAST_TRIGGER_TIME_MICROS = 0L;

// Keep track whether the PIN is on HIGH or LOW
boolean PIN_IS_ON = false;

// Expected packet envelope bytes
const byte E_H0 = 255;        // Header
const byte E_H1 = 254;        // Header
const byte E_H2 = 253;        // Header
const byte E_E  = 255;        // Footer

// Obtained bytes
byte H0 = 0;
byte H1 = 0;
byte H2 = 0;
byte C  = 0;
byte P1 = 0;
byte P2 = 0;
byte P3 = 0;
byte P4 = 0;
byte E  = 0;

// Flag that indicates wheter we are processing 
// the 5 (command + parameter) bytes between 
// packet header and footer.
bool reading_command = false;

// Number of bytes found in current packet
int n_bytes_found = 0;

// Board setup
void setup() {

  // Start the serial connection
  Serial.begin(115200);

  while (!Serial)
  {
    ; // wait for serial port to connect.
  }
  
  // initialize digital pin LED_BUILTIN as an output.
  pinMode(PIN, OUTPUT);

  // Initialize TRIGGER_COUNTER
  TRIGGER_COUNTER = 0L;
}

// Board loop
void loop() {

  // Prevent continuously calling loop() from main()
  while (true)
  {
    // Current value of micros
    CURRENT_TIME_MICROS = micros() - EXPERIMENT_START_TIME_MICROS;

    // Elapsed time since last trigger (correct for possible wrap-around)
    long elapsed_time = CURRENT_TIME_MICROS - LAST_TRIGGER_TIME_MICROS;
    if (elapsed_time < 0)
    {
      elapsed_time = 4294967295 - elapsed_time;  
    }
    if (PIN_IS_ON == true && elapsed_time > TURN_OFF_DELAY)
    {
      turnOff();
    }

    if (Serial.available() > 0)
    {
      // Read next byte
      byte b = Serial.read();
  
      // Nothing found yet
      if (reading_command == false && H0 == 0 && H1 == 0 && H2 == 0)
      {
        if (b == E_H0)
        {
          // Found H0
          H0 = b;
          n_bytes_found = 1;
          reading_command = false;
        }
        else
        {
          reset_all();
        }
      }

      // First header byte already found
      else if (reading_command == false && H0 == E_H0 && H1 == 0 && H2 == 0)
      {
        if (b == E_H1)
        {
          // Found H1
          H1 = b;
          n_bytes_found = 2;
          reading_command = false;
        }
        else
        {
          reset_all();
        }
      }

      // Second header byte already found
      else if (reading_command == false && H0 == E_H0 && H1 == E_H1 && H2 == 0)
      {
        if (b == E_H2)
        {
          // Found H2
          H2 = b;
          n_bytes_found = 3;
          reading_command = true;
        }
        else
        {
          reset_all();
        }
      }

      // Last header byte already found: read the next 6 bytes (5 of data and 1 of footer)
      else if (reading_command == true && H0 == E_H0 && H1 == E_H1 && H2 == E_H2 && n_bytes_found >= 3 && n_bytes_found <= PACKET_LENGTH)
      {
        if (n_bytes_found == 3)
        {
          // Found C
          C = b;
        }
        else if (n_bytes_found == 4)
        {
          // Found P1
          P1 = b;
        }
        else if (n_bytes_found == 5)
        {
          // Found P2
          P2 = b;
        }
        else if (n_bytes_found == 6)
        {
          // Found P3
          P3 = b;
        }
        else if (n_bytes_found == 7)
        {
          // Found P4
          P4 = b;
        }
        else if (n_bytes_found == 8)
        {
          E = b;
          if (E == E_E) {
            // Found E
            processCommand(C, P1, P2, P3, P4);
            reset_all();
          }
          else {
            reset_all();
          }
        }

        n_bytes_found++;
      }
      else
      {
        reset_all();
      }
    }
  }
}

void processCommand(byte C, byte P1, byte P2, byte P3, byte P4)
{
  // TRIGGER does not have parameters and -- for performance 
  // reasons -- does not return anything.
  if (C == TRIGGER)
  {
    // Trigger digital output. @TODO: For performance
    // reasons, directly set the bit to true instead 
    // of calling digitalWrite().

    // Set experiment start also for individual triggers
    if (EXPERIMENT_START_TIME_MICROS == 0) {
      EXPERIMENT_START_TIME_MICROS = micros();
    }

    // Set the PIN to high and set timers and status
    turnOn();

    // Increase counter
    TRIGGER_COUNTER++;

    // Return here
    return;
  }

  // Start a new acquisition
  if (C == START)
  {
    // Set the time of the beginning of the experiment
    EXPERIMENT_START_TIME_MICROS = micros();

    // Reset trigger counter
    TRIGGER_COUNTER = 0L;

    // Return here
    return;
  }

  // QUERY_COUNTER does not have parameters. It returns the 
  // current internal trigger counter (TRIGGER_COUNTER).
  if (C == QUERY_COUNTER)
  {
    // Report internal trigger counter (TRIGGER_COUNTER)
    byte response[5];
    response[0] = C;
    response[1] = TRIGGER_COUNTER;
    response[2] = TRIGGER_COUNTER >> 8;
    response[3] = TRIGGER_COUNTER >> 16;
    response[4] = TRIGGER_COUNTER >> 24;
    Serial.write((byte*)&response, 5);

    // Return here
    return;
  }

  // STOP does not have parameters and does not return anything.
  if (C == STOP)
  {
    // Disable digital output. For performance reasons,
    // directly set the bit to true instead of calling 
    // digitalWrite(PIN, LOW);

    // Turn off
    turnOff();

    // Return here
    return;
  }

  // SET_PIN has a parameter and returns the parameter for confirmation.
  if (C == SET_PIN)
  {
    // Build the number from the bytes
    PIN = 0;
    PIN |= (unsigned long) P1;
    PIN |= ((unsigned long) P2) << 8;
    PIN |= ((unsigned long) P3) << 16;
    PIN |= ((unsigned long) P4) << 24;

    // Send back the new value (confirmation)
    byte response[5];
    response[0] = C;
    response[1] = PIN;
    response[2] = PIN >> 8;
    response[3] = PIN >> 16;
    response[4] = PIN >> 24;
    Serial.write((byte*)&response, 5);

    // Return here
    return;
  }

  // QUERY_PIN does not have parameters and returns the PIN number.
  if (C == QUERY_PIN)
  {
    // Report internal trigger counter (TRIGGER_COUNTER)
    byte response[5];
    response[0] = C;
    response[1] = PIN;
    response[2] = PIN >> 8;
    response[3] = PIN >> 16;
    response[4] = PIN >> 24;
    Serial.write((byte*)&response, 5);

    // Return here
    return;
  }

  // SET_DELAY has a parameter and returns the new value of delay.
  if (C == SET_DELAY)
  {
    // Build the number from the bytes
    TURN_OFF_DELAY = 0;
    TURN_OFF_DELAY |= (unsigned long) P1;
    TURN_OFF_DELAY |= ((unsigned long) P2) << 8;
    TURN_OFF_DELAY |= ((unsigned long) P3) << 16;
    TURN_OFF_DELAY |= ((unsigned long) P4) << 24;

    // Report trigger delay (TURN_OFF_DELAY)
    byte response[5];
    response[0] = C;
    response[1] = (byte) TURN_OFF_DELAY;
    response[2] = (byte) (TURN_OFF_DELAY >> 8);
    response[3] = (byte) (TURN_OFF_DELAY >> 16);
    response[4] = (byte) (TURN_OFF_DELAY >> 24);
    Serial.write((byte*)&response, 5);

    // Return here
    return;
  }

  // QUERY_DELAY does not have parameters and returns the trigger delay.
  if (C == QUERY_DELAY)
  {
    // Report trigger delay
    byte response[5];
    response[0] = C;
    response[1] = (byte) TURN_OFF_DELAY;
    response[2] = (byte) (TURN_OFF_DELAY >> 8);
    response[3] = (byte) (TURN_OFF_DELAY >> 16);
    response[4] = (byte) (TURN_OFF_DELAY >> 24);
    Serial.write((byte*)&response, 5);

    // Return here
    return;
  }

  // RESET_COUNTER does not have parameters.
  // It returns the new trigger counter value = 0.
  if (C == RESET_COUNTER)
  {
    // Reset internal trigger counter (TRIGGER_COUNTER)
    TRIGGER_COUNTER = 0;

    // Report internal trigger counter (TRIGGER_COUNTER)
    byte response[5];
    response[0] = C;
    response[1] = TRIGGER_COUNTER;
    response[2] = TRIGGER_COUNTER >> 8;
    response[3] = TRIGGER_COUNTER >> 16;
    response[4] = TRIGGER_COUNTER >> 24;
    Serial.write((byte*)&response, 5);

    // Return here
    return;
  }
}

// Turn on
inline void turnOn()
{
    // Store the time of the trigger
    LAST_TRIGGER_TIME_MICROS = micros() - EXPERIMENT_START_TIME_MICROS;

    // If the PIN is already on, we don't need
    // to turn it on again
    if (PIN_IS_ON == false)
    {
      // Set the PIN to HIGH
      digitalWrite(PIN, HIGH);
    }

    // Store the status of the PIN
    PIN_IS_ON = true;
}


// Turn off
inline void turnOff()
{
    // Set the PIN to HIGH
    digitalWrite(PIN, LOW);

    // Store the status of the PIN
    PIN_IS_ON = false;
}

// Resetting the process of a command packet
void reset_all()
{
  n_bytes_found = 0;
  reading_command = false;
  H0 = 0;
  H1 = 0;
  H2 = 0;
  C = 0;
  P1 = 0;
  P2 = 0;
  P3 = 0;
  P4 = 0;
  E = 0;
}
