using System.Threading.Tasks;

namespace RemoteConfig
{
    public interface IRemoteConfigLoader
    {
        public Task<IRemoteConfig> GetAsync();
    }
}