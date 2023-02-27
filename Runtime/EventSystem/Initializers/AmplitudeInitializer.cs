using System;
using com.adjust.sdk;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AmplitudeInitializer : IInitializer
    {

        public void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.Amplitude))
            {
                if (!string.IsNullOrEmpty(analyticsConfig.AmplitudeApiKey))
                {
                    try
                    {
                        Amplitude amplitude = Amplitude.Instance;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
                        Amplitude.Instance.logging = true;
                        Debug.Log("Init amplitude " + analyticsConfig.AmplitudeApiKey);
#else
                    Amplitude.Instance.logging = false;
#endif

                        amplitude.trackSessionEvents(true);

                        amplitude.init(analyticsConfig.AmplitudeApiKey);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.StackTrace);
                    }
                }
                else
                {
                    Debug.LogError("Amplitude has no api key");
                }
            }
        }
    }
}