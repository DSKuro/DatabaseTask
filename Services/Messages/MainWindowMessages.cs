using CommunityToolkit.Mvvm.Messaging.Messages;

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
}
