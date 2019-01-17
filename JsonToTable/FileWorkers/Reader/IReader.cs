using System;
using System.IO;
using System.Text;

namespace JsonToTable.FileWorkers.Reader
{

    public interface IWriter<T> 
    {
        void Write(string path, T data);
    }


    public interface IReader : IWriter<string>
    {
        string[] Read(string path);
    }


    public class FileReader : IReader
    {
        public string[] Read(string path)
        {
            return File.ReadAllLines(path);
        }

        public void Write(string path, string data)
        {
            using (FileStream fs = File.Create(path))
            {
                fs.Write(new UTF8Encoding(true).GetBytes(data), 0, data.Length);
            }
        }
    }
}
