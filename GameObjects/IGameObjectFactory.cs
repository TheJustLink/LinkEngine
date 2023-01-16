using System.Threading.Tasks;

namespace LinkEngine.GameObjects
{
    public interface IGameObjectFactory
    {
        public IGameObject Create();
        public Task<IGameObject> CreateAsync();
    }
}