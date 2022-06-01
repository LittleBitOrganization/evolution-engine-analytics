namespace LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression
{
    public interface IDataEventAdImpression
    {
        public string AdSource { get; }

        public string AdFormat { get; }

        public string AdUnitName { get; }

        public string Currency { get; }

        public double Value { get; }
    }
}