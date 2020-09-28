using AppFour.Models.Fields;
using System.Collections.Generic;
using System.Linq;

namespace AppFour.Models.Item
{
    public class ItemAction
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CollectionId { get; set; }

        public static IEnumerable<CustomField> ItemFields { get; set; }
        public static IQueryable<FieldData> ItemData { get; set; }
        public static string[] DataId { get; set; }
        public List<Item> items;
        public ItemAction(List<Item> collectionItems, string collectionID)
        {
            items = collectionItems;
            CollectionId = collectionID;
        }

        public ItemAction(string collectionID, IEnumerable<CustomField> InputFields)
        {
            ItemFields = InputFields;
            CollectionId = collectionID;
        }

        public ItemAction(){}
    }
}
