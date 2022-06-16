using com.adjust.sdk;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Services;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AdjustInitializer : IInitializer
    {
        private Transform _parent; 
        public AdjustInitializer(GameObject parent)
        {
            _parent = parent.transform;
        }
        public void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.Adjust))
            {
                Adjust adjustPrefab = Resources.Load<Adjust>("Adjust");
                var instance = Object.Instantiate(adjustPrefab);
                instance.transform.SetParent(_parent);
                Adjust.start(analyticsConfig.AdjustConfig);
            }
        }
    }
}