using System;
using System.Linq;
using UnityEngine;

namespace RemoteConfig
{
    [Serializable]
    public class FallbackConfig : IRemoteConfig
    {
        [SerializeField] private KeyValuePair[] _values;

        public bool GetBoolean(string name) => GetValue<bool>(name);

        public string GetString(string name) => GetValue<string>(name);

        public double GetDouble(string name) => GetValue<double>(name);
        private T GetValue<T>(string name) => (T) Convert.ChangeType(_values.First(v => v.Name == name), typeof(T));

        [Serializable]
        public struct KeyValuePair
        {
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public string Value { get; private set; }
        }
    }
}