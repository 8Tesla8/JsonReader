using JsonToTable.Desirializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonToTable.Converters
{
    public class JsonConverter : IConverter<Dictionary<string, List<string>>, string[]>
    {
        protected readonly IDeserializer<Dictionary<string, string>> _deserializer;

        public JsonConverter()
        {
            _deserializer = new DictionaryDeserializer();
        }

        public Dictionary<string, List<string>> Convert(string[] data)
        {
            var str = CreateOneLine(data);

            var dictionaries = new List<Dictionary<string, string>>();

            if (str.StartsWith('['))
            {
                dictionaries = _deserializer.DeserializeArray(str);
            }
            else
            {
                if (str.Count(f => f == '}') > 1)
                {

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


        private string CreateOneLine(string[] data)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in data)
            {
                builder.Append(value);
            }
            return builder.ToString().TrimStart().TrimEnd();
        }

    }
}
