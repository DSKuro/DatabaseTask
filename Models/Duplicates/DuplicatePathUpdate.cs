namespace DatabaseTask.Models.Duplicates
{
    public class DuplicatePathUpdate
    {
        public string OldPath { get; }
        public string NewPath { get; }

        public DuplicatePathUpdate(string oldPath, string newPath)
        {
            OldPath = oldPath;
            NewPath = newPath;
        }
    }
}
