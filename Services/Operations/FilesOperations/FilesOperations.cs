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

        public bool CopyFolder(string oldPath, string newPath)
        {
            try
            {
                DirectoryInfo? dir = new DirectoryInfo(oldPath);

                if (!dir.Exists)
                {
                    return false;
                }

                DirectoryInfo[] dirs = dir.GetDirectories();

                Directory.CreateDirectory(newPath);

                foreach (FileInfo file in dir.GetFiles())
                {
                    string targetFilePath = Path.Combine(newPath, file.Name);
                    file.CopyTo(targetFilePath);
                }

                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(newPath, subDir.Name);
                    CopyFolder(subDir.FullName, newDestinationDir);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CopyFile(string oldPath, string newPath)
        {
            try
            {
                if (!File.Exists(oldPath))
                {
                    return false;
                }

                File.Copy(oldPath, newPath, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool MoveFile(string oldPath, string newPath)
        {
            try
            {
                if (!File.Exists(oldPath))
                {
                    return false;
                }

                File.Move(oldPath, newPath, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
