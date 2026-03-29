using System.Collections.Generic;
using System.IO;

namespace DatabaseTask.Services.AnalyseServices.Utils.Interfaces
{
    public interface IAnalyseUtils
    {
        public void ClearTempFiles();
        public IEnumerable<FileInfo> GetCoreFiles();
    }
}
