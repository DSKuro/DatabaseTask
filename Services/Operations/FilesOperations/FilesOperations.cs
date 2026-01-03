using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System.IO;

namespace DatabaseTask.Services.Operations.FilesOperations
{
    public class FilesOperations : IFilesOperations
    {
        public bool CreateFolder(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool RenameFolder(string oldPath, string newPath)
        {
            try
            {
                if (!Directory.Exists(oldPath))
                {
                    return false;
                }
                Directory.Move(oldPath, newPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
