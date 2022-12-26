using System.Threading.Tasks;
using UnityEngine;

namespace RemoteConfig
{
    public abstract class RemoteConfigLoader : IRemoteConfigLoader
    {
        private readonly IRemoteConfig _fallbackConfig;
        
        protected abstract Task<IRemoteConfig> GetAsyncImpl();

        public RemoteConfigLoader(IRemoteConfig fallbackConfig) => _fallbackConfig = fallbackConfig;

        public async Task<IRemoteConfig> GetAsync()
        {
            var implTask = GetAsyncImpl();
            await implTask;
            
            return !implTask.IsCompletedSuccessfully ? _fallbackConfig : implTask.Result;
        }
    }
}