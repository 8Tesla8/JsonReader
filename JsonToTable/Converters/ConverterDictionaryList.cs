using System;
using System.Collections.Generic;
using System.Text;

namespace JsonToTable.Converters
{
    public class ConverterDictionaryList : IConverter<string, Dictionary<string, List<string>>>
    {
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
