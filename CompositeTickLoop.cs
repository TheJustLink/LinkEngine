using System;
using System.Collections.Generic;

namespace GameProject
{
    class CompositeTickLoop : ITickLoop
    {
        public event Action? Started;
        public event Action? Stopped;
        public event Action<Exception>? Aborted;

        private readonly List<ITickLoop> _tickLoops;

        public CompositeTickLoop(params ITickLoop[] tickLoops)
        {
            _tickLoops = new List<ITickLoop>(tickLoops);

            foreach (var tickLoop in _tickLoops)
                tickLoop.Aborted += OnTickLoopAborted;
        }

        public void Add(ITickLoop tickLoop)
        {
            _tickLoops.Add(tickLoop);

            tickLoop.Aborted += OnTickLoopAborted;
        }
        public void Remove(ITickLoop tickLoop)
        {
            _tickLoops.Remove(tickLoop);

            tickLoop.Aborted -= OnTickLoopAborted;
        }
        public void Clear()
        {
            foreach (var tickLoop in _tickLoops)
                tickLoop.Aborted -= OnTickLoopAborted;

            _tickLoops.Clear();
        }

        public void Start()
        {
            foreach (var tickLoop in _tickLoops)
                tickLoop.Start();

            Started?.Invoke();
        }
        public void Stop()
        {
            foreach (var tickLoop in _tickLoops)
                tickLoop.Stop();

            Stopped?.Invoke();
        }

        private void OnTickLoopAborted(Exception exception) => Aborted?.Invoke(exception);
    }
}