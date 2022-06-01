using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Configs
{
    public class AnalyticsConfig : ScriptableObject
    {
        
        [SerializeField, EnumFlags] private EventsServiceType enabledServices;

        public EventsServiceType EnabledServices => enabledServices;
    }
}