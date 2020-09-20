using System.Collections.Generic;

namespace AppFour.Models.Collection
{
    public class CustomField
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CollectionId { get; set; }
        public Collection Collection { get; set; }
        public ICollection<CustomFieldData> CustomFieldData { get; set; }
    }
}