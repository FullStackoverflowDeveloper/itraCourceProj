using AppFour.Globals;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public CollectionAction()
        { 
        }

        public CollectionAction(string Title, string Description, Topic? Topic, IFormFile Image, string UserId)
        {
            this.Title = Title;
            this.Description = Description;
            this.Topic = Topic;
            this.Image = Image;
            this.UserId = UserId;
        }
    }
}