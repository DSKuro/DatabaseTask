using System.Collections.Generic;

namespace DatabaseTask.Services.AnalyseServices.Interfaces
{
    public interface IFindHiddenDwgFilesService
    {
        public List<string> FindHiddenDwgFiles();
    }
}
