using System;
using System.Collections.Generic;

namespace JsonToTable.Data
{
    public class Column
    {
        public string Key { get; set; }

        public List<string> Data { get; set; }


        public Column()
        {
            Data = new List<string>();
        }
    }
}
