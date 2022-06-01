namespace LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency
{
    public interface ICurrencyEvent<in T> where T : IDataEventCurrency
    {
        public void EarnVirtualCurrency(T dataEventCurrency);
        public void SpendVirtualCurrency(T dataEventCurrency);
    }
}