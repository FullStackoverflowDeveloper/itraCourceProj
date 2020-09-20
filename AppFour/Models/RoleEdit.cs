using AppFour.Models.Entrance;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AppFour.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}