using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.AnalyseServices
{
    public class FindHiddenDwgFilesService : IFindHiddenDwgFilesService
    {
        private readonly ITblDrawingContentsRepository _drawingRepository;
        private readonly IAnalyseUtils _analyseUtils;
        private readonly IDatabasePath _databasePath;
        private readonly IFullPath _fullPath;

        public FindHiddenDwgFilesService(
            ITblDrawingContentsRepository drawingRepository,
            IAnalyseUtils analyseUtils,
            IDatabasePath databasePath,
            IFullPath fullPath)
        {
            _drawingRepository = drawingRepository;
            _analyseUtils = analyseUtils;
            _databasePath = databasePath;
            _fullPath = fullPath;
        }

        public List<string> FindHiddenDwgFiles()
        {
            List<string>? paths = _drawingRepository.GetHiddenDwgPaths();

            if (paths is null || string.IsNullOrEmpty(_fullPath.PathToCoreFolder))
            {
                return new List<string>();
            }

            var hiddenPaths = paths
                .Where(path => !string.IsNullOrWhiteSpace(path))
                .Select(_databasePath.NormalizeDatabasePath)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            return _analyseUtils.GetCoreFiles()
                .Where(file => string.Equals(
                    file.Extension,
                    ".dwg",
                    StringComparison.OrdinalIgnoreCase))
                .Select(file => Path.Combine(
                    ".",
                    Path.GetRelativePath(_fullPath.PathToCoreFolder, file.FullName)))
                .Where(hiddenPaths.Contains)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }
}
