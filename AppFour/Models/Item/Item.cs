using AppFour.Models.Fields;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppFour.Models.Item
{
    public class Item
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string Title { get; set; }

        [Required]
        public string CollectionId { get; set; }
        [Required]
        public Collection.Collection Collection { get; set; }

        public List<FieldData> DataFields { get; set; }
    }
}
