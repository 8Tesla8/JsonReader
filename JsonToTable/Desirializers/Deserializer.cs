using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonToTable.Desirializers
{
    public class Deserializer : IDeserializer <Dictionary<string, string>>
    {
        public List<Dictionary<string, string>> DeserializeArray(string data)
        {
            return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);

            //example
            //foreach (Dictionary<string, string> lst in result)
            //{
            //    Console.WriteLine("--NewObject--");
            //    foreach (KeyValuePair<string, string> item in lst)
            //    {
            //        Console.WriteLine(string.Format("Key: {0} Value: {1}", item.Key, item.Value));
            //    }
            //}
        }

        public dynamic DeserializeDynamic(string data)
        {
            return JArray.Parse(data);

            //string jsonExample = @"
            //[
            //  {
            //    'Title': 'Json.NET is awesome!',
            //    'Author': {
            //      'Name': 'James Newton-King',
            //      'Twitter': '@JamesNK',
            //      'Picture': '/jamesnk.png'
            //    },
            //    'Date': '2013-01-23T19:30:00',
            //    'BodyHtml': '&lt;h3&gt;Title!&lt;/h3&gt;\r\n&lt;p&gt;Content!&lt;/p&gt;'
            //  }
            //]";      
        }

        public Dictionary<string, string> DeserializeItem(string data)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            //string jsonExample = @"{""key1"":""value1"",""key2"":""value2""}"; 
        }
    }
}
