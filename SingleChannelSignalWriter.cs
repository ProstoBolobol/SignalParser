using Microsoft.Win32;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParser
{
    static internal class SingleChannelSignalWriter
    {
        static public void SignalWriter(SingleChannelSignal data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(data.Frequency);
                foreach (double element in data.Signal)
                {
                    writer.WriteLine(element);
                }
                
            }
        }
    }
}

