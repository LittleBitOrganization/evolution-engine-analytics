using System.Linq;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using UnityEngine;

namespace RemoteConfig
{
    public class FirebaseRemoteConfigLoader : RemoteConfigLoader
    {
        public FirebaseRemoteConfigLoader(IRemoteConfig fallbackConfig) : base(fallbackConfig) { }

        protected override async Task<IRemoteConfig> GetAsyncImpl()
        {
            var config = FirebaseRemoteConfig.DefaultInstance;

            Debug.Log("Trying to fetch firebase remote config");

            await config.FetchAsync();
            await config.ActivateAsync();

            Debug.Log("Firebase remote config has been fetched!");
            Debug.Log("Firebase remote config values: " + "[" + string.Join(", ", config.AllValues.Keys.Select(c => "'" + c + "'")) + "]");

            return new FirebaseRemoteConfigAdapter(FirebaseRemoteConfig.DefaultInstance);
        }
    }
}