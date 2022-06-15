using System;

namespace LittleBit.Modules.Analytics.EventSystem.Configs
{
    [Flags]
    public enum EventsServiceType
    {
        Firebase = 1 << 0,
        GA = 1 << 1,
        Adjust = 1 << 2,
        Everything = Int32.MaxValue
    }
}