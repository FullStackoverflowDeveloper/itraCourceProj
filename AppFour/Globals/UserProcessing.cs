using AppFour.Models.Entrance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AppFour.Globals
{
    public class UserProcessing
    {
        public static User UpdateUser(User user)
        {
            user.LatestLoginDate = DateTime.Now.ToString();
            return user;
        }

        public static bool CheckBlock(User user)
        {
            bool result;
            if (user.Status)
            {
                result = false;
            }
            else
            {
                result = true; 
            }
            return result;
        }
    }
}
