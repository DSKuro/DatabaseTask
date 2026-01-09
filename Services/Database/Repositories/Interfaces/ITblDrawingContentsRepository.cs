using System.Collections.Generic;

namespace DatabaseTask.Services.Database.Repositories.Interfaces
{
    public interface ITblDrawingContentsRepository
    {
        public TblDrawingContent? GetFirstItem();
        public List<string?>? GetExistedPaths();
    }
}
