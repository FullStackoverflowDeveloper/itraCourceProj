using AppFour.Models.Entrance;
using System.Collections.Generic;

namespace AppFour.Models.Roles
{
    public class ChangeRoleModel1
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<User> UsersAll { get; set; }
        public IList<User> UsersInRole { get; set; }
        public ChangeRoleModel1()
        {
            UsersAll = new List<User>();
            UsersInRole = new List<User>();
        }
    }
}