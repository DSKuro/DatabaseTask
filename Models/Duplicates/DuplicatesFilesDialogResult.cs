using System.Collections.Generic;

namespace DatabaseTask.Models.Duplicates
{
    public class DuplicatesFilesDialogResult
    {
        public List<string> PathsToDelete { get; } = new List<string>();
        public List<DuplicatePathUpdate> PathsToUpdate { get; } = new List<DuplicatePathUpdate>();

        public DuplicatesFilesDialogResult()
        {

        }

        public DuplicatesFilesDialogResult(List<string> pathsToDelete, List<DuplicatePathUpdate> pathsToUpdate)
        {
            PathsToDelete = pathsToDelete;
            PathsToUpdate = pathsToUpdate;
        }
    }
}
