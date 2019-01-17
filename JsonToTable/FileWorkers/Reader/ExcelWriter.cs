using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using JsonToTable.Converters;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace JsonToTable.FileWorkers.Reader
{
    public class DataModel 
    {
        public Dictionary<string, List<string>> Data;
        public Dictionary<string, List<string>> Filters; //set filters

        public DataModel()
        {
            Filters = new Dictionary<string, List<string>>();

            var list = new List<string>();
            list.Add("2");
            list.Add("4");

            Filters.Add("id", list);
        }
    }


    public class ExcelWriter : IWriter<DataModel>
    {
        public void Write(string path, DataModel model)
        {
            var data = model.Data;

            using (ExcelPackage excel = new ExcelPackage())
            {
                var newFile = new FileInfo(path);
                var worksheet = excel.Workbook.Worksheets.Add("Result");

                //titles
                var indexAlphabet = 1;
                foreach (var key in data.Keys)
                {
                    worksheet.Cells[GetLetter(indexAlphabet) + 1].Value = key;  
                    indexAlphabet++;
                }


                for (int i = 1; i < data.Keys.Count +1; i++)
                    worksheet.Column(i).Width = 20;  


                //set data
                indexAlphabet = 1;
                foreach (var dict in data)
                {
                    for (int i = 0, excelIndex = 2; i < dict.Value.Count; i++, excelIndex++)
                    {
                        var l = GetLetter(indexAlphabet);
                        worksheet.Cells[GetLetter(indexAlphabet) + excelIndex].Value = dict.Value[i];

                        //check 
                        if(model.Filters.ContainsKey(dict.Key))
                        {
                            if (!model.Filters[dict.Key].Any(s => s.Equals(dict.Value[i])))
                            {
                                worksheet.Cells[GetLetter(indexAlphabet) + excelIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[GetLetter(indexAlphabet) + excelIndex].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                            }
                        }

                    }

                    indexAlphabet++;
                }


                excel.SaveAs(newFile);
            }
        }


        private string GetLetter(int index)
        {
            string columnString = "";
            decimal columnNumber = index;
            while (columnNumber > 0)
            {
                decimal currentLetterNumber = (columnNumber - 1) % 26;
                char currentLetter = (char)(currentLetterNumber + 65);
                columnString = currentLetter + columnString;
                columnNumber = (columnNumber - (currentLetterNumber + 1)) / 26;
            }
            return columnString;
        }
    }
}
