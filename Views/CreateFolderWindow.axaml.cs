using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Messages;

namespace DatabaseTask;

public partial class CreateFolderWindow : Window
{
    public CreateFolderWindow()
    {
        InitializeComponent();

        InitializeMessages();
    }

    private void InitializeMessages()
    {
        WeakReferenceMessenger.Default.Register<CreateFolderWindow, CreateFolderWindowCloseMessage>(this,
            (window, message) =>
            {
                window.Close(message.FolderName);
            });
    }
}