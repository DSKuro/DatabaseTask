using DatabaseTask.Services.FilesOperations.Interfaces;
using System.IO;

namespace DatabaseTask.Services.FilesOperations
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
