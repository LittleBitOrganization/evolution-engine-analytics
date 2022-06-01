namespace LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency
{
    public class DataEventCurrency : IDataEventCurrency
    {
        private readonly string _resourceId;
        private readonly double _countResources;
        private readonly string _type;
        private readonly string _placeId;

        public string ResourceId => _resourceId;

        public double CountResources => _countResources;

        public string Type => _type;

        public string PlaceId => _placeId;
        
        public DataEventCurrency(string resourceId, double countResources, string type, string placeId)
        {
            _resourceId = resourceId;
            _countResources = countResources;
            _type = type;
            _placeId = placeId;
        }
    }
}