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
using Microsoft.AspNetCore.Http;
using AppFour.Models.Fields;
using System.Threading.Tasks;

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

        public async Task<IActionResult> MyCollections()
        {
            List<Collection> collectionsView = new List<Collection>();
            List<Collection> collection = await Context.Collections.ToListAsync();
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

        public async Task<IActionResult> AllCollections()
        {
            List<Collection> collectionsView = await Context.Collections.ToListAsync();
            foreach (var it in collectionsView)
            {
                it.Description = Markdown.Parse(it.Description);
            }
            return View("CollectionsManager", collectionsView);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(DataForm AllData)
        {
            CollectionAction collectionAct = new CollectionAction(AllData.Title, AllData.Description, AllData.Topic, AllData.Image,userManager.GetUserId(HttpContext.User));
            Collection collection = CollectionProcessing.CollectionProcess(collectionAct);
            if (AllData.AdditionalFields != "[]")
            {
                List<CustomField> fields = FieldProcessing.FieldsProcess(AllData.AdditionalFields, collection.Id);
                for (int i = 0; i < fields.Count; i++)
                {
                    await Context.CustomFields.AddAsync(fields[i]);
                }
            }
            await Context.Collections.AddAsync(collection);
            await Context.SaveChangesAsync();
            return RedirectToAction("MyCollections", "Collection");
        }

        public IActionResult Update(string collectionID)
        {
            return View(CollectionProcessing.GetCollectionOnView(Context.Collections.Find(collectionID)));
        }

        public IActionResult Update1(CollectionAction collection)
        {
            Context.Collections.Update(CollectionProcessing.CollectionProcess(collection));
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