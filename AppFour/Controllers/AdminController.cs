using System.Linq;
using System.Threading.Tasks;
using AppFour.Cotexts;
using AppFour.Globals;
using AppFour.Models.Collection;
using AppFour.Models.Entrance;
using CloudinaryDotNet;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppFour.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly AppDbContext Context;

        public AdminController(UserManager<User> usrMgr, AppDbContext context)
        {
            userManager = usrMgr;
            Context = context;
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
                    return RedirectToAction("Block", "Admin", new { ids = idsArr });
                else if (button.Equals("Unblock selected"))
                    return RedirectToAction("Unblock", "Admin", new { ids = idsArr });
                else if (button.Equals("Delete selected"))
                    return RedirectToAction("Delete", "Admin", new { ids = idsArr });
            }
            return View("UsersManager", userManager.Users);
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
            Cloudinary cloudinary = CloudAccount.Cloud();
            for (int g = 0; g < ids.Count(); g++)
            {
                var collectionsFinded = Context.Collections.Include(c => c.User).Where(u => u.UserId.Equals(ids[g]));
                foreach (Collection collection in collectionsFinded)
                {
                    cloudinary.DeleteResources(collection.ImageCloudId);
                }
            }
            for (int i = 0; i < ids.Count(); i++)
            {
                User usr = await userManager.FindByIdAsync(ids[i]);
                await userManager.DeleteAsync(usr);
            }
            string signeduserId = userManager.GetUserId(HttpContext.User);
            for (int i = 0; i < ids.Count(); i++)
            {
                if (ids[i].Equals(signeduserId)) { return RedirectToAction("Logout", "Entrance"); }
            }

            return View("UsersManager", userManager.Users);
        }
    }
}
