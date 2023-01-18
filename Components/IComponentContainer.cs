using System.Threading.Tasks;

namespace LinkEngine.Components
{
    public interface IComponentContainer
    {
        T Add<T>() where T : class, IComponent;
        Task<T> AddAsync<T>() where T : class, IComponent;

        void Remove<T>() where T : class, IComponent;

        T Get<T>() where T : class, IComponent;
        bool TryGet<T>(out T component) where T : class, IComponent;

        bool Has<T>() where T : class, IComponent;
    }
}