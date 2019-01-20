using System;
using System.Collections.Generic;
using System.Linq;
using JsonToTable.Data;

namespace JsonToTable.Chekers
{
    public class TableCheker : ICheker<Table>
    {

        public Table Check(Table data, Table filter) 
        {
            var notFeetIndexes = new List<int>();

            // check
            foreach (var column in data.Columns)
            {
                for (int i = 0; i < column.Data.Count; i++)
                {
                    //check 
                    var filterColumn = filter.GetColumn(column.Key);

                    if (filterColumn != null)
                    {
                        if (!filterColumn.Data.Any(d => d.Equals(column.Data[i])))
                        {
                            notFeetIndexes.Add(i);
                        }
                    }
                }
            }

            // data which data did not passed, according ro filtes
            var result = new Table();
            foreach (var column in data.Columns)
            {
                var resColumn = new Column();
                resColumn.Key = column.Key;
                result.Columns.Add(resColumn);

                for (int i = 0; i < notFeetIndexes.Count; i++)
                {
                    var index = notFeetIndexes[i];
                    var dat = column.Data[index];


                    resColumn.Data.Add(column.Data[notFeetIndexes[i]]);
                }
            }

            return result;
        }
    }
}
