using System;
using System.Collections.Generic;
using System.Linq;
using JsonToTable.Desirializers;

namespace JsonToTable.Converters
{
    public interface IConverter
    {
        Dictionary<string, List<string>> Convert(string data);
        Dictionary<string, List<string>> Convert(string[] data);
        
        string Convert(Dictionary<string, List<string>> data);
    }


    public class Converter : IConverter
    {
        protected readonly IDeserializer _deserializer;

        public Converter()
        {
            _deserializer = new Deserializer();
        }

        public Dictionary<string, List<string>> Convert(string[] data) 
        {
            var str = string.Empty; 
            foreach (var line in data)
              str += line;

            return Convert(str);
        }

        public Dictionary<string, List<string>> Convert(string data)
        {
            var str = data.TrimStart().TrimEnd();

            var dictionaries = new List<Dictionary<string, string>>();

            if (str.StartsWith('['))
            {
                dictionaries = _deserializer.DeserializeArray(data);
            }
            else
            {
                if(str.Count(f => f == '}') > 1) {

                    var items = new List<string>();
                    foreach (var item in str.Split('}'))
                    {
                        if (string.IsNullOrEmpty(item)) 
                            continue;

                        var trimedItem = item.TrimStart().TrimEnd();

                        if (trimedItem.StartsWith('{')) 
                        {
                            items.Add(trimedItem + "}");
                        }
                        else 
                        {
                            items.Add(
                                trimedItem.Substring(trimedItem.IndexOf('{')) + "}");
                        }
                    }

                    foreach (var item in items)
                    {
                        dictionaries.Add(_deserializer.DeserializeItem(item));
                    }
                }

            }


            var result = new Dictionary<string, List<string>>();
            foreach (var listItem in dictionaries)
            {
                foreach (var dictItem in listItem)
                {
                    if (result.ContainsKey(dictItem.Key))
                    {
                        result[dictItem.Key].Add(dictItem.Value);
                    }
                    else
                    {
                        var value = new List<string>();
                        value.Add(dictItem.Value);

                        result.Add(dictItem.Key, value);
                    }
                }
            }

            return result;
        }


        public string Convert(Dictionary<string, List<string>> data)
        {
            var result = "index";

            for (int i = 0; i < data.Count; i++)
            {
                result += "\t" + i;
            }

            foreach (var dict in data)
            {
                result += "\n" + dict.Key;

                foreach (var item in dict.Value)
                {
                    result += "\t" + item;
                }
            }

            return result;
        }
    }
}
