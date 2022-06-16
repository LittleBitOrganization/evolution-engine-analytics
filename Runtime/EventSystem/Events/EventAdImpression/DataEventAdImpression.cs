namespace LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression
{
    public class DataEventAdImpression : IDataEventAdImpression
    {
        public DataEventAdImpression(SdkSource sdkSource, string adSource, string adFormat, string adUnitName, string currency, double value)
        {
            SdkSource = sdkSource;
            AdSource = adSource;
            AdFormat = adFormat;
            AdUnitName = adUnitName;
            Currency = currency;
            Value = value;
        }

        public SdkSource SdkSource { get; }
        public string AdSource { get; }
        public string AdFormat { get; }
        public string AdUnitName { get; }
        public string Currency { get; }
        public double Value { get; }
    }
}