using System.Collections.Generic;

namespace GameProject
{
    class CompositeTickable : ITickable
    {
        private readonly List<ITickable> _tickables;

        public CompositeTickable(params ITickable[] tickables) => _tickables = new List<ITickable>(tickables);

        public void Add(ITickable tickable) => _tickables.Add(tickable);
        public void Remove(ITickable tickable) => _tickables.Remove(tickable);
        public void Clear() => _tickables.Clear();

        public void Tick(ElapsedTime time)
        {
            foreach (var tickable in _tickables)
                tickable.Tick(time);
        }
    }
}