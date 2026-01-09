using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
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

        public List<string> FindUnusedFiles()
        {
            List<string?>? existedPaths = _drawingRepository.GetExistedPaths();
            if (existedPaths == null)
            {
                return new List<string>();
            }

            IEnumerable<FileInfo?> directory = _analyseUtils.GetCoreFiles();
            if (!directory.Any())
            {
                return new List<string>();
            }

            List<string> filesInCatalog = GetPathsForFilesInCatalog(directory);

            return GetUnusedFiles(existedPaths, filesInCatalog);
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
            foreach (string file in filesInCatalog)
            {
                if (!existedPaths.Contains(file))
                {
                    unusedFiles.Add(file);
                }
            }
            return unusedFiles;
        }
    }
}
