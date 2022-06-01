namespace LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression
{
    public interface IAdImpressionEvent<in T> where T : IDataEventAdImpression
    {
        void AdRevenuePaidEvent(T data);
    }
}