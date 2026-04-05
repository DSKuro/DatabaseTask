using DatabaseTask.Services.Database.Utils.Interfaces;
using System.IO;

namespace DatabaseTask.Services.Database.Utils
{
    public class DatabasePath : IDatabasePath
    {
        const string _baseFolder = @".\..\dwg";

        public string NormalizeDatabasePath(string path)
        {

            if (path.Contains(@"\dwg\"))
            {
                return @".\" + Path.GetRelativePath(_baseFolder, path);
            }

            return path;
        }

        public string DenormalizePath(string path)
        {
            if (path.StartsWith(_baseFolder))
            {
                return path;
            }

            return $@"{_baseFolder}\{path.TrimStart('.', '\\')}";
        }
    }
}
