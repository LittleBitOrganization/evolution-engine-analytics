using System;
using com.adjust.sdk;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Services;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AdjustInitializer : MonoBehaviour
    {
        private void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.Adjust))
            {
                Adjust adjustPrefab = Resources.Load<Adjust>("Adjust");
                var instance = Instantiate(adjustPrefab);
                instance.transform.SetParent(transform);
                Adjust.start(analyticsConfig.AdjustConfig);
            }
        }
    }
}