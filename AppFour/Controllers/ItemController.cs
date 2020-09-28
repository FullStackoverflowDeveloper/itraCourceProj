using System.Collections.Generic;
using System.Linq;
using AppFour.Cotexts;
using AppFour.Globals;
using AppFour.Models.Fields;
using AppFour.Models.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return RedirectToAction("GetCollectionItems", "Item");
        }

        public IActionResult GetCollectionItems(string collectionID)
        {
            List<Item> collectionItems = new List<Item>();
            foreach (var item in Context.Items.ToList())
            {
                if (item.CollectionId == collectionID) {
                    collectionItems.Add(item); 
                }
            }
            ItemAction itemsView = new ItemAction(collectionItems, collectionID);
            return View("ItemsManager", itemsView);
        }
        
        public IActionResult GetAllItems()
        {
            List<Item> items = Context.Items.ToList();
            return View("ItemsManager", items);
        }

        public IActionResult Create(string collectionID)
        {
            IEnumerable<CustomField> ItemFields = Context.CustomFields.Include(f => f.Collection).Where(c => c.CollectionId == collectionID);
            ItemAction item = new ItemAction(collectionID, ItemFields);
            return View("Create", item);
        }

        public IActionResult Create1(ItemAction item, string[] DataFieldId, string[] DataValue)
        {
            string collectionID = ItemProcessing.ItemProcess(item, DataFieldId, DataValue, Context);
            return RedirectToAction("GetCollectionItems", "Item", new { collectionID });
        }

        public IActionResult Delete(string[] ids, string collectionID)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                Item item = Context.Items.Find(ids[i]);
                Context.Items.Remove(item);
                Context.SaveChanges();
            }
            return RedirectToAction("GetCollectionItems", "Item", new { collectionID });
        }

        public IActionResult Update(string itemId)
        {
            ItemAction itemUpdate = ItemProcessing.GetItem(itemId, Context);
            
            return View(itemUpdate);
        }

        public IActionResult Update1(ItemAction item, string[] DataFieldId, string[] DataValue, string[] DataId)
        {
            ItemAction.DataId = DataId;
            string collectionID = ItemProcessing.ItemProcess(item, DataFieldId, DataValue, Context);
            return RedirectToAction("GetCollectionItems", "Item", new { collectionID });
        }
    }
}
