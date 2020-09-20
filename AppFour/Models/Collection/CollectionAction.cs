using AppFour.Globals;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace AppFour.Models.Collection
{
    public class CollectionAction
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [EnumDataType(typeof(Topic))]
        public Topic? Topic { get; set; }
        public IFormFile Image { get; set; }
        public string ImgUrl { get; set; }
        public string ImageCloudId { get; set; }
    }
}
