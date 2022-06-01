namespace LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression
{
    public class DataEventAdImpression : IDataEventAdImpression
    {
        public DataEventAdImpression(string adSource, string adFormat, string adUnitName, string currency, double value)
        {
            AdSource = adSource;
            AdFormat = adFormat;
            AdUnitName = adUnitName;
            Currency = currency;
            Value = value;
        }

        public string AdSource { get; }
        public string AdFormat { get; }
        public string AdUnitName { get; }
        public string Currency { get; }
        public double Value { get; }
    }
}