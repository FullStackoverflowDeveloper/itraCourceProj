using AppFour.Globals;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppFour.Models.Fields
{
    public class CustomField
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string Key { get; set; }
        [EnumDataType(typeof(ValueType))]
        public ValueType ValueType { get; set; }

        [Required]
        public string CollectionId { get; set; }
        [Required]
        public Collection.Collection Collection { get; set; }

        public List<FieldData> DataFields { get; set; }

        public CustomField(string CollectionId, string Key, ValueType ValueType)
        {
            Id = System.Guid.NewGuid().ToString();
            this.Key = Key;
            this.CollectionId = CollectionId;
            this.ValueType = ValueType;
        }
    }
}