using System;
using System.Collections.Generic;
using System.Text;
using JsonToTable.Data;
using JsonToTable.Desirializers;

namespace JsonToTable.Converters
{
    public class ConverterJsonToFilterTable : IConverter<Table, string[]>
    {
        protected readonly IDeserializerItem<Dictionary<string, List<string>>> _deserializer;

        public ConverterJsonToFilterTable()
        {
            _deserializer = new FilterDeserializer();
        }


        public Table Convert(string[] data)
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


        // TODO put method somewhere for ConverterJsonToDataTable
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
