using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParser
{
    static class SingleChannelSignalReader
    {
        static public SingleChannelSignal SignalReader(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {
                double frequency = double.Parse(reader.ReadLine());
                List<double> Signal = new List<double>();
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    Signal.Add(double.Parse(line));
                }
                double[] arrSignal = Signal.ToArray();
                SingleChannelSignal data = new SingleChannelSignal(frequency, arrSignal);
                return data;
            }
        }
    }
}
