using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonToTable.Converters
{
    public class CheckResult 
    {
        public string Key;
        public bool KeyWas;

        public List<string> Filters;
        public List<int> Indexes;


        public CheckResult()
        {
            Key = string.Empty;
            KeyWas = false;

            Indexes = new List<int>();
            Filters = new List<string>();
        }
    }

    // TODO create interface , change location of file 
    public class DataChecker
    {
        public DataChecker()
        {

        }
        // TODO read filtration


        public List<CheckResult> Check(Dictionary<string, List<string>> data) {
            var dataForCheck = new  Dictionary<string, List<string>> ();
            var list = new List<string>();
            list.Add("12");
            list.Add("12.3");
            
            dataForCheck.Add("Price", list);


            var result = new List<CheckResult>();

            if (dataForCheck.Count == 0)
                return result;


            foreach (var dict in dataForCheck)
            {
                var checkResult = new CheckResult();
                checkResult.Key = dict.Key;
                checkResult.Filters = dict.Value;

                result.Add(checkResult);


                if (data.ContainsKey(checkResult.Key))
                {
                    for (int i = 0; i < data[checkResult.Key].Count; i++)
                    {
                        //if current item data[checkResult.Key][i] is not fit to any filters
                        if (!checkResult.Filters.Any(s => s.Equals(data[checkResult.Key][i])))
                        {
                            checkResult.KeyWas = true;
                            checkResult.Indexes.Add(i);
                        }

                        //foreach (var filter in checkResult.Filters) 
                        //{
                        //    // find the same
                        //    //if (data[dict.Key][i].Equals(filter)) 
                        //    //{
                        //    //    var found = result.Find(o => o.Key.Equals(dict.Key));
                        //    //    found.KeyWas = true;
                        //    //    found.Indexes.Add(i);
                        //    //}
                        //}
                    }

                }
            }

            return result;
        }

     
    }
}
