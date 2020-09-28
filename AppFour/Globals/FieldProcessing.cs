using AppFour.Models.Fields;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppFour.Globals
{
    public class FieldProcessing
    {
        private static readonly List<CustomField> FieldsResult = new List<CustomField>();
        public static List<CustomField> FieldsProcess(string AdditionalFields, string collectionID)
        {
            List<AddFields> fields = JsonConvert.DeserializeObject<List<AddFields>>(AdditionalFields);
            for(int i = 0; i < fields.Count; i++)
            {
                CustomField cf = new CustomField(collectionID, fields[i].Name, fields[i].Type);
                FieldsResult.Add(cf);
            }
            return FieldsResult;
        }
    }
}
