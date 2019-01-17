using System;
using System.Collections.Generic;

namespace JsonToTable.Desirializers
{
    public interface IDeserializer
    {
        List<Dictionary<string, string>> DeserializeArray(string data);

        Dictionary<string, string> DeserializeItem(string data);
     
        dynamic DeserializeDynamic(string data);
    }
}
