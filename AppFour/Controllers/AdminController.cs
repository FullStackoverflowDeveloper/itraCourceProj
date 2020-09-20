using System.Linq;
using System.Threading.Tasks;
using AppFour.Models.Entrance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppFour.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;

        public AdminController(UserManager<User> usrMgr)
        {
            userManager = usrMgr;
        }

        public IActionResult UsersManager() => View(userManager.Users);

        public IActionResult  TableUsersManager(string button, string[] idsArr)
        {
            if (idsArr == null || idsArr.Length == 0 || button == "")
            {
                return View("UsersManager", userManager.Users);
            }
            else
            {
                if (button.Equals("Block selected"))
                {
                    return RedirectToAction("Block", "Admin", new { ids = idsArr });
                }
                else if (button.Equals("Unblock selected"))
                {
                    return RedirectToAction("Unblock", "Admin", new { ids = idsArr });
                }
                else if (button.Equals("Delete selected"))
                {
                    return RedirectToAction("Delete", "Admin", new { ids = idsArr });
                }
                else
                    return View("UsersManager", userManager.Users);
            }
        }

        public async Task<IActionResult> Block(string[] ids)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                User usr = await userManager.FindByIdAsync(ids[i]);
                usr.Status = false;
                await userManager.UpdateAsync(usr);
            }
            User userBlock = await userManager.FindByIdAsync(userManager.GetUserId(HttpContext.User));
            if (userBlock.Status == false)
            {
                return RedirectToAction("Logout", "Entrance");
            }
            return View("UsersManager", userManager.Users);
        }
        public async Task<IActionResult> UnBlock(string[] ids)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                User usr = await userManager.FindByIdAsync(ids[i]);
                usr.Status = true;
                await userManager.UpdateAsync(usr);
            }
            return View("UsersManager", userManager.Users);
        }

        public async Task<IActionResult> Delete(string[] ids)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                User usr = await userManager.FindByIdAsync(ids[i]);
                await userManager.DeleteAsync(usr);
            }
            return View("UsersManager", userManager.Users);
        }
    }
}
