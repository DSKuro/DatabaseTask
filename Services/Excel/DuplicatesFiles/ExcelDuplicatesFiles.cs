using DatabaseTask.Services.Excel.DuplicatesFiles.Interfaces;
using DatabaseTask.ViewModels.Analyses.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.Excel.DuplicatesFiles
{
    public class ExcelDuplicatesFiles : IExcelDuplicatesFiles
    {
        private const string _filePrefix = "duplicates_";
        private const string _duplicatesFiles = "Дубликаты";

        public void WriteDuplicatesToExcel(IEnumerable<DuplicatesFilesItemViewModel> data)
        {
            if (data is null || !data.Any())
            {
                return;
            }

            string tempFile = Path.Combine(Path.GetTempPath(), $"{_filePrefix}{Guid.NewGuid()}.xlsx");

            using var document = SpreadsheetDocument.Create(tempFile, SpreadsheetDocumentType.Workbook, true);

            var workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var sheets = workbookPart.Workbook.AppendChild(new Sheets());
            var wsPart = workbookPart.AddNewPart<WorksheetPart>();

            var sheetData = new SheetData();
            var columns = new Columns();

            wsPart.Worksheet = new Worksheet(columns, sheetData);

            sheets.Append(new Sheet()
            {
                Id = workbookPart.GetIdOfPart(wsPart),
                SheetId = 1,
                Name = _duplicatesFiles
            });

            uint rowIndex = 1;

            sheetData.Append(CreateRow(rowIndex++, "Группа", "Путь", "В БД"));

            int maxGroup = "Группа".Length;
            int maxPath = "Путь".Length;
            int maxDb = "В БД".Length;

            foreach (var item in data)
            {
                string group = item.FileName ?? "";
                string path = item.Path ?? "";
                string dbValue = item.IsDB ? "Да" : "Нет";

                sheetData.Append(CreateRow(rowIndex++, group, path, dbValue));

                if (group.Length > maxGroup)
                {
                    maxGroup = group.Length;
                }

                if (path.Length > maxPath)
                {
                    maxPath = path.Length;
                }

                if (dbValue.Length > maxDb) 
                { 
                    maxDb = dbValue.Length; 
                }
            }

            columns.Append(
                CreateColumn(1, maxGroup),
                CreateColumn(2, maxPath),
                CreateColumn(3, maxDb)
            );

            workbookPart.Workbook.Save();

            Process.Start(new ProcessStartInfo(tempFile)
            {
                UseShellExecute = true
            });
        }

        private Row CreateRow(uint index, string col1, string col2, string col3)
        {
            var row = new Row() { RowIndex = index };

            row.Append(CreateCell(col1));
            row.Append(CreateCell(col2));
            row.Append(CreateCell(col3));

            return row;
        }

        private Cell CreateCell(string value)
        {
            return new Cell
            {
                CellValue = new CellValue(value),
                DataType = CellValues.String
            };
        }

        private Column CreateColumn(uint index, int maxLength)
        {
            double width = (maxLength + 2) * 1.2;

            width = Math.Min(width, 30);

            return new Column()
            {
                Min = index,
                Max = index,
                Width = width,
                CustomWidth = true
            };
        }
    }
}
