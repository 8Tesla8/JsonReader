using System;
using System.Collections.Generic;

namespace JsonToTable.Data
{
    public class Table
    {
        public List<Column> Columns { get; set; }

        public Table()
        {
            Columns = new List<Column>();
        }

        public Column GetColumn(string key) {
            return Columns.Find(c => c.Key.Equals(key));
        }
    }
}
