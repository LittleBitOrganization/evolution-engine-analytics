namespace LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency
{
    public interface IDataEventCurrency
    {
        public string Type { get; }

        public string PlaceId { get; }
        
        public string ResourceId { get; }
        
        public double CountResources { get; }
    }
}