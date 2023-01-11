using System;

namespace GameProject
{
    interface ITickLoop
    {
        event Action? Started;
        event Action? Stopped;
        event Action<Exception>? Aborted;
        
        void Start();
        void Stop();
    }
}