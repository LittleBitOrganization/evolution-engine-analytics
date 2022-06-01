using System;

namespace LittleBit.Modules.Analytics.EventSystem.Actions
{
    public class FtueEventsTaskStep
    {
        public event Action OnFtueOrderStart;
        public event Action OnFtueOrderEnd;
    }
}