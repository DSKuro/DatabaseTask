using DatabaseTask.Models.Duplicates;
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

    public class AnalyseFilesDialogueCloseMessage
    {
        public List<string> Paths { get; }

        public AnalyseFilesDialogueCloseMessage(List<string> paths)
        {
            Paths = paths;
        }
    }

    public class DuplicatesFilesDialogueCloseMessage
    {
        public DuplicatesFilesDialogResult Paths { get; }

        public DuplicatesFilesDialogueCloseMessage(DuplicatesFilesDialogResult paths)
        {
            Paths = paths;
        }
    }
}
