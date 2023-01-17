using System;
using LinkEngine.IO;
using LinkEngine.Time;

namespace LinkEngine.Ticks
{
    public class TickCounter : ITickable
    {
        private readonly IOutput<string> _output;

        private int _tick;
        private TimeSpan _time;

        public TickCounter(IOutput<string> output) => _output = output;

        public void Tick(ElapsedTime time)
        {
            _tick++;
            _time += time.Delta;

            if (_time < TimeSpan.FromSeconds(1)) return;

            _output.Write($"{_tick}/{_time.TotalSeconds:N2} secs");

            _tick = 0;
            _time = TimeSpan.Zero;
        }
    }
}