using System.Collections.Generic;

namespace DatabaseTask.Services.Messages
{ 
    public class DialogueWindowCloseMessage
    {
        public string? StringValue { get; }

        public DialogueWindowCloseMessage(string? stringValue)
        {
            StringValue = stringValue;
        }
    }

    public class UnusedFilesDialogueCloseMessage
    {
        public List<string> Paths { get; }

        public UnusedFilesDialogueCloseMessage(List<string> paths)
        {
            Paths = paths;
        }
    }

    public class DuplicatesFilesDialogueCloseMessage
    {
        public List<(string path, bool isInDatabase)> PathsWithDatabase { get; }

        public DuplicatesFilesDialogueCloseMessage(List<(string path, bool isInDatabase)> paths)
        {
            PathsWithDatabase = paths;
        }
    }
}
