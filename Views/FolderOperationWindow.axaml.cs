using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Messages;
using System;

namespace DatabaseTask;

public partial class FolderOperationWindow : Window
{
    public string Watermark { get; set; }

    public FolderOperationWindow()
    {
        InitializeComponent();

        InitializeMessages();
    }

    private void InitializeMessages()
    {
        WeakReferenceMessenger.Default.Register<FolderOperationWindow, FolderOperationWindowCloseMessage>(this,
            (window, message) =>
            {
                window.Close(message.FolderName);
            });
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        textBox.Watermark = Watermark;
    }
}