namespace LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce
{
    public interface IDataEventEcommerce
    {
        public string ItemId{ get; }

        public string CartType{ get; }

        public string Receipt { get; }

        public string Signature{ get; }

        public string Currency{ get; }

        public int Amount{ get; }

        public string ItemType{ get; }
    }
}