using LittleBit.Modules.Analytics.EventSystem.Configs;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Services
{
    internal class AnalyticsConfigFactory
    {
        public AnalyticsConfig Create()
        {
            return Resources.Load<AnalyticsConfig>(Constants.AnalyticsConfigResourcesPath);
        }
    }
}