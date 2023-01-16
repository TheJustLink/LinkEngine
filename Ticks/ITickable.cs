using LinkEngine.Time;

namespace LinkEngine.Ticks
{
    interface ITickable
    {
        void Tick(ElapsedTime time);
    }
}