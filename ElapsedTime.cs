using System;

namespace GameProject
{
    class ElapsedTime
    {
        public float DeltaSeconds => (float)Delta.TotalSeconds;
        public float TotalSeconds => (float)Total.TotalSeconds;

        public TimeSpan Delta { get; }
        public TimeSpan Total { get; }

        public ElapsedTime(TimeSpan delta, TimeSpan total)
        {
            Delta = delta;
            Total = total;
        }
    }
}