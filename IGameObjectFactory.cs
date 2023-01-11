using System.Threading.Tasks;

namespace GameProject
{
    public interface IGameObjectFactory
    {
        public IGameObject Create();
        public Task<IGameObject> CreateAsync();
    }
}