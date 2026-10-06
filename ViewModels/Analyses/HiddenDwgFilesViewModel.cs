using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.Excel;
using DatabaseTask.Services.Excel.Writer;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using System.Linq;

namespace DatabaseTask.ViewModels.Analyses
{
    public class HiddenDwgFilesViewModel : FilesSelectionViewModel, IHiddenDwgFilesViewModel
    {
        private IExcelWriter _excelWriter;

        public HiddenDwgFilesViewModel(IFindHiddenDwgFilesService findHiddenDwgFilesService,
                                       IExcelWriter excelWriter)
            : base(
                "Удаление скрытых DWG-файлов",
                "DWG-файлы с параметром «Скрыть = Да»",
                showExport: true)
        {
            _excelWriter = excelWriter;
            SetFiles(findHiddenDwgFilesService.FindHiddenDwgFiles());
        }

        protected override void ExportCore()
        {
            _excelWriter.Create("hiddenDWG_",
                new ExcelSheetData(
                    "Скрытые элементы",
                     Files.Select(item => item.Path).ToList()));
        }
    }
}
