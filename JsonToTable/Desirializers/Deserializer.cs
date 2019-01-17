﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonToTable.Desirializers
{
    public class Deserializer : IDeserializer
    {
        public List<Dictionary<string, string>> DeserializeArray(string data)
        {

            List<Dictionary<string, string>> result = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(data);

            //foreach (Dictionary<string, string> lst in result)
            //{
            //    Console.WriteLine("--NewObject--");
            //    foreach (KeyValuePair<string, string> item in lst)
            //    {
            //        Console.WriteLine(string.Format("Key: {0} Value: {1}", item.Key, item.Value));
            //    }
            //}

            return result;
        }

        public dynamic DeserializeDynamic(string data)
        {
//            string jsonExample = @"
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

            return JArray.Parse(data);
        }

        public Dictionary<string, string> DeserializeItem(string data)
        {
            //string jsonExample = @"{""key1"":""value1"",""key2"":""value2""}";

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
        }
    }
}
