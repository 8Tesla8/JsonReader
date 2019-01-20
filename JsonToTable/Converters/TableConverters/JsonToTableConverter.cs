using System;
using System.Text;
using JsonToTable.Data;

namespace JsonToTable.Converters.TableConverters
{
    public abstract class JsonToTableConverter : IConverter<Table, string[]>
    {  
        public abstract Table Convert(string[] data);

        protected string CreateOneLine(string[] data)
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
