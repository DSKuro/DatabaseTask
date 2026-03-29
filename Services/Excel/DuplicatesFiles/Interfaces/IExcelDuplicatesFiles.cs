using DatabaseTask.ViewModels.Analyses.Models;
using System.Collections.Generic;

namespace DatabaseTask.Services.Excel.DuplicatesFiles.Interfaces
{
    public interface IExcelDuplicatesFiles
    {
        public void WriteDuplicatesToExcel(IEnumerable<DuplicatesFilesItemViewModel> data);
    }
}
