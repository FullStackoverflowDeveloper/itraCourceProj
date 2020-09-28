using System;
using System.Linq;
using AppFour.Cotexts;
using AppFour.Models.Fields;
using AppFour.Models.Item;
using Microsoft.EntityFrameworkCore;

namespace AppFour.Globals
{
    public static class ItemProcessing
    {
        private static Item itemResult;

        public static string ItemProcess(ItemAction item, string[] DataFieldId, string[] DataValue, AppDbContext Context)
        {
                itemResult = new Item
                    {
                        Id = item.Id ?? Guid.NewGuid().ToString(),
                        Title = item.Title,
                        CollectionId = item.CollectionId
                    };
                if (item.Id == null)
                {
                    Context.Items.Add(itemResult);
                }
                else
                {
                    Context.Items.Update(itemResult);
                }
                for (int i = 0; i < DataValue.Length; i++)
                {
                    FieldData fd = new FieldData()
                    {
                        Id = ItemAction.DataId[i] ?? Guid.NewGuid().ToString(),
                        ItemId = itemResult.Id,
                        FieldId = DataFieldId[i],
                        Value = DataValue[i]
                    };
                    if (item.Id == null)
                    {
                        Context.FieldsData.Add(fd);
                    }
                    else
                    {
                        Context.FieldsData.Update(fd);
                    }
                }
                Context.SaveChanges();
            return itemResult.CollectionId;
        }

        public static ItemAction GetItem(string itemId, AppDbContext Context)
        {
            Item item = Context.Items.Find(itemId);
            ItemAction itemResult = new ItemAction
            {
                Id = item.Id,
                Title = item.Title,
                CollectionId = item.CollectionId
            };
            ItemAction.ItemFields = Context.CustomFields.Include(f => f.Collection).Where(c => c.CollectionId == item.CollectionId);
            IQueryable<FieldData> itemData = Context.FieldsData.Include(d=>d.Item).Where(i=>i.ItemId == itemId);
            ItemAction.ItemData = itemData.Include(f => f.Field);

            return itemResult;
        }
    }
}