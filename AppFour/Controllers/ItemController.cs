using System.Collections.Generic;
using System.Linq;
using AppFour.Cotexts;
using AppFour.Globals;
using AppFour.Models.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppFour.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ItemController : Controller
    {
        private readonly AppDbContext Context;

        public ItemController(AppDbContext context)
        {
            Context = context;
        }

        public IActionResult ItemsManager(string button, string[] idsArr, string collectionID)
        {
            if (idsArr.Length != 0)
            {
                if (button.Equals("Delete selected"))
                    return RedirectToAction("Delete", "Item", new { ids = idsArr, collectionID });
            }
            else
            {
                if (button.Equals("Update"))
                    return RedirectToAction("Update", "Item", new { collectionID });
            }
            return RedirectToAction("GetItems", "Item");
        }

        public IActionResult GetItems(string collectionID)
        {
            List<Item> itemsById = new List<Item>();
            foreach (var item in Context.Items.ToList())
            {
                if (item.CollectionId == collectionID)
                    itemsById.Add(item);
            }
            ItemAction itemsView = new ItemAction(itemsById, collectionID);
            return View("ItemsManager", itemsView);
        }

        public IActionResult GetAllItems()
        {
            List<Item> items = Context.Items.ToList();
            return View("ItemsManager", items);
        }

        public IActionResult Create(string collectionID)
        {
            ItemAction item = new ItemAction(collectionID);
            return View("Create", item);
        }

        public IActionResult Create1(ItemAction item)
        {
            Context.Items.Add(ItemProcessing.GetItem(item));
            Context.SaveChanges();
            return RedirectToAction("GetItems", "Item", new { collectionID = item.CollectionId });
        }

        public IActionResult Delete(string[] ids, string collectionID)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                Item item = Context.Items.Find(ids[i]);
                Context.Items.Remove(item);
                Context.SaveChanges();
            }
            return RedirectToAction("GetItems", "Item", new { collectionID });
        }

        public IActionResult Update(string itemID)
        {
            Item item = Context.Items.Find(itemID);
            ItemAction itemUpdate = new ItemAction
            {
                Id = item.Id,
                Title = item.Title,
                CollectionId = item.CollectionId
            };
            return View(itemUpdate);
        }
        public IActionResult Update1(ItemAction item)
        {
            Context.Items.Update(ItemProcessing.GetItem(item));
            Context.SaveChanges();
            return RedirectToAction("GetItems", "Item", new { item.CollectionId });
        }
    }
}
