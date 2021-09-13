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

using System;
using System.Threading;

namespace spectra.utils
{
    internal class HighResStopWatch : System.Diagnostics.Stopwatch
    {
        private readonly double _microSecPerTick = 1000000D / System.Diagnostics.Stopwatch.Frequency;

        public HighResStopWatch()
        {
            if (!System.Diagnostics.Stopwatch.IsHighResolution)
            {
                throw new Exception("This system does not support high-resolution timers!");
            }
        }

        public long ElapsedMicroseconds
        {
            get
            {
                return (long)(ElapsedTicks * _microSecPerTick);
            }
        }

        public void WaitForMicroseconds(long microseconds)
        {
            while ((long)(ElapsedTicks * _microSecPerTick) <= microseconds)
            {
                // Context switch
                Thread.Sleep(0);
            }
        }

        public static long TimerAccuracyInNanoseconds()
        {
            return 1000000000L / HighResStopWatch.Frequency;
        }
    }
}