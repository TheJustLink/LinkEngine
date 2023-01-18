using LinkEngine.Components;

namespace LinkEngine.GameObjects
{
    public interface IGameObject
    {
        public IComponentContainer Components { get; }
    }
}