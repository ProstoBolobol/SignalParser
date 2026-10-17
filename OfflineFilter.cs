using System;
using System.Linq;

namespace SignalParser
{
    public static class OfflineFilter
    {
        public static float[] Filter(float[] data, int windowSize)
        {
            if (data == null || data.Length == 0) return Array.Empty<float>();
            if (windowSize < 1) windowSize = 1;
            if (windowSize % 2 == 0) windowSize++;

            int M = (windowSize - 1) / 2;
            float first = data[0];
            float last = data[data.Length - 1];

            // Расширение: M копий первого + исходный + M копий последнего
            float[] extended = new float[data.Length + 2 * M];
            for (int i = 0; i < M; i++) extended[i] = first;
            for (int i = 0; i < data.Length; i++) extended[M + i] = data[i];
            for (int i = 0; i < M; i++) extended[M + data.Length + i] = last;

            // Скользящее среднее через LINQ
            return Enumerable.Range(0, data.Length)
                .Select(i => (float)Enumerable.Range(0, windowSize)
                    .Select(j => extended[i + j])
                    .Average())
                .ToArray();
        }
    }
}