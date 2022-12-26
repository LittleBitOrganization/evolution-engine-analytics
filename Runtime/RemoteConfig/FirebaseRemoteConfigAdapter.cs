using Firebase.RemoteConfig;

namespace RemoteConfig
{
    public class FirebaseRemoteConfigAdapter : IRemoteConfig
    {
        private readonly FirebaseRemoteConfig _remoteConfig;

        public FirebaseRemoteConfigAdapter(FirebaseRemoteConfig remoteConfig) => _remoteConfig = remoteConfig;
        
        public bool GetBoolean(string name) => _remoteConfig.GetValue(name).BooleanValue;

        public string GetString(string name) => _remoteConfig.GetValue(name).StringValue;

        public double GetDouble(string name) => _remoteConfig.GetValue(name).DoubleValue;
    }
}