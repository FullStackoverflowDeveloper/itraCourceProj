using System;
using AppFour.Models.Item;

namespace AppFour.Globals
{
    public static class ItemProcessing
    {
        public static Item itemResult;

        public static Item GetItem(ItemAction item)
        {
            itemResult = ItemProcess(item);
            return itemResult;
        }

        public static Item ItemProcess(ItemAction item)
        {
            itemResult = new Item
            {
                Id = item.Id ?? Guid.NewGuid().ToString(),
                Title = item.Title,
                CollectionId = item.CollectionId
            };
            return itemResult;
        }
    }
}
