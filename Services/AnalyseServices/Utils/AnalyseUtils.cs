using DatabaseTask.Services.AnalyseServices.Utils.Interfaces;
using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DatabaseTask.Services.AnalyseServices.Utils
{
    public class AnalyseUtils : IAnalyseUtils
    {
        private const string _filePrefix = "unusedfiles_";

        private readonly IFullPath _fullPath;

        public AnalyseUtils(IFullPath fullPath)
        {
            _fullPath = fullPath;
        }

        public void ClearTempFiles()
        {
            string tempPath = Path.GetTempPath();
            var files = Directory.GetFiles(tempPath, $"{_filePrefix}*.xlsx");

            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                }
            }
        }

        public IEnumerable<FileInfo> GetCoreFiles()
        {
            string? corePath = _fullPath.PathToCoreFolder;
            if (string.IsNullOrEmpty(corePath))
            {
                return Enumerable.Empty<FileInfo>();
            }

            DirectoryInfo directory = new DirectoryInfo(corePath);
            return directory.EnumerateFiles("*.*", SearchOption.AllDirectories);
        }
    }
}
