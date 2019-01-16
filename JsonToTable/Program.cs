﻿using JsonToTable.FileWorkers;
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
            var converter = new Converters.Converter();
           
            while (true)
            {

                var convertedData = converter.Convert(reader.Read(PathDefinder.Path + "/data.txt"));

                var resultStr = converter.Convert(convertedData);
                reader.Write(PathDefinder.Path + "/result.txt", resultStr);


                Console.WriteLine("\n" + resultStr);


                Console.WriteLine("\nEnter space and press Enter to continue or just Enter to exit");
                var input = Console.ReadLine();
                if (!input.Equals(" ", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }

}
