namespace AppFour.Models.Collection
{
    public class CustomFieldData
    {
        public string Id { get; set; }
        public string Data { get; set; }
        public string CustomFieldId { get; set; }
        public string ItemId { get; set; }
        public CustomField CustomField { get; set; }
        public Item.Item Item { get; set; }
    }
}
