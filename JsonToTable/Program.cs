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
            Console.WriteLine("Hello World!");

            while (true)
            {

                //var path 

                var reader = new FileReader();


                //create filtration 
                var converter = new Converters.Converter();
                var data = converter.Convert(@"{ ""Name"":""Apple"", 
            ""Price"":""12.3"" }  
            ,
             { ""Name"":""Grape"", ""Price"":""3.21"" }");

                var str = converter.Convert(data);

                Console.WriteLine("\n" + str);

                Console.WriteLine("\nEnter space and press Enter to continue or just Enter to exit");
                var input = Console.ReadLine();
                if (!input.Equals(" ", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }

}
