using com.adjust.sdk;
using LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AdjustSystemEvent : IAdImpressionEvent<IDataEventAdImpression>
    {
        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(data.SdkSource.Source);
            // set revenue and currency
            adjustAdRevenue.setRevenue(data.Value, data.Currency);
            // optional parameters
            adjustAdRevenue.setAdRevenueNetwork(data.AdSource);
            // track ad revenue
            Adjust.trackAdRevenue(adjustAdRevenue);
        }
    }
}