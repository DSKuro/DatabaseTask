using System.Collections.Generic;

namespace DatabaseTask.Services.Database.Repositories.Interfaces
{
    public interface ITblDrawingContentsRepository
    {
        public TblDrawingContent? GetFirstItem();
        public List<string?>? GetExistedPaths();
        public Dictionary<string, List<TblDrawingContent>> GetPathIndex(DataContext context, List<string> paths);
        public void UpdatePathContext(DataContext context, List<TblDrawingContent> allRecords, string oldPath, string newPath);
        public void CopyItemsContext(DataContext context, List<TblDrawingContent> allRecords, string oldPath, string newPath);
        public void DeleteItemContext(DataContext context, List<TblDrawingContent> allRecords, string path);
    }
}
