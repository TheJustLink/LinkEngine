using System;
using System.Linq;

using LinkEngine.Engines;

namespace LinkEngine
{   
    public static class EntryPoint
    {
        public static void Enter(IEngine engine)
        {
            engine.Logger.Write("[EntryPoint] Enter");

            var applicationTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && type.IsAbstract == false && typeof(IApplication).IsAssignableFrom(type));

            foreach (var applicationType in applicationTypes)
            {
                engine.Logger.Write($"[EntryPoint] Create app {applicationType.Name}");

                Activator.CreateInstance(applicationType, engine);
            }

            engine.Logger.Write("[EntryPoint] Exit");
        }
    }
}