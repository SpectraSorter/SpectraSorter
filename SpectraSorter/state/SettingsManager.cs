/*

    Copyright © 2018-2021, ETH Zurich, D-BSSE, Aaron Ponti & Todd Duncombe
    All rights reserved. This program and the accompanying materials
    are made available under the terms of the Apache-2.0 license
    which accompanies this distribution, and is available at
    https://www.apache.org/licenses/LICENSE-2.0

    SpectraSorter is based on FXStreamer by Oliver Lischtschenko (Ocean Optics):
    Lischtschenko, O.; private communication on OBP protocol, 2018. 
    The original code is added to the repository.

*/

using spectra.processing;
using System.Collections.Generic;

namespace spectra.state
{
    /// <summary>
    /// Gets and sets (and immediately saves) the Application Settings.
    /// </summary>
    public static partial class SettingsManager
    {

        /// <summary>
        /// Return the complete collection of Settings.
        /// </summary>
        public static System.Configuration.SettingsPropertyValueCollection AllSettings
        {
            get => Properties.Settings.Default.PropertyValues;
        }

        /// <summary>
        /// Acquisition delay.
        /// </summary>
        public static uint AcquisitionDelay
        {
            get => (uint)Properties.Settings.Default[nameof(AcquisitionDelay)];

            set
            {
                Properties.Settings.Default[nameof(AcquisitionDelay)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Index ofthe acquisition duration option.
        /// </summary>
        public static int AcquisitionDurationIndex
        {
            get => (int)Properties.Settings.Default[nameof(AcquisitionDurationIndex)];

            set
            {
                Properties.Settings.Default[nameof(AcquisitionDurationIndex)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Delay before the Arduino is triggered.
        /// </summary>
        public static uint ArduinoTriggerDelay
        {
            get => (uint)Properties.Settings.Default[nameof(ArduinoTriggerDelay)];

            set
            {
                Properties.Settings.Default[nameof(ArduinoTriggerDelay)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Acquisition duration.
        /// </summary>
        public static double AcquisitionDuration
        {
            get => (double)Properties.Settings.Default[nameof(AcquisitionDuration)];

            set
            {
                Properties.Settings.Default[nameof(AcquisitionDuration)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Back-to-back spectrum scans per trigger.
        /// </summary>
        public static uint BackToBackPerTrigger
        {
            get => (uint)Properties.Settings.Default[nameof(BackToBackPerTrigger)];

            set
            {
                Properties.Settings.Default[nameof(BackToBackPerTrigger)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Buffer enabled.
        /// </summary>
        public static byte BufferEnabled
        {
            get => (byte)Properties.Settings.Default[nameof(BufferEnabled)];

            set
            {
                Properties.Settings.Default[nameof(BufferEnabled)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Short (experiment) condition info string.
        /// </summary>
        public static string ConditionsInfo
        {
            get => (string)Properties.Settings.Default[nameof(ConditionsInfo)];

            set
            {
                Properties.Settings.Default[nameof(ConditionsInfo)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Short experiment info string.
        /// </summary>
        public static string ExperimentInfo
        {
            get => (string)Properties.Settings.Default[nameof(ExperimentInfo)];

            set
            {
                Properties.Settings.Default[nameof(ExperimentInfo)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Integration time for spectrum acquisition.
        /// </summary>
        public static uint IntegrationTime
        {
            get => (uint)Properties.Settings.Default[nameof(IntegrationTime)];

            set
            {
                Properties.Settings.Default[nameof(IntegrationTime)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Lamp enable.
        /// </summary>
        public static byte LampEnable
        {
            get => (byte)Properties.Settings.Default[nameof(LampEnable)];

            set
            {
                Properties.Settings.Default[nameof(LampEnable)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Number of spectra per request.
        /// </summary>
        public static uint NumSpectraPerRequest
        {
            get => (uint)Properties.Settings.Default[nameof(NumSpectraPerRequest)];

            set
            {
                Properties.Settings.Default[nameof(NumSpectraPerRequest)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Current plot type.
        /// </summary>
        public static Options.PlotType CurrentPlotType
        {
            get => (Options.PlotType)Properties.Settings.Default[nameof(CurrentPlotType)];

            set
            {
                // Remember last value
                Properties.Settings.Default[nameof(LastPlotType)] = Properties.Settings.Default[nameof(CurrentPlotType)];

                Properties.Settings.Default[nameof(CurrentPlotType)] = (int)value;

                // Raise the event
                SettingsManager.OnChangePlotType(null,
                    new PlotTypeEventArgs
                    {
                        Type = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Current plot type.
        /// </summary>
        public static bool PlotThresholds
        {
            get => (bool)Properties.Settings.Default[nameof(PlotThresholds)];

            set
            {
                Properties.Settings.Default[nameof(PlotThresholds)] = (bool)value;

                // Raise the event
                SettingsManager.OnTogglePlotThresholds(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Current plot type.
        /// </summary>
        public static bool PlotTriggerPoints
        {
            get => (bool)Properties.Settings.Default[nameof(PlotTriggerPoints)];

            set
            {
                Properties.Settings.Default[nameof(PlotTriggerPoints)] = (bool)value;

                // Raise the event
                SettingsManager.OnTogglePlotTriggerPoints(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        public static Options.ResultSpectrumType ResultSpectrumType
        {
            get => (Options.ResultSpectrumType)Properties.Settings.Default[nameof(ResultSpectrumType)];

            set
            {
                Properties.Settings.Default[nameof(ResultSpectrumType)] = (int)value;

                // Raise the event
                SettingsManager.OnChangeResultSpectrumType(null,
                    new ResultSpectrumTypeEventArgs
                    {
                        Type = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }

            }

        }

        /// <summary>
        /// Current plot type.
        /// </summary>

        public static Options.ReferenceType ReferenceType
        {
            get => (Options.ReferenceType)Properties.Settings.Default[nameof(ReferenceType)];

            set
            {
                Properties.Settings.Default[nameof(ReferenceType)] = (int)value;

                // Raise the event
                SettingsManager.OnChangeReferenceType(null,
                    new ReferenceTypeEventArgs
                    {
                        Type = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Last plot type.
        /// </summary>
        public static Options.PlotType LastPlotType
        {
            get => (Options.PlotType)Properties.Settings.Default[nameof(LastPlotType)];

            set
            {
                Properties.Settings.Default[nameof(LastPlotType)] = (int)value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Directory where to save the results.
        /// </summary>
        public static string SaveDirectory
        {
            get => (string)Properties.Settings.Default[nameof(SaveDirectory)];

            set
            {
                Properties.Settings.Default[nameof(SaveDirectory)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Current save file name.
        /// </summary>
        public static string SaveFileName
        {
            get => (string)Properties.Settings.Default[nameof(SaveFileName)];

            set
            {
                Properties.Settings.Default[nameof(SaveFileName)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Index of the first pixel in the spectrum to save.
        /// </summary>
        public static int SaveStartPixel
        {
            get => (int)Properties.Settings.Default[nameof(SaveStartPixel)];

            set
            {
                Properties.Settings.Default[nameof(SaveStartPixel)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Index of the last pixel in the spectrum to save.
        /// </summary>
        public static int SaveEndPixel
        {
            get => (int)Properties.Settings.Default[nameof(SaveEndPixel)];

            set
            {
                Properties.Settings.Default[nameof(SaveEndPixel)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Step (pixel) when saving a spectrum.
        /// </summary>
        public static int SaveStepPixel
        {
            get => (int)Properties.Settings.Default[nameof(SaveStepPixel)];

            set
            {
                Properties.Settings.Default[nameof(SaveStepPixel)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Value of the first wavelength in the spectrum to save.
        /// </summary>
        public static double SaveStartWavelength
        {
            get => (double)Properties.Settings.Default[nameof(SaveStartWavelength)];

            set
            {
                Properties.Settings.Default[nameof(SaveStartWavelength)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Value of the last wavelength in the spectrum to save.
        /// </summary>
        public static Options.RangeUnits SaveRangeUnits
        {
            get => (Options.RangeUnits)Properties.Settings.Default[nameof(SaveRangeUnits)];

            set
            {
                Properties.Settings.Default[nameof(SaveRangeUnits)] = (Options.RangeUnits)value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Value of the last wavelength in the spectrum to save.
        /// </summary>
        public static double SaveEndWavelength
        {
            get => (double)Properties.Settings.Default[nameof(SaveEndWavelength)];

            set
            {
                Properties.Settings.Default[nameof(SaveEndWavelength)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Enable/disable saving a range instead of individual wavelengths.
        /// </summary>
        public static bool SaveWavelengthRange
        {
            get => (bool)Properties.Settings.Default[nameof(SaveWavelengthRange)];

            set
            {
                Properties.Settings.Default[nameof(SaveWavelengthRange)] = value;

                // Raise the event
                SettingsManager.OnToggleSavingWavelengthRange(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Enable/disable saving to file.
        /// </summary>
        public static bool SaveToFile
        {
            get => (bool)Properties.Settings.Default[nameof(SaveToFile)];

            set
            {
                Properties.Settings.Default[nameof(SaveToFile)] = value;

                // Raise the event
                SettingsManager.OnToggleSaving(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }


        /// <summary>
        /// Enable/disable auto-update of file name of acquisition start.
        /// </summary>
        public static bool SaveFileNameAutoUpdate
        {
            get => (bool)Properties.Settings.Default[nameof(SaveFileNameAutoUpdate)];

            set
            {
                Properties.Settings.Default[nameof(SaveFileNameAutoUpdate)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Number of scans to average.
        /// </summary>
        public static uint ScansToAverage
        {
            get => (uint)Properties.Settings.Default[nameof(ScansToAverage)];

            set
            {
                Properties.Settings.Default[nameof(ScansToAverage)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Set the spectrum filter to be an average filter.
        /// </summary>
        public static bool SpectrumFilteringAverage
        {
            get => (bool)Properties.Settings.Default[nameof(SpectrumFilteringAverage)];

            set
            {
                Properties.Settings.Default[nameof(SpectrumFilteringAverage)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Enable/disable spectrum filtering.
        /// </summary>
        public static bool SpectrumFilteringEnabled
        {
            get => (bool)Properties.Settings.Default[nameof(SpectrumFilteringEnabled)];

            set
            {
                Properties.Settings.Default[nameof(SpectrumFilteringEnabled)] = value;

                // Raise the event
                SettingsManager.OnToggleFiltering(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Set the spectrum filter to be a Gaussian filter.
        /// </summary>
        public static bool SpectrumFilteringGaussian
        {
            get => (bool)Properties.Settings.Default[nameof(SpectrumFilteringGaussian)];

            set
            {
                Properties.Settings.Default[nameof(SpectrumFilteringGaussian)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Set the kernel support in pixels for the spectrum filter.
        /// </summary>
        public static int SpectrumFilteringSupport
        {
            get => (int)Properties.Settings.Default[nameof(SpectrumFilteringSupport)];

            set
            {
                Properties.Settings.Default[nameof(SpectrumFilteringSupport)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Enable/disable continuous strobe.
        /// </summary>
        public static bool ContinuousStrobePulseEnabled
        {
            get => (bool)Properties.Settings.Default[nameof(ContinuousStrobePulseEnabled)];

            set
            {
                Properties.Settings.Default[nameof(ContinuousStrobePulseEnabled)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Get/set continuous strobe period.
        /// </summary>
        public static uint ContinuousStrobePulsePeriod
        {
            get => (uint)Properties.Settings.Default[nameof(ContinuousStrobePulsePeriod)];

            set
            {
                Properties.Settings.Default[nameof(ContinuousStrobePulsePeriod)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Get/set continuous strobe width.
        /// </summary>
        public static uint ContinuousStrobePulseWidth
        {
            get => (uint)Properties.Settings.Default[nameof(ContinuousStrobePulseWidth)];

            set
            {
                Properties.Settings.Default[nameof(ContinuousStrobePulseWidth)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Enable/disable single strobe.
        /// </summary>
        public static bool SingleStrobePulseEnabled
        {
            get => (bool)Properties.Settings.Default[nameof(SingleStrobePulseEnabled)];

            set
            {
                Properties.Settings.Default[nameof(SingleStrobePulseEnabled)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Get/set single strobe period.
        /// </summary>
        public static uint SingleStrobePulseDelay
        {
            get => (uint)Properties.Settings.Default[nameof(SingleStrobePulseDelay)];

            set
            {
                Properties.Settings.Default[nameof(SingleStrobePulseDelay)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Get/set single strobe width.
        /// </summary>
        public static uint SingleStrobePulseWidth
        {
            get => (uint)Properties.Settings.Default[nameof(SingleStrobePulseWidth)];

            set
            {
                Properties.Settings.Default[nameof(SingleStrobePulseWidth)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Enable/disable spectrum thresholding.
        /// </summary>
        public static bool SpectrumThresholdingEnabled
        {
            get => (bool)Properties.Settings.Default[nameof(SpectrumThresholdingEnabled)];

            set
            {
                Properties.Settings.Default[nameof(SpectrumThresholdingEnabled)] = value;

                // Raise the event
                SettingsManager.OnToggleThresholding(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Set whether all wavelengths must above or below threshold or
        /// whether only one is enough.
        /// </summary>
        public static bool SpectrumThresholdingAllSatisfiedEnabled
        {
            get => (bool)Properties.Settings.Default[nameof(SpectrumThresholdingAllSatisfiedEnabled)];

            set
            {
                Properties.Settings.Default[nameof(SpectrumThresholdingAllSatisfiedEnabled)] = value;

                // Raise the event
                SettingsManager.OnToggleThresholdingAllSatisfied(null,
                    new SingleBooleanEventArgs
                    {
                        Enabled = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Trigger mode.
        /// </summary>
        public static byte TriggerMode
        {
            get => (byte)Properties.Settings.Default[nameof(TriggerMode)];

            set
            {
                Properties.Settings.Default[nameof(TriggerMode)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Persist properties to disk on modification.
        /// </summary>
        public static bool SaveOnWrite
        {
            get => (bool)Properties.Settings.Default[nameof(SaveOnWrite)];

            set
            {
                Properties.Settings.Default[nameof(SaveOnWrite)] = value;

                // This is the only setting that is ALWAYS persisted on change.
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// Enable single software trigger.
        /// </summary>
        public static bool SingleSoftwareTriggerEnabled
        {
            get => (bool)Properties.Settings.Default[nameof(SingleSoftwareTriggerEnabled)];

            set
            {
                Properties.Settings.Default[nameof(SingleSoftwareTriggerEnabled)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Index ofthe acquisition duration option.
        /// </summary>
        public static uint NumberOfTriggerPoints
        {
            get => (uint)Properties.Settings.Default[nameof(NumberOfTriggerPoints)];

            set
            {
                Properties.Settings.Default[nameof(NumberOfTriggerPoints)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Index ofthe acquisition duration option.
        /// </summary>
        public static int NumberOfSpectraForDynamicAccumulation
        {
            get => (int)Properties.Settings.Default[nameof(NumberOfSpectraForDynamicAccumulation)];

            set
            {
                Properties.Settings.Default[nameof(NumberOfSpectraForDynamicAccumulation)] = value;

                // Raise the event
                SettingsManager.OnChangeNumberOfSpectraForDynamicAccumulation(null,
                    new NumberOfSpectraForDynamicAccumulationEventArgs
                    {
                        Value = value
                    });

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Number of seconds to accumulate spectra for static reference.
        /// </summary>
        public static uint StaticReferenceAccumulateTime
        {
            get => (uint)Properties.Settings.Default[nameof(StaticReferenceAccumulateTime)];

            set
            {
                Properties.Settings.Default[nameof(StaticReferenceAccumulateTime)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Interval (number of accumulated spectra) between regeneration of dynamic reference.
        /// </summary>
        public static uint IntervalForDynamicAccumulation
        {
            get => (uint)Properties.Settings.Default[nameof(IntervalForDynamicAccumulation)];

            set
            {
                Properties.Settings.Default[nameof(IntervalForDynamicAccumulation)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// List of Wavelengths.
        /// </summary>
        public static List<Wavelength> Wavelengths
        {
            get
            {
                if (Properties.Settings.Default[nameof(Wavelengths)] == null)
                {
                    List<Wavelength> tmp = new List<Wavelength>(capacity: 10);
                    Properties.Settings.Default[nameof(Wavelengths)] = tmp;
                    return tmp;
                }
                else
                {
                    return (List<Wavelength>)Properties.Settings.Default[nameof(Wavelengths)];
                }
            }

            set
            {
                Properties.Settings.Default[nameof(Wavelengths)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Number of timepoints to cache for time series plotting.
        /// </summary>
        public static uint NumberOfTimePointsToStore
        {
            get => (uint)Properties.Settings.Default[nameof(NumberOfTimePointsToStore)];

            set
            {
                Properties.Settings.Default[nameof(NumberOfTimePointsToStore)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Number of timepoints to plot for time series plotting.
        /// </summary>
        public static uint NumberOfTimePointsToPlot
        {
            get => (uint)Properties.Settings.Default[nameof(NumberOfTimePointsToPlot)];

            set
            {
                Properties.Settings.Default[nameof(NumberOfTimePointsToPlot)] = value;

                if ((bool)Properties.Settings.Default[nameof(SaveOnWrite)] == true)
                {
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Persist all Settings to disk.
        /// </summary>
        public static void Save()
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Revert to default values
        /// </summary>
        public static void RestoreDefaults()
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Reload();
        }
    }
}