using DatabaseTask.Services.Operations.FilesOperations.Interfaces;
using System;
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

        public bool DeleteFolder(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
