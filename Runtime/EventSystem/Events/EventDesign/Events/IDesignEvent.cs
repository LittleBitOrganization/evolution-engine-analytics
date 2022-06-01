using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;

namespace LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events
{
    public interface IDesignEvent <in T> where T : IDataEventDesign
    {
        public void DesignEvent(DataEventDesign label);
    }
}