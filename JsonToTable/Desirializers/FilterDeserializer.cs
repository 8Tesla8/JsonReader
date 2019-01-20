using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JsonToTable.Desirializers
{
    public class FilterDeserializer : IDeserializerItem<Dictionary<string, List<string>>>
    {
        public Dictionary<string, List<string>> DeserializeItem(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);
        }
    }
}
