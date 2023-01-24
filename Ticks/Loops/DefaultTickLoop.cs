using System;
using System.Threading;
using LinkEngine.Time;

namespace LinkEngine.Ticks.Loops
{
    public class DefaultTickLoop : ITickLoop, ITickable
    {
        public event Action? Started;
        public event Action? Stopped;
        public event Action<Exception>? Aborted;

        private readonly Thread _thread;
        private readonly ITickable _tickable;
        private readonly TimeSpan _stopTimeout;
        private readonly TimeSpan _tickTimestep;

        private bool _isRunning;
        private bool _isStopping;

        public DefaultTickLoop(ITickable tickable, int tickRate = 60, int timeout = 3)
        {
            _tickable = tickable;

            _tickTimestep = TimeSpan.FromTicks((long)10_000_000m / tickRate);
            _stopTimeout = TimeSpan.FromSeconds(timeout);

            _thread = new Thread(Loop);
            _thread.IsBackground = true;
            _thread.Name = "Tick Loop Thread (" + tickRate + ")/sec";
            _thread.Priority = ThreadPriority.Highest;
        }

        public void Start()
        {
            _isRunning = true;
            _thread.Start();

            Started?.Invoke();
        }
        public void Stop()
        {
            _isStopping = true;

            _thread.Join(_stopTimeout);

            if (_isRunning)
                _thread.Abort();

            Stopped?.Invoke();
        }

        private void Loop()
        {
            var startLoopDate = DateTime.Now;
            var startTickTime = DateTime.Now - _tickTimestep;

            while (_isStopping == false)
            {
                // Calculate delta time

                var currentTime = DateTime.Now;
                var deltaTime = currentTime - startTickTime;

                // Tick

                startTickTime = currentTime;
                Tick(new ElapsedTime(deltaTime, currentTime - startLoopDate));

                // Calculate sleep time

                var tickTime = DateTime.Now - startTickTime;
                var sleepTime = TimeSpan.Zero;

                if (tickTime < _tickTimestep)
                    sleepTime = _tickTimestep - tickTime;

                // Sleep

                Thread.Sleep(sleepTime);
            }

            _isRunning = false;
        }

        public void Tick(ElapsedTime time)
        {
            try
            {
                _tickable.Tick(time);
            }
            catch (Exception exception)
            {
                Aborted?.Invoke(exception);
            }
        }
    }
}