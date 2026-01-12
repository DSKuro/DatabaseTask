using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;

namespace DatabaseTask.Services.Messages
{
    public class MainWindowToggleManagerButtons 
    {
        public bool Value { get; set; }

        public MainWindowToggleManagerButtons(bool value)
        {
            Value = value;
        }
    }

    public class MainWindowDialogueMessage : AsyncRequestMessage<string> { }
    public class MainWindowCreateFolderMessage : MainWindowDialogueMessage { }
    public class MainWindowRenameFolderMessage : MainWindowDialogueMessage { }
    public class MainWindowCopyFolderMessage : MainWindowDialogueMessage { }

    public class MainWindowUnusedFilesMessage : AsyncRequestMessage<List<string>> { }
    public class MainWindowDuplicatesFilesMessage : MainWindowDialogueMessage { }
}
