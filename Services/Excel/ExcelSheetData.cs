using System;
using System.Collections.Generic;

namespace DatabaseTask.Services.Excel
{
    public class ExcelSheetData
    {
        public string Name 
        {
            get; 
        }

        public IEnumerable<string> Data 
        {
            get; 
        }

        public ExcelSheetData(string name, IEnumerable<string> data)
        {
            Name = name;
            Data = data;
        }
    }
}
