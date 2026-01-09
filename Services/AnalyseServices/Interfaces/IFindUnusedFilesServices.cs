using System.Collections.Generic;

namespace DatabaseTask.Services.AnalyseServices.Interfaces
{
    public interface IFindUnusedFilesServices
    {
        public List<string> FindUnusedFiles();
    }
}
