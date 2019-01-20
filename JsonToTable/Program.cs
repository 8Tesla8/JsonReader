using JsonToTable.Chekers;
using JsonToTable.Converters;
using JsonToTable.Converters.TableConverters;
using JsonToTable.Data;
using JsonToTable.FileWorkers;
using JsonToTable.FileWorkers.Reader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JsonToTable
{
    class Program
    {
        static void Main(string[] args)
        {
            // improvements
            // write in console data which did not passed according to filter
            // create Http request and deseriaze response - read setting about request from file


            Console.WriteLine("Hello World! \n");

            try 
            {
                Start(); 
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }


        private static void Start() 
        {
            Console.WriteLine("Current path: ");
            Console.WriteLine(PathDefinder.Path + "\n");

            Console.WriteLine("Write path to data.txt file or press enter to use current path: ");
            var path = Console.ReadLine();
            if (!string.IsNullOrEmpty(path))
                PathDefinder.Path = path;


            var reader = new FileReader();
            var excelTableWriter = new ExcelTableWriter();

            var convJsonToDataTable = new JsonToDataTableConverter();
            var convJsonToFilterTable = new JsonToFilterTableConverter();


            while (true)
            {
                var exceModel = new ExcelModel();
                exceModel.Data = convJsonToDataTable.Convert(reader.Read(PathDefinder.Path + "/data.txt"));
                exceModel.Filters = convJsonToFilterTable.Convert(reader.Read(PathDefinder.Path + "/filter.txt"));


                // check data
                var checker = new TableCheker();
                var checkResult = checker.Check(exceModel.Data, exceModel.Filters);

                Console.WriteLine("Filter: ");
                Console.WriteLine(exceModel.Filters);

                Console.WriteLine("Data which not fit:");
                Console.WriteLine(checkResult);
                //


                // write results
                excelTableWriter.Write(PathDefinder.Path + "/result.xlsx", exceModel);
                reader.Write(PathDefinder.Path + "/result.txt", checkResult.ToString());
                //

                Console.WriteLine("\n\nEnter space and press Enter to continue or just Enter to exit");
                var input = Console.ReadLine();
                if (!input.Equals(" ", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }

}
