namespace RemoteConfig
{
    public interface IRemoteConfig
    {
        public bool GetBoolean(string name);
        public string GetString(string name);
        public double GetDouble(string name);
    }
}