using LinkEngine.Components;

namespace LinkEngine.GameObjects
{
    public interface IGameObject
    {
        public ITransform Transform { get; }
    }
}