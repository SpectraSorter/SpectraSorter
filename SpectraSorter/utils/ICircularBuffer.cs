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

namespace spectra.utils
{
    /// <summary>
    /// Circular buffer interface.
    /// </summary>
    ///
    /// See http://geekswithblogs.net/blackrob/archive/2014/09/01/circular-buffer-in-c.aspx.
    ///
    /// <typeparam name="T"></typeparam>
    internal interface ICircularBuffer<T>
    {
        int Count { get; }
        int Capacity { get; set; }

        T Enqueue(T item);

        T Dequeue();

        void Clear();

        T this[int index] { get; set; }

        int IndexOf(T item);

        void Insert(int index, T item);

        void RemoveAt(int index);
    }
}