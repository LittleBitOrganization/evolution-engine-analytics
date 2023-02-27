using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;
using UnityEngine;
using Wazzitude;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class WazzitudeInitializer:IInitializer
    {
        public void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.Wazzitude)
                &&analyticsConfig.IsEnableService(EventsServiceType.Amplitude)
                &&analyticsConfig.IsEnableService(EventsServiceType.Adjust))
            {
                if (!string.IsNullOrEmpty(analyticsConfig.WazzitudeUrl))
                {
                    WazzitudeAnalytics.Instance.Init(analyticsConfig.WazzitudeUrl, GetAdid(),GetAmplitudeDeviceId());
                }
                else
                {
                    Debug.LogError("Wazzitude has no served url");
                }
            }
        }

        private string GetAdid()
        {
            return com.adjust.sdk.Adjust.getAdid();
        }

        private string GetAmplitudeDeviceId()
        {
            return Amplitude.Instance.getDeviceId();
        }
    }
    
    
    
    
}