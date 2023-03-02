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
            if (analyticsConfig.IsEnableService(EventsServiceType.Wazzitude))
            {
                if (!string.IsNullOrEmpty(analyticsConfig.WazzitudeUrl))
                {
                    var adjustId = "";
                    var amplitudeId = "";
                    if (analyticsConfig.IsEnableService(EventsServiceType.Amplitude))
                        amplitudeId = GetAmplitudeDeviceId();
                    if (analyticsConfig.IsEnableService(EventsServiceType.Adjust))
                        adjustId = GetAdid();
                    WazzitudeAnalytics.Instance.Init(analyticsConfig.WazzitudeUrl, adjustId,amplitudeId);
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