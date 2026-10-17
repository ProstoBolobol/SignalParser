using System;

namespace SignalParser
{
    public class OnlineFilter
    {
        private readonly int windowSize;
        private readonly float[] buffer;
        private int bufferIndex;
        private int bufferCount;
        private float bufferSum;

        public OnlineFilter(int windowSize)
        {
            if (windowSize < 1)
                throw new ArgumentException("windowSize должно быть >= 1", nameof(windowSize));
            this.windowSize = windowSize;
            buffer = new float[windowSize];
            bufferIndex = 0;
            bufferCount = 0;
            bufferSum = 0f;
        }

        public float[] Filter(float[] data)
        {
            if (data == null || data.Length == 0) return Array.Empty<float>();
            float[] result = new float[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                if (bufferCount < windowSize)
                {
                    buffer[bufferIndex] = data[i];
                    bufferSum += data[i];
                    bufferCount++;
                    bufferIndex = (bufferIndex + 1) % windowSize;
                    result[i] = bufferSum / bufferCount;
                }
                else
                {
                    bufferSum -= buffer[bufferIndex];
                    buffer[bufferIndex] = data[i];
                    bufferSum += data[i];
                    bufferIndex = (bufferIndex + 1) % windowSize;
                    result[i] = bufferSum / windowSize;
                }
            }
            return result;
        }
    }
}