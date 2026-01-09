using DatabaseTask.Services.AnalyseServices.Interfaces;
using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace DatabaseTask.Services.AnalyseServices
{
    public class FindDuplicatesService : IFindDuplicatesService
    {
        private readonly IAnalyseUtils _analyseUtils;

        public FindDuplicatesService(IAnalyseUtils analyseUtils)
        {
            _analyseUtils = analyseUtils;
        }

        public IEnumerable<(string key, List<FileInfo> files)> FindDuplicatesByHash()
        { 
            return _analyseUtils.GetCoreFiles()
                   .GroupBy(file => file.Length)
                   .Where(sizeGroup => sizeGroup.Count() > 1)
                   .SelectMany(sizeGroup => sizeGroup)
                   .GroupBy(file => GetHash(file.FullName))
                   .Where(hashGroup => hashGroup.Count() > 1)
                   .Select(hashGroup =>
                   (
                       hashGroup.First().Name,
                       hashGroup.ToList()
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

        public IEnumerable<(string key, List<FileInfo> files)> FindDuplicatesByNameAndSize()
        {
            return _analyseUtils.GetCoreFiles()
                   .GroupBy(file => new { file.Name, file.Length })
                   .Where(group => group.Count() > 1)
                   .Select(group => (
                       group.Key.Name,
                       group.ToList()
                   ));
        }
    }
}
