using System.Numerics;

namespace GameProject
{
    public interface IEngine
    {
        public ILogger Logger { get; }
        public IInput<Vector2> Input { get; }
        public IGameObjectFactory GameObjectFactory { get; }
        public IAssetProvider AssetProvider { get; }
    }
}