namespace LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce
{
    public class DataEventEcommerce : IDataEventEcommerce
    {
        private readonly string _currency;
        private readonly int _amount;
        private readonly string _itemType;
        private readonly string _itemId;
        private readonly string _cartType;
        private readonly string _receipt;
        private readonly string _signature;

        public DataEventEcommerce(string currency, int amount, string itemType, string itemId, string cartType, string receipt, string signature)
        {
            _currency = currency;
            _amount = amount;
            _itemType = itemType;
            _itemId = itemId;
            _cartType = cartType;
            _receipt = receipt;
            _signature = signature;
        }

        public string ItemId => _itemId;

        public string CartType => _cartType;

        public string Receipt => _receipt;

        public string Signature => _signature;

        public string Currency => _currency;

        public int Amount => _amount;

        public string ItemType => _itemType;
    }
}