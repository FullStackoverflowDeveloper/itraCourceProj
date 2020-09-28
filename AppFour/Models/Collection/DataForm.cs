using AppFour.Globals;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AppFour.Models.Collection
{
    public class DataForm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [EnumDataType(typeof(Topic))]
        public Topic? Topic { get; set; }
        public IFormFile Image { get; set; }
        public string AdditionalFields { get; set; }
    }
}