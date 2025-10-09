using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.IO;

namespace DatabaseTask.Services.Operations.FilesOperations
{
    public class FilesOperations : IFilesOperations
    {
        public void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
