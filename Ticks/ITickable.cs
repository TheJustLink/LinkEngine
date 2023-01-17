using LinkEngine.Time;

namespace LinkEngine.Ticks
{
    public interface ITickable
    {
        void Tick(ElapsedTime time);
    }
}