using AppFour.Globals;
using AppFour.Models.Entrance;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppFour.Models.Collection
{
    public class Collection
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        [EnumDataType(typeof(Topic))]
        public Topic? Topic { get; set; }
        public string ImgUrl { get; set; }
        public string ImageCloudId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public List<Item.Item> Items { get; set; }
    }
}