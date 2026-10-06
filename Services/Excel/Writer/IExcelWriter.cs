namespace DatabaseTask.Services.Excel.Writer
{
    public interface IExcelWriter
    {
        public void Create(string filePrefix, params ExcelSheetData[] sheetsData);
    }
}
