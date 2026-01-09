using System.Collections.Generic;
using System.IO;

namespace DatabaseTask.Services.AnalyseServices.Interfaces
{
    public interface IFindDuplicatesService
    {
        public IEnumerable<(string key, List<FileInfo> files)> FindDuplicatesByNameAndSize();
        public IEnumerable<(string key, List<FileInfo> files)> FindDuplicatesByHash();
    }
}
