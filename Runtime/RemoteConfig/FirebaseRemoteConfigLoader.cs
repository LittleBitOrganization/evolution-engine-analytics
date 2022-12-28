using System;
using System.Linq;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using UnityEngine;

namespace RemoteConfig
{
    public class FirebaseRemoteConfigLoader : RemoteConfigLoader
    {
        private readonly AnalyticsConfig _analyticsConfig;

        public FirebaseRemoteConfigLoader(IRemoteConfig fallbackConfig, AnalyticsConfig analyticsConfig) : base(fallbackConfig) =>
            _analyticsConfig = analyticsConfig;

        protected override async Task<IRemoteConfig> GetAsyncImpl()
        {
            var config = FirebaseRemoteConfig.DefaultInstance;

            Debug.Log("Trying to fetch firebase remote config");

            await config.FetchAsync(TimeSpan.FromHours(_analyticsConfig.RemoteConfigCacheExpiration));
            await config.ActivateAsync();

            Debug.Log("Firebase remote config has been fetched!");
            Debug.Log("Firebase remote config values: " + "[" + string.Join(", ", config.AllValues.Keys.Select(c => "'" + c + "'")) + "]");

            return new FirebaseRemoteConfigAdapter(FirebaseRemoteConfig.DefaultInstance);
        }
    }
}
