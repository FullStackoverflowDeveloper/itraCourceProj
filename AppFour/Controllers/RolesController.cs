using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFour.Models.Entrance;
using AppFour.Models.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppFour.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public RolesController(RoleManager<IdentityRole> rlManager, UserManager<User> usrManager)
        {
            roleManager = rlManager;
            userManager = usrManager;
        }

        
        public IActionResult RolesManager(string button, string[] idsArr)
        {
            if (button.Equals("Create role"))
            {
                return RedirectToAction("Create", "Roles");
            }
            else
            {
                if (idsArr == null || idsArr.Length == 0)
                {
                    return View("Index", roleManager.Roles);
                }
                else
                {
                    if (button.Equals("Delete selected"))
                    {
                        return RedirectToAction("Delete", new { ids = idsArr });
                    }
                    else
                    {
                        return View("Index", roleManager.Roles);
                    }
                }
            }
        }
        public IActionResult Index() => View(roleManager.Roles.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", roleManager.Roles);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        public async Task<IActionResult> Delete(string[] ids)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                IdentityRole role = await roleManager.FindByIdAsync(ids[i]);
                await roleManager.DeleteAsync(role);
            }
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string userId)
        {
            User user = await userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = roleManager.Roles.ToList();
                ChangeRoleModel model = new ChangeRoleModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit1(string roleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            if(role != null)
            {
        
                var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
                var usersAll = userManager.Users.ToList();
                ChangeRoleModel1 model = new ChangeRoleModel1
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    UsersAll = usersAll,
                    UsersInRole = usersInRole
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit2(string roleId, List<string> users)
        {
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);
            
                List<User> usersList = new List<User>();
                foreach(var user in users)
                {
                    var userResult = await userManager.FindByIdAsync(user);
                    usersList.Add(userResult);
                }

                var addedUsers = usersList.Except(usersInRole);
                var removedUsers = usersInRole.Except(usersList);
                foreach (User user in addedUsers)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                foreach (User user in removedUsers)
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                return View("Index", roleManager.Roles.ToList());
            }
            return NotFound();
        }
    }
}
