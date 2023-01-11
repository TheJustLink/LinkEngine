using System;

namespace GameProject
{
    public class Application
    {
        private readonly IEngine _engine;
        private readonly ITickLoop _tickLoop;

        public Application(IEngine engine)
        {
            _engine = engine;
            _tickLoop = InitializeTickLoop();
        }

        private ITickLoop InitializeTickLoop()
        {
            var mainTickables = new CompositeTickable(
                new TickCounter(_engine.Logger),
                new Game(_engine)
            );
            
            var compositeTickLoop = new CompositeTickLoop(
                new DefaultTickLoop(mainTickables)
            );

            compositeTickLoop.Started += TickLoopStarted;
            compositeTickLoop.Stopped += TickLoopStopped;
            compositeTickLoop.Aborted += TickLoopAborted;

            return compositeTickLoop;
        }
        
        public void Start()
        {
            _engine.Logger.Write("App started");
            
            _tickLoop.Start();
        }
        public void Stop()
        {
            _tickLoop.Stop();

            _engine.Logger.Write("App stopped");
        }

        private void TickLoopStarted()
        {
            _engine.Logger.Write("Game tick loop started");
        }
        private void TickLoopStopped()
        {
            _engine.Logger.Write("Game tick loop stopped successfully");
        }
        private void TickLoopAborted(Exception exception)
        {
            _engine.Logger.WriteError($"Game tick loop aborted, {exception}");
        }
    }
}