using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Excel;
using DatabaseTask.Services.Excel.Writer;
using DatabaseTask.ViewModels.Analyses.Interfaces;
using System.Linq;

namespace DatabaseTask.ViewModels.Analyses
{
    public class UnusedFilesViewModel : FilesSelectionViewModel, IUnusedFilesViewModel
    {
        private readonly IFindUnusedFilesServices _findUnusedFilesServices;
        private readonly IExcelWriter _excelWriter;
        private readonly IAnalyseUtils _analyseUtils;

        public UnusedFilesViewModel(
            IFindUnusedFilesServices findUnusedFilesServices,
            IExcelWriter excelWriter,
            IAnalyseUtils analyseUtils)
            : base(
                "Анализ неиспользуемых файлов",
                "Неиспользуемые файлы",
                "Неопознанные файлы",
                true)
        {
            _findUnusedFilesServices = findUnusedFilesServices;
            _excelWriter = excelWriter;
            _analyseUtils = analyseUtils;
            _analyseUtils.ClearTempFiles();
            LoadUnusedFiles();
        }

        private void LoadUnusedFiles()
        {
            (var unusedFiles, var exceptFiles) = _findUnusedFilesServices.FindUnusedFiles();
            SetFiles(unusedFiles);
            SetAdditionalFiles(exceptFiles);
        }

        protected override void ExportCore()
        {
            _excelWriter.Create("unusedfiles_",
                new ExcelSheetData(
                    "Неиспользуемые файлы",
                     Files.Select(item => item.Path).ToList()),
                new ExcelSheetData(
                    "Неопознанные файлы",
                    AdditionalFiles.ToList()));
        }
    }
}
