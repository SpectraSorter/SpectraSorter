/*

    Copyright © 2018-2022, ETH Zurich, D-BSSE, Aaron Ponti & Todd Duncombe
    All rights reserved. This program and the accompanying materials
    are made available under the terms of the Apache-2.0 license
    which accompanies this distribution, and is available at
    https://www.apache.org/licenses/LICENSE-2.0

    SpectraSorter is based on FXStreamer by Oliver Lischtschenko (Ocean Optics):
    Lischtschenko, O.; private communication on OBP protocol, 2018. 
    The original code is added to the repository.

*/

using spectra.processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spectra.state
{
    [Serializable]
    public class SettingsSnapshot
    {
        public uint AcquisitionDelay { get; set; }
        public int AcquisitionDurationIndex { get; set; }
        public uint ArduinoTriggerDelay { get; set; }
        public double AcquisitionDuration { get; set; }
        public uint BackToBackPerTrigger { get; set; }
        public byte BufferEnabled { get; set; }
        public string ConditionsInfo { get; set; }
        public string ExperimentInfo { get; set; }
        public uint IntegrationTime { get; set; }
        public byte LampEnable { get; set; }
        public uint NumSpectraPerRequest { get; set; }
        public Options.PlotType CurrentPlotType { get; set; }
        public bool PlotThresholds { get; set; }
        public bool PlotTriggerPoints { get; set; }
        public Options.ResultSpectrumType ResultSpectrumType { get; set; }
        public Options.ReferenceType ReferenceType { get; set; }
        public Options.PlotType LastPlotType { get; set; }
        public string SaveDirectory { get; set; }
        public string SaveFileName { get; set; }
        public int SaveStartPixel { get; set; }
        public int SaveEndPixel { get; set; }
        public double SaveStartWavelength { get; set; }
        public Options.RangeUnits SaveRangeUnits { get; set; }
        public double SaveEndWavelength { get; set; }
        public bool SaveToFile { get; set; }
        public uint ScansToAverage { get; set; }
        public bool SpectrumFilteringAverage { get; set; }
        public bool SpectrumFilteringEnabled { get; set; }
        public bool SpectrumFilteringGaussian { get; set; }
        public int SpectrumFilteringSupport { get; set; }
        public bool ContinuousStrobePulseEnabled { get; set; }
        public uint ContinuousStrobePulsePeriod { get; set; }
        public uint ContinuousStrobePulseWidth { get; set; }
        public bool SingleStrobePulseEnabled { get; set; }
        public uint SingleStrobePulseDelay { get; set; }
        public uint SingleStrobePulseWidth { get; set; }
        public bool SpectrumThresholdingEnabled { get; set; }
        public bool SpectrumThresholdingAllSatisfiedEnabled { get; set; }
        public byte TriggerMode { get; set; }
        public bool SaveOnWrite { get; set; }
        public bool SingleSoftwareTriggerEnabled { get; set; }
        public int NumberOfSpectraForDynamicAccumulation { get; set; }
        public uint StaticReferenceAccumulateTime { get; set; }
        public uint IntervalForDynamicAccumulation { get; set; }
        public List<Wavelength> Wavelengths { get; set; }
        public uint NumberOfTimePoints { get; set; }
        public int SaveStepPixel { get; set; }
        public bool SaveFileNameAutoUpdate { get; set; }

        /// <summary>
        /// Initialize the writer with current Settings. 
        /// </summary>
        /// 
        /// Re-initialize to store updated settings!
        public SettingsSnapshot()
        {
        }

        /// <summary>
        /// Take a snapshot of current Settings.
        /// </summary>
        public void Take()
        {
            // Take a snapshow of current settings
            this.AcquisitionDelay = SettingsManager.AcquisitionDelay;
            this.AcquisitionDurationIndex = SettingsManager.AcquisitionDurationIndex;
            this.ArduinoTriggerDelay = SettingsManager.ArduinoTriggerDelay;
            this.AcquisitionDuration = SettingsManager.AcquisitionDuration;
            this.BackToBackPerTrigger = SettingsManager.BackToBackPerTrigger;
            this.BufferEnabled = SettingsManager.BufferEnabled;
            this.ConditionsInfo = SettingsManager.ConditionsInfo;
            this.ExperimentInfo = SettingsManager.ExperimentInfo;
            this.IntegrationTime = SettingsManager.IntegrationTime;
            this.LampEnable = SettingsManager.LampEnable;
            this.NumSpectraPerRequest = SettingsManager.NumSpectraPerRequest;
            this.CurrentPlotType = SettingsManager.CurrentPlotType;
            this.PlotThresholds = SettingsManager.PlotThresholds;
            this.PlotTriggerPoints = SettingsManager.PlotTriggerPoints;
            this.ResultSpectrumType = SettingsManager.ResultSpectrumType;
            this.ReferenceType = SettingsManager.ReferenceType;
            this.LastPlotType = SettingsManager.LastPlotType;
            this.SaveDirectory = SettingsManager.SaveDirectory;
            this.SaveFileName = SettingsManager.SaveFileName;
            this.SaveStartPixel = SettingsManager.SaveStartPixel;
            this.SaveEndPixel = SettingsManager.SaveEndPixel;
            this.SaveStartWavelength = SettingsManager.SaveStartWavelength;
            this.SaveRangeUnits = SettingsManager.SaveRangeUnits;
            this.SaveEndWavelength = SettingsManager.SaveEndWavelength;
            this.SaveToFile = SettingsManager.SaveToFile;
            this.ScansToAverage = SettingsManager.ScansToAverage;
            this.SpectrumFilteringAverage = SettingsManager.SpectrumFilteringAverage;
            this.SpectrumFilteringEnabled = SettingsManager.SpectrumFilteringEnabled;
            this.SpectrumFilteringGaussian = SettingsManager.SpectrumFilteringGaussian;
            this.SpectrumFilteringSupport = SettingsManager.SpectrumFilteringSupport;
            this.ContinuousStrobePulseEnabled = SettingsManager.ContinuousStrobePulseEnabled;
            this.ContinuousStrobePulsePeriod = SettingsManager.ContinuousStrobePulsePeriod;
            this.ContinuousStrobePulseWidth = SettingsManager.ContinuousStrobePulseWidth;
            this.SingleStrobePulseEnabled = SettingsManager.SingleStrobePulseEnabled;
            this.SingleStrobePulseDelay = SettingsManager.SingleStrobePulseDelay;
            this.SingleStrobePulseWidth = SettingsManager.SingleStrobePulseWidth;
            this.SpectrumThresholdingEnabled = SettingsManager.SpectrumThresholdingEnabled;
            this.SpectrumThresholdingAllSatisfiedEnabled = SettingsManager.SpectrumThresholdingAllSatisfiedEnabled;
            this.TriggerMode = SettingsManager.TriggerMode;
            this.SaveOnWrite = SettingsManager.SaveOnWrite;
            this.SingleSoftwareTriggerEnabled = SettingsManager.SingleSoftwareTriggerEnabled;
            this.NumberOfSpectraForDynamicAccumulation = SettingsManager.NumberOfSpectraForDynamicAccumulation;
            this.StaticReferenceAccumulateTime = SettingsManager.StaticReferenceAccumulateTime;
            this.IntervalForDynamicAccumulation = SettingsManager.IntervalForDynamicAccumulation;
            this.Wavelengths = SettingsManager.Wavelengths;
            this.NumberOfTimePoints = SettingsManager.NumberOfTimePointsToStore;
            this.SaveStepPixel = SettingsManager.SaveStepPixel;
            this.SaveFileNameAutoUpdate = SettingsManager.SaveFileNameAutoUpdate;
    }

        public void Apply()
        {
            // Take a snapshow of current settings
            SettingsManager.AcquisitionDelay = this.AcquisitionDelay;
            SettingsManager.AcquisitionDurationIndex = this.AcquisitionDurationIndex;
            SettingsManager.ArduinoTriggerDelay = this.ArduinoTriggerDelay;
            SettingsManager.AcquisitionDuration = this.AcquisitionDuration;
            SettingsManager.BackToBackPerTrigger = this.BackToBackPerTrigger;
            SettingsManager.BufferEnabled = this.BufferEnabled;
            SettingsManager.ConditionsInfo = this.ConditionsInfo;
            SettingsManager.ExperimentInfo = this.ExperimentInfo;
            SettingsManager.IntegrationTime = this.IntegrationTime;
            SettingsManager.LampEnable = this.LampEnable;
            SettingsManager.NumSpectraPerRequest = this.NumSpectraPerRequest;
            SettingsManager.CurrentPlotType = this.CurrentPlotType;
            SettingsManager.PlotThresholds = this.PlotThresholds;
            SettingsManager.PlotTriggerPoints = this.PlotTriggerPoints;
            SettingsManager.ResultSpectrumType = this.ResultSpectrumType;
            SettingsManager.ReferenceType = this.ReferenceType;
            SettingsManager.LastPlotType = this.LastPlotType;
            SettingsManager.SaveDirectory = this.SaveDirectory;
            SettingsManager.SaveFileName = this.SaveFileName;
            SettingsManager.SaveStartPixel = this.SaveStartPixel;
            SettingsManager.SaveEndPixel = this.SaveEndPixel;
            SettingsManager.SaveStartWavelength = this.SaveStartWavelength;
            SettingsManager.SaveRangeUnits = this.SaveRangeUnits;
            SettingsManager.SaveEndWavelength = this.SaveEndWavelength;
            SettingsManager.SaveToFile = this.SaveToFile;
            SettingsManager.ScansToAverage = this.ScansToAverage;
            SettingsManager.SpectrumFilteringAverage = this.SpectrumFilteringAverage;
            SettingsManager.SpectrumFilteringEnabled = this.SpectrumFilteringEnabled;
            SettingsManager.SpectrumFilteringGaussian = this.SpectrumFilteringGaussian;
            SettingsManager.SpectrumFilteringSupport = this.SpectrumFilteringSupport;
            SettingsManager.ContinuousStrobePulseEnabled = this.ContinuousStrobePulseEnabled;
            SettingsManager.ContinuousStrobePulsePeriod = this.ContinuousStrobePulsePeriod;
            SettingsManager.ContinuousStrobePulseWidth = this.ContinuousStrobePulseWidth;
            SettingsManager.SingleStrobePulseEnabled = this.SingleStrobePulseEnabled;
            SettingsManager.SingleStrobePulseDelay = this.SingleStrobePulseDelay;
            SettingsManager.SingleStrobePulseWidth = this.SingleStrobePulseWidth;
            SettingsManager.SpectrumThresholdingEnabled = this.SpectrumThresholdingEnabled;
            SettingsManager.SpectrumThresholdingAllSatisfiedEnabled = this.SpectrumThresholdingAllSatisfiedEnabled;
            SettingsManager.TriggerMode = this.TriggerMode;
            SettingsManager.SaveOnWrite = this.SaveOnWrite;
            SettingsManager.SingleSoftwareTriggerEnabled = this.SingleSoftwareTriggerEnabled;
            SettingsManager.NumberOfSpectraForDynamicAccumulation = this.NumberOfSpectraForDynamicAccumulation;
            SettingsManager.StaticReferenceAccumulateTime = this.StaticReferenceAccumulateTime;
            SettingsManager.IntervalForDynamicAccumulation = this.IntervalForDynamicAccumulation;
            SettingsManager.Wavelengths = this.Wavelengths;
            SettingsManager.NumberOfTimePointsToStore = this.NumberOfTimePoints;
            SettingsManager.SaveStepPixel = this.SaveStepPixel;
            SettingsManager.SaveFileNameAutoUpdate = this.SaveFileNameAutoUpdate;
        }
    }
}
