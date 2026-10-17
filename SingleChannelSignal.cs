using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SignalParser
{
    internal class SingleChannelSignal
    {
        private SingleChannelSignal data;
        private double[] newSignal;

        public double Frequency { get; }
        public double[] Signal { get; }
        public double[] TimeTicks { get{
                double[] timeTicks = new double[Signal.Length];
                for (int i = 0; i < timeTicks.Length; i++)
                {
                    timeTicks[i] = i / Frequency;
                }
                return timeTicks;
            } 
        }
        public SingleChannelSignal(double frequency, double[] signal)
        {
            Frequency = frequency;
            Signal = signal;
        }

        public SingleChannelSignal(SingleChannelSignal data)
        {
            this.data = data;
        }

        public SingleChannelSignal(double[] newSignal)
        {
            this.newSignal = newSignal;
        }
    }
}
