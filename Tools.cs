using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalParser
{
    internal static class Tools
    {
        public static SingleChannelSignal GenerateSinus(double phase, double period, double frequency, double len)
        {
            int count = (int)(len * frequency + 0.5);
            double[] signal = new double[count];
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] = Math.Sin(2 * Math.PI * (i / frequency + phase) / period);
            }
           
            return new SingleChannelSignal(frequency, signal);
        }
        public static SingleChannelSignal GenerateLine(double k, double b, double frequency, double len)
        {
            int count = (int)(len * frequency + 0.5);
            double[] signal = new double[count];
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] = k * i + b;
            }

            return new SingleChannelSignal(frequency, signal);
        }
        public static SingleChannelSignal GenerateNoise(double amplitude, double frequency, double len)
        {
            int count = (int)(len * frequency + 0.5);
            double[] signal = new double[count];
            Random rnd = new Random();
            for (int i = 0; i < signal.Length; i++)
            {
                signal[i] = rnd.NextDouble() * amplitude - amplitude / 2;
            }
            return new SingleChannelSignal(frequency, signal);
        }

        public static SingleChannelSignal GenerateSum(List<SingleChannelSignal> dataList)
        {
            if (dataList.Count == 0)
            {
                return null;
            }            
            double frequency = dataList.First().Frequency;
            foreach (SingleChannelSignal data in dataList) 
            {
                if (Math.Abs(frequency - data.Frequency) > 1E-6)
                {
                    return null;
                }
                else
                {
                    frequency = data.Frequency;
                }    
            }
            int maxLength = dataList.Select(l => l.Signal.Length).Max();
            double[] newSignal = new double[maxLength];
            foreach (SingleChannelSignal data in dataList)
            {
                for (int i = 0; i < data.Signal.Length; i++)
                {
                    newSignal[i] += data.Signal[i]; 
                }
            }
            return new SingleChannelSignal(frequency, newSignal);
        }
        public static SingleChannelSignal GenerateConcat(List<SingleChannelSignal> dataList)
        {
            if (dataList.Count == 0)
            {
                return null;
            }
            double frequency = dataList.First().Frequency;
            foreach (SingleChannelSignal data in dataList)
            {
                if (frequency != data.Frequency)
                {
                    return null;
                }
                else
                {
                    frequency = data.Frequency;
                }
            }
            int maxLen = dataList.Select(l => l.Signal.Length).Sum();
            double[] newSignal = dataList.First().Signal;
            foreach (SingleChannelSignal data in dataList.Skip(1))
            {
                newSignal = newSignal.Concat(data.Signal).ToArray();

            }
            return new SingleChannelSignal(frequency, newSignal);
        }
    }
}
