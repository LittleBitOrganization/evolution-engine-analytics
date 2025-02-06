using System.Collections.Generic;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBit.Modules.Analytics.Initializers;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Configs
{
    public abstract class AnalyticsServiceConfig : ScriptableObject
    {
        public abstract IInitializer CreateInitializer();
        public abstract IEventService CreateEventService();
    }
}