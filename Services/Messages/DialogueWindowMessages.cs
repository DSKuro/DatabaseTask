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
}
