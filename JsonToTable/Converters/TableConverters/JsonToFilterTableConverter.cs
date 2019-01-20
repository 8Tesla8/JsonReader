using System;
using System.Collections.Generic;
using JsonToTable.Data;
using JsonToTable.Desirializers;

namespace JsonToTable.Converters.TableConverters
{
    public class JsonToFilterTableConverter : JsonToTableConverter
    {
        protected readonly IDeserializerItem<Dictionary<string, List<string>>> _deserializer;

        public JsonToFilterTableConverter()
        {
            _deserializer = new DictionaryListDeserializer();
        }


        public override Table Convert(string[] data)
        {
            var str = CreateOneLine(data);

            if (string.IsNullOrEmpty(str))
                return new Table();

            var filterDictionary = _deserializer.DeserializeItem(str);

            var filterTable = new Table();
            foreach (var dict in filterDictionary)
            {
                var foundColumn = filterTable.Columns.Find(c => c.Key == dict.Key);
                if (foundColumn == null)
                {
                    foundColumn = new Column();
                    foundColumn.Key = dict.Key;

                    filterTable.Columns.Add(foundColumn);
                }

                foreach (var item in dict.Value)
                {
                    foundColumn.Data.Add(item);
                }
            }
            return filterTable;
        }
    }
}
