using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinkEngine.Assets
{
    public interface IAssetProvider
    {
        IEnumerable<T> GetMany<T>(string folderPath) where T : class;
        T Get<T>(string path) where T : class;

        Task<T> GetAsync<T>(string path) where T : class;
        Task<IEnumerable<T>> GetManyAsync<T>(string folderPath) where T : class;
    }
}