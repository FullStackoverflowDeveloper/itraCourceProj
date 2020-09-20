using System.Collections.Generic;
using System.Linq;
using AppFour.Models.Collection;
using AppFour.Globals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using Westwind.AspNetCore.Markdown;
using LinqToDB;
using AppFour.Cotexts;
using Microsoft.AspNetCore.Identity;
using AppFour.Models.Entrance;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AppFour.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class CollectionController : Controller
    {    
        private readonly AppDbContext Context;
        private readonly UserManager<User> userManager;
        public CollectionController(AppDbContext context, UserManager<User> _userManager)
        {
            userManager = _userManager;
            Context = context;
        }

        public IActionResult CollectionsManager(string button, string[] idsArr, string collId)
        {
            if (idsArr.Length != 0)
            {
                if (button.Equals("Delete selected"))
                    return RedirectToAction("Delete", "Collection", new { ids = idsArr });
            }
            else
            {
                if (button.Equals("Update"))
                    return RedirectToAction("Update", "Collection", new { collectionID = collId });
            }
            return RedirectToAction("MyCollections", "Collection");
        }

        public IActionResult MyCollections()
        {
            List<Collection> collectionsView = new List<Collection>();
            List<Collection> collection = Context.Collections.ToList();
            foreach (var it in collection)
            {
                if (it.UserId == userManager.GetUserId(HttpContext.User))
                {
                    it.Description = Markdown.Parse(it.Description);
                    collectionsView.Add(it);
                }
            }
            return View("CollectionsManager", collectionsView);
        }

        public IActionResult AllCollections()
        {
            List<Collection> collectionsView = Context.Collections.ToList();
            foreach (var it in collectionsView)
            {
                it.Description = Markdown.Parse(it.Description);
            }
            return View("CollectionsManager", collectionsView);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CollectionAction collection)
        {
            collection.UserId = userManager.GetUserId(HttpContext.User);
            Context.Collections.Add(CollectionProcessing.GetCollection(collection));
            Context.SaveChanges();
            return RedirectToAction("MyCollections", "Collection");
        }

        public IActionResult Update(string collectionID)
        {
            return View(CollectionProcessing.GetCollectionOnView(Context.Collections.Find(collectionID)));
        }

        public IActionResult Update1(CollectionAction collection)
        {
            collection.UserId = userManager.GetUserId(HttpContext.User);
            Context.Collections.Update(CollectionProcessing.GetCollection(collection));
            Context.SaveChanges();
            return RedirectToAction("MyCollections", "Collection");
        }

        public IActionResult Delete(string[] ids)
        {
            Cloudinary cloudinary = CloudAccount.Cloud();
            for (int i = 0; i < ids.Count(); i++)
            {
                Collection collection = Context.Collections.Find(ids[i]);
                cloudinary.DeleteResources(collection.ImageCloudId);
                Context.Collections.Remove(collection);
                Context.SaveChanges();
            }
            return RedirectToAction("MyCollections", "Collection");
        }
    }
}