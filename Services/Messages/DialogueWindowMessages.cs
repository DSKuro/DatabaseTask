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
}
