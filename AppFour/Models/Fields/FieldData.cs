using System.ComponentModel.DataAnnotations;

namespace AppFour.Models.Fields
{
    public class FieldData
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string Value { get; set; }
        public string FieldId { get; set; }
        public CustomField Field { get; set; }
        [Required]
        public string ItemId { get; set; }
        [Required]
        public Item.Item Item { get; set; }
    }
}
