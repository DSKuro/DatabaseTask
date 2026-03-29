using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.AnalyseServices
{
    public class FindUnusedFilesServices : IFindUnusedFilesServices
    {
        private readonly ITblDrawingContentsRepository _drawingRepository;
        private readonly IAnalyseUtils _analyseUtils;
        private readonly IFullPath _fullPath;

        public FindUnusedFilesServices(ITblDrawingContentsRepository drawingRepository,
            IAnalyseUtils analyseUtils,
            IFullPath fullPath)
        {
            _drawingRepository = drawingRepository;
            _analyseUtils = analyseUtils;
            _fullPath = fullPath;
        }

        public (List<string>, List<string>) FindUnusedFiles()
        {
            List<string?>? existedPaths = _drawingRepository.GetExistedPaths();

            if (existedPaths == null)
            {
                return (new List<string>(), new List<string>());
            }

            var newExistedPaths = GetCorrectDatabasePaths(existedPaths);

            if (newExistedPaths is null || !newExistedPaths.Any())
            {
                return (new List<string>(), new List<string>());
            }

            IEnumerable<FileInfo?> directory = _analyseUtils.GetCoreFiles();
            if (!directory.Any())
            {
                return (new List<string>(), new List<string>());
            }

            List<string> filesInCatalog = GetPathsForFilesInCatalog(directory);

            List<string> unusedFiles = GetUnusedFiles(newExistedPaths!, filesInCatalog);

            var existFiles = filesInCatalog.Except(unusedFiles);

            var exceptFiles = newExistedPaths.Except(existFiles).ToList();

            return (unusedFiles, exceptFiles);
        }

        private List<string>? GetCorrectDatabasePaths(List<string?> paths)
        {
            return paths.Distinct()
                .Select(x =>
                {
                    string baseFolder = @".\..\dwg"; 
                    string? result = x!.Contains(@"\dwg\") ? 
                    @".\" + Path.GetRelativePath(baseFolder, x) : 
                    x; 
                    return result;
                })
                .ToList();
        }

        private List<string> GetPathsForFilesInCatalog(IEnumerable<FileInfo?> directory)
        {
            List<string> filesList = new List<string>();



            foreach (var file in directory)
            {
                if (file != null)
                {
                    string simpleRelativePath = Path.GetRelativePath(_fullPath.PathToCoreFolder!,
                        file.FullName);
                    string result = Path.Combine(".", simpleRelativePath);
                    filesList.Add(result);
                }
            }

            return filesList;
        }

        private List<string> GetUnusedFiles(List<string?> existedPaths, List<string> filesInCatalog)
        {
            List<string> unusedFiles = new List<string>();

            var existedSet = new HashSet<string>(existedPaths!, StringComparer.OrdinalIgnoreCase);

            foreach (string file in filesInCatalog)
            {
                if (!existedSet.Contains(file))
                {
                    unusedFiles.Add(file);
                }
            }

            return unusedFiles;
        }
    }
}
