using System.Collections.Generic;

namespace DatabaseTask.Services.AnalyseServices.Interfaces
{
    public interface IFindDuplicatesService
    {
        public IEnumerable<(string key, List<string> files)> FindDuplicatesByNameAndSize();
        public IEnumerable<(string key, List<string> files)> FindDuplicatesByHash();
    }
}
