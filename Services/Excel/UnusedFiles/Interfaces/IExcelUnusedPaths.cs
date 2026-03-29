using System.Collections.Generic;

namespace DatabaseTask.Services.Excel.UnusedFiles.Interfaces
{
    public interface IExcelUnusedPaths
    {
        public void WriteUnusedPathsToExcel(List<string> unusedFiles, List<string> exceptFiles);
    }
}
