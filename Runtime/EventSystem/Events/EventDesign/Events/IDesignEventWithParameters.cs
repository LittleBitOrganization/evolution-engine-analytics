using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;

namespace LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events
{
    public interface IDesignEventWithParameters
    {
        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams);
    }
}