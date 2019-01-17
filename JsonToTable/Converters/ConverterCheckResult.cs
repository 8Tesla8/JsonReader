using System;
using System.Collections.Generic;
using System.Text;

namespace JsonToTable.Converters
{
    public class ConverterCheckResult : IConverter<string, List<CheckResult>>
    {
        public string Convert(List<CheckResult> data)
        {
            var result = string.Empty;

            result += "\nFiltration result";
            foreach (var res in data)
            {
                if (res.KeyWas)
                {
                    result += "Filter: \n" + res.Key + "\t";

                    foreach (var filter in res.Filters)
                        result += filter + " ";

                    result += "\nIndexes: ";
                    foreach (var index in res.Indexes)
                        result += index + " ";
                }
            }
            result += "\n";

            return result;
        }
    }
}
