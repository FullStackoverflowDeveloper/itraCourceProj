using System.Collections.Generic;

namespace AppFour.Models.Item
{
    public class ItemAction
    {
        public List<Item> items;
        public string Id { get; set; }
        public string Title { get; set; }
        public string CollectionId { get; set; }

        public string CollectionID { get; set; }
        public ItemAction(List<Item> items, string collectionID)
        {
            this.items = items;
            CollectionID = collectionID;
        }
        public ItemAction(string collectionID)
        {
            CollectionId = collectionID;
        }
        public ItemAction()
        {
        }
    }
}
