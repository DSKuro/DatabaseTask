using DatabaseTask.Services.Excel.UnusedFiles.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Column = DocumentFormat.OpenXml.Spreadsheet.Column;

namespace DatabaseTask.Services.Excel
{
    public class ExcelUnusedPaths : IExcelUnusedPaths
    {
        private const string _unusedFiles = "Неиспользуемые файлы";
        private const string _unrecognisedFiles = "Неопознанные файлы";

        public void WriteUnusedPathsToExcel(List<string> unusedFiles, List<string> exceptFiles)
        {
            Create(unusedFiles, exceptFiles);
        }

        private void Create(List<string> unusedFiles, List<string> exceptFiles)
        {
            if (unusedFiles is null || !unusedFiles.Any())
            {
                return;
            }

            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xlsx");

            using var document = SpreadsheetDocument.Create(tempFile, SpreadsheetDocumentType.Workbook, true);

            var workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var sheets = workbookPart.Workbook.AppendChild(new Sheets());

            CreateSheet(workbookPart, sheets, unusedFiles, _unusedFiles, 1);

            if (exceptFiles is not null && exceptFiles.Any())
            {
                CreateSheet(workbookPart, sheets, exceptFiles, _unrecognisedFiles, 2);
            }

            workbookPart.Workbook.Save();

            Process.Start(new ProcessStartInfo(tempFile)
            {
                UseShellExecute = true
            });
        }

        private void CreateSheet(
                    WorkbookPart workbookPart,
                    Sheets sheets,
                    List<string> data,
                    string sheetName,
                    uint sheetId)
        {
            var wsPart = workbookPart.AddNewPart<WorksheetPart>();

            var sheetData = new SheetData();
            var worksheet = new Worksheet();

            var columns = new Columns();
            worksheet.Append(columns);
            worksheet.Append(sheetData);

            wsPart.Worksheet = worksheet;

            var sheet = new Sheet()
            {
                Id = workbookPart.GetIdOfPart(wsPart),
                SheetId = sheetId,
                Name = sheetName
            };
            sheets.Append(sheet);

            uint rowIndex = 1;
            int maxLength = 0;

            foreach (var item in data)
            {
                var row = new Row() { RowIndex = rowIndex };

                var cell = new Cell
                {
                    CellValue = new CellValue(item),
                    DataType = CellValues.String
                };

                row.Append(cell);
                sheetData.Append(row);

                if (item.Length > maxLength)
                    maxLength = item.Length;

                rowIndex++;
            }

            double width = (maxLength + 2) * 1.2;

            columns.Append(new Column()
            {
                Min = 1,
                Max = 1,
                Width = width,
                CustomWidth = true
            });
        }
    }
}
