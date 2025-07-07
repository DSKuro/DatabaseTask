using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DatabaseTask.Services.Messages
{
    public class MainWindowEnableManagerButtons {}
    public class MainWindowDialogueMessage : AsyncRequestMessage<string> { }
    public class MainWindowCreateFolderMessage : MainWindowDialogueMessage { }
    public class MainWindowRenameFolderMessage : MainWindowDialogueMessage { }
}
