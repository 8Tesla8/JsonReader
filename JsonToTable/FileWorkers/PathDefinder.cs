using System;
namespace JsonToTable.FileWorkers
{
    public static class PathDefinder
    {
        public static string Path { get; set; }

        static PathDefinder()
        {
            Path = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void SetCurrentPath () 
        {
            Path = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
