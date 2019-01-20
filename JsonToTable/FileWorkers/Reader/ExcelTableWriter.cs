using System;
using System.Drawing;
using System.IO;
using System.Linq;
using JsonToTable.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace JsonToTable.FileWorkers.Reader
{
    public class ExcelModel 
    {
        public Table Data;
        public Table Filters; 
    }


    public class ExcelTableWriter : IWriter<ExcelModel>
    { 
        public void Write(string path, ExcelModel model)
        {
            var dataTable = model.Data;
            var filteTable = model.Filters;


            using (ExcelPackage excel = new ExcelPackage())
            {
                var newFile = new FileInfo(path);
                var worksheet = excel.Workbook.Worksheets.Add("Result");

                // header titles
                var indexAlphabet = 1;

                foreach (var column in dataTable.Columns)
                {
                    worksheet.Cells[GetLetter(indexAlphabet) + 1].Value = column.Key;
                    indexAlphabet++;
                }

                for (int i = 1; i < dataTable.Columns.Count + 1; i++)
                    worksheet.Column(i).Width = 20;


                // write data 
                indexAlphabet = 1;
                foreach (var column in dataTable.Columns)
                {
                    for (int i = 0, excelIndex = 2; i < column.Data.Count; i++, excelIndex++)
                    {
                        var excelLetter = GetLetter(indexAlphabet);
                        worksheet.Cells[excelLetter + excelIndex].Value = column.Data[i];

                        //check 
                        var filterColumn = filteTable.GetColumn(column.Key);

                        if(filterColumn != null) 
                        {
                            if(!filterColumn.Data.Any(d => d.Equals(column.Data[i]))) 
                            {
                                worksheet.Cells[excelLetter + excelIndex].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[excelLetter + excelIndex].Style.Fill.BackgroundColor.SetColor(Color.Orange);
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
