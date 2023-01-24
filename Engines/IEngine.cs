using System;
using System.Numerics;

using LinkEngine.Assets;
using LinkEngine.GameObjects;
using LinkEngine.IO;
using LinkEngine.IO.Mouse;
using LinkEngine.Logs;

namespace LinkEngine.Engines
{
    public interface IEngine
    {
        public event Action Stopped;

        public ILogger Logger { get; }
        public IMouse Mouse { get; }
        public IInput<Vector2> Input { get; }
        public IGameObjectFactory GameObjectFactory { get; }
        public IAssetProvider AssetProvider { get; }
    }
}