using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace DatabaseTask.Services.Excel.Writer
{
    public class ExcelWriter : IExcelWriter
    {
        public void Create(string filePrefix, params ExcelSheetData[] sheetsData)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), $"{filePrefix}{Guid.NewGuid()}.xlsx");

            using (var document = SpreadsheetDocument.Create(tempFile, SpreadsheetDocumentType.Workbook, true)) 
            {
                var workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());

                uint sheetId = 1;

                foreach (var sheetData in sheetsData)
                {
                    CreateSheet(
                        workbookPart,
                        sheets,
                        sheetData.Data,
                        sheetData.Name,
                        sheetId++);
                }

                workbookPart.Workbook.Save();
            }

            Process.Start(new ProcessStartInfo(tempFile)
            {
                UseShellExecute = true
            });
        }

        private void CreateSheet(
            WorkbookPart workbookPart,
            Sheets sheets,
            IEnumerable<string> data,
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

            sheets.Append(new Sheet
            {
                Id = workbookPart.GetIdOfPart(wsPart),
                SheetId = sheetId,
                Name = sheetName
            });

            uint rowIndex = 1;
            int maxLength = 0;

            foreach (var item in data)
            {
                var row = new Row
                {
                    RowIndex = rowIndex++
                };

                row.Append(new Cell
                {
                    CellValue = new CellValue(item),
                    DataType = CellValues.String
                });

                sheetData.Append(row);

                maxLength = Math.Max(maxLength, item.Length);
            }

            columns.Append(new Column
            {
                Min = 1,
                Max = 1,
                Width = (maxLength + 2) * 1.2,
                CustomWidth = true
            });
        }
    }
}
