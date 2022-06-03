namespace LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce
{
    public interface IEcommerceEvent<in T> where T : IDataEventEcommerce
    {
        void EcommercePurchase(T data);
    }
}