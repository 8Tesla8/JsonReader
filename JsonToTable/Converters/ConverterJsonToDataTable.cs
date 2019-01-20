using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonToTable.Data;
using JsonToTable.Desirializers;

namespace JsonToTable.Converters
{
    public class ConverterJsonToDataTable : IConverter<Table, string[]>
    {
        protected readonly IDeserializer<Dictionary<string, string>> _deserializer;

        public ConverterJsonToDataTable()
        {
            _deserializer = new Deserializer();

        }


        public Table Convert(string[] data)
        {
            var str = CreateOneLine(data);

            if (string.IsNullOrEmpty(str))
                return new Table();

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


            var table = new Table();
            foreach (var listItem in dictionaries)
            {
                foreach (var dictItem in listItem)
                {
                    var found = table.Columns.Find(c => c.Key == dictItem.Key);
                    if (found == null)
                    {
                        var column = new Column();
                        column.Key = dictItem.Key;

                        column.Data.Add(dictItem.Value);
                        table.Columns.Add(column);
                    }
                    else
                    {
                        found.Data.Add(dictItem.Value);
                    }
                }
            }

            return table;
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
