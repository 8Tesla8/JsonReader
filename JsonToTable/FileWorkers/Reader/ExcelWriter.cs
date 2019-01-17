using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace JsonToTable.FileWorkers.Reader
{
    public class ExcelWriter : IWriter<Dictionary<string, List<string>>>
    {

        private char[] _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public void Write(string path, Dictionary<string, List<string>> data)
        {

            using (ExcelPackage excel = new ExcelPackage())
            {
                var newFile = new FileInfo(path);
                var worksheet = excel.Workbook.Worksheets.Add("Result");

                //titles
                var indexAlphabet = 0;
                foreach (var key in data.Keys)
                {
                    worksheet.Cells[_alphabet[indexAlphabet] + 1.ToString()].Value = key;  
                    indexAlphabet++;
                }


                for (int i = 1; i < data.Keys.Count +1; i++)
                    worksheet.Column(i).Width = 20;  


                //set data
                indexAlphabet = 0;
                foreach (var dict in data)
                {
                    char letter = _alphabet[indexAlphabet];

                    for (int i = 0, excelIndex = 2; i < dict.Value.Count; i++, excelIndex++)
                    {
                        worksheet.Cells[letter + excelIndex.ToString()].Value = dict.Value[i];
                    }

                    indexAlphabet++;
                }




                //worksheet.Cells.AutoFitColumns(minWidth, maxWidth);

                //worksheet.Cells["A1"].LoadFromDataTable(TheDataTable, true);
                //worksheet.Cells["F4"].BackgroundColor.SetColor(Color.Red);

                excel.SaveAs(newFile);
            }
        }
    }
}
