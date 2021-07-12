# SpectraSorter

Software for droplet sorting based on the analysis of high-speed spectrophotometer (OceanFX) measurements. This repository accompanies a manuscript submission currently under review. A link will be included after acceptance.

## Getting Started

The source code comes in the form of a Visual Studio 2019 solution. A full description of software features and operation instructions are included in the user manual. The [most recent release](https://github.com/SpectraSorter/SpectraSorter/releases/latest) can be downloaded and installed.

### Prerequisites

#### Software

* Visual Studio 2019 with .NET Desktop development workload.
* .NET framework 4.8.
* Arduino IDE.

#### Hardware

* Ocean Optics OceanFX spectrophotometer (USB3 or Ethernet connection).
* Arduino MEGA 2560 (USB).

### Building and preparing

* Open `SpectraSorter.sln` in Visual Studio, switch to `Release` and run `Build > Build Solution`.
* Start Arduino IDE, open `Solution Items > SpectraSorter_Trigger.ino` and upload it to Arduino.

### Running

* Make sure both the OceanFX spectrophotometer and Arduino are connected to your PC.
* Run SpectraSorter from Visual Studio.

## Deployment

The `SpectraSorterSetup` project can be used to create a redistributable Windows Installer. Right-click on `SpectraSorterSetup` and select `Build`. Once the build is complete, right-click on the project again and choose `Open Folder in File Explorer`.  The setup files can be used to install SpectraSorter on any machine.

## Contributing

Please submit any issues via the repository.

## Authors

- **Aaron Ponti** - Software development.
- **Todd Duncombe** - Experimental design.

## License

This project is licensed under the Apache 2.0 License - see the [LICENSE-2.0.txt](./LICENSE-2.0.txt) file for details

## Acknowledgments

- SpectraSorter is based on FXStreamer by Oliver Lischtschenko (Ocean Optics): Lischtschenko, O.; private communication on OBP protocol, 2018.
