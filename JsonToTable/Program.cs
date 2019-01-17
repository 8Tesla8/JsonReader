using JsonToTable.Converters;
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
            Console.WriteLine("Hello World! \n");

            Console.WriteLine("Current path: ");
            Console.WriteLine(PathDefinder.Path + "\n");

            Console.WriteLine("Write path to data.txt file or press enter to use current path: ");
            var path = Console.ReadLine();
            if (!string.IsNullOrEmpty(path)) 
                PathDefinder.Path = path;


            var reader = new FileReader();

            var excelWriter = new ExcelWriter();

            var converterJson = new ConverterJSON();
            var converterDictionaryList = new ConverterDictionaryList();
            var converterCheckResult = new ConverterCheckResult();

            var dataChecker = new DataChecker();

            while (true)
            {

                var convertedData = converterJson.Convert(reader.Read(PathDefinder.Path + "/data.txt"));

                var resultStr = converterDictionaryList.Convert(convertedData);
                reader.Write(PathDefinder.Path + "/result.txt", resultStr);

                excelWriter.Write(PathDefinder.Path + "/result.xlsx", convertedData);

                Console.WriteLine("\n" + resultStr);


                //check
                var checkResults = dataChecker.Check(convertedData);
                var checkResultsStr = converterCheckResult.Convert(checkResults);

                Console.WriteLine(checkResultsStr);


                Console.WriteLine("\n\nEnter space and press Enter to continue or just Enter to exit");
                var input = Console.ReadLine();
                if (!input.Equals(" ", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }

}
