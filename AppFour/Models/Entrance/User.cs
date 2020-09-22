using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace AppFour.Models.Entrance
{
    public class User : IdentityUser
    {
        public string RegistrationDate { get; set; }
        public string LatestLoginDate { get; set; }
        public bool Status { get; set; }
        public bool Check { get; set; }
        public List<Collection.Collection> Collections { get; set; }
    }
}