using System.Collections.Generic;

namespace DatabaseTask.Services.Database.Repositories.Interfaces
{
    public interface ITblDrawingContentsRepository
    {
        public TblDrawingContent? GetFirstItem();
        public List<string?>? GetExistedPaths();
        public void UpdatePath(string oldPath, string newPath);
        public void CopyItems(string oldPath, string newPath);
        public void DeleteItem(string path);
    }
}
