using System.Collections.Generic;

namespace DatabaseTask.Services.AnalyseServices.Interfaces
{
    public interface IFindDuplicatesService
    {
        public IEnumerable<(string key, List<(string path, bool isInDatabase)> files)> FindDuplicatesByNameAndSize();
        public IEnumerable<(string key, List<(string path, bool isInDatabase)> files)> FindDuplicatesByHash();
    }
}
