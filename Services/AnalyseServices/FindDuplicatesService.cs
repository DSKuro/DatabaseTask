using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Database.Repositories.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DatabaseTask.Services.AnalyseServices
{
    public class FindDuplicatesService : IFindDuplicatesService
    {
        private readonly IFullPath _fullPath;
        private readonly ITblDrawingContentsRepository _drawingRepository;
        private readonly IAnalyseUtils _analyseUtils;

        public FindDuplicatesService(IFullPath fullPath,
            ITblDrawingContentsRepository drawingRepository,
            IAnalyseUtils analyseUtils)
        {
            _fullPath = fullPath;
            _drawingRepository = drawingRepository;
            _analyseUtils = analyseUtils;
        }

        public IEnumerable<(string key, List<(string path, bool isInDatabase)> files)> FindDuplicatesByHash()
        {
            var existedPaths = _drawingRepository.GetExistedPaths();

            if (existedPaths == null)
            {
                return new List<(string key, List<(string path, bool isInDatabase)>)>();
            }

            return _analyseUtils.GetCoreFiles()
                   .GroupBy(file => file.Length)
                   .Where(sizeGroup => sizeGroup.Count() > 1)
                   .SelectMany(sizeGroup => sizeGroup)
                   .GroupBy(file => GetHash(file.FullName))
                   .Where(hashGroup => hashGroup.Count() > 1)
                   .Select(hashGroup =>
                   (
                       hashGroup.First().Name,
                       files: hashGroup.Select(file => GetPaths(existedPaths, file)).ToList()
                   ));
        }

        private string GetHash(string filePath)
        {
            using var sha256 = SHA256.Create();
            using (var stream = File.OpenRead(filePath))
            {
                return BitConverter.ToString(sha256.ComputeHash(stream));
            }
        }

        public IEnumerable<(string key, List<(string path, bool isInDatabase)> files)> FindDuplicatesByNameAndSize()
        {
            var existedPaths = _drawingRepository.GetExistedPaths();

            if (existedPaths == null)
            {
                return new List<(string key, List<(string path, bool isInDatabase)>)>();
            }

            return _analyseUtils.GetCoreFiles()
                   .GroupBy(file => new { file.Name, file.Length })
                   .Where(group => group.Count() > 1)
                   .Select(group => (
                       group.Key.Name,
                       files: group.Select(file => GetPaths(existedPaths, file)).ToList()
                   ));
        }

        private (string path, bool isInDatabase) GetPaths(List<string?> existedPaths, FileInfo info)
        {
            string simpleRelativePath = Path.GetRelativePath(_fullPath.PathToCoreFolder!, info.FullName);
            string result = Path.Combine(".", simpleRelativePath);
            bool isExisted = false;

            if (existedPaths.Contains(result))
            {
                isExisted = true;
            }

            return (result, isExisted);
        }
    }
}
