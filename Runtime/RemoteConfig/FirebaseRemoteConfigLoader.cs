using System;
using System.Linq;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using LittleBit.Modules.Analytics.Initializers;
using UnityEngine;

namespace RemoteConfig
{
    public class FirebaseRemoteConfigLoader
    {
        public event Action Loaded;
        public FirebaseRemoteConfig Result { get; private set; }
    
        public FirebaseRemoteConfigLoader(AnalyticsInitializer analyticsInitializer) =>
            analyticsInitializer.OnFirebaseInit += _ => new Task(TryLoad).Start();
    
        private async void TryLoad()
        {
            var config = FirebaseRemoteConfig.DefaultInstance;
    
            Debug.Log("Trying to fetch firebase remote config");
    
            await config.FetchAsync();
            await config.ActivateAsync();
    
            Debug.Log("Firebase remote config has been fetched!");
            Debug.Log("Firebase remote config values: " + "[" + string.Join(", ", config.AllValues.Keys.Select(c => "'" + c + "'")) + "]");
            
            Result = config;
    
            Loaded?.Invoke();
        }
    }
}