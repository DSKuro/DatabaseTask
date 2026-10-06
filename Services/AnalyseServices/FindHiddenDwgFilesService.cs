using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Database.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseTask.Services.AnalyseServices
{
    public class FindHiddenDwgFilesService : IFindHiddenDwgFilesService
    {
        private readonly ITblDrawingContentsRepository _drawingRepository;
        private readonly IDatabasePath _databasePath;

        public FindHiddenDwgFilesService(
            ITblDrawingContentsRepository drawingRepository,
            IDatabasePath databasePath)
        {
            _drawingRepository = drawingRepository;
            _databasePath = databasePath;
        }

        public List<string> FindHiddenDwgFiles()
        {
            List<string>? paths = _drawingRepository.GetHiddenDwgPaths();

            if (paths is null)
            {
                return new List<string>();
            }

            return paths
                .Where(path => !string.IsNullOrWhiteSpace(path))
                .Select(_databasePath.NormalizeDatabasePath)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
    }
}
