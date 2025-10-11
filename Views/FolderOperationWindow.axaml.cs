using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Messages;
using System;

namespace DatabaseTask;

public partial class FolderOperationWindow : Window
{
    public string Watermark { get; set; }
    public string Text { get; set; }

    public FolderOperationWindow()
    {
        InitializeComponent();
        Watermark = "";
        Text = "";
        InitializeMessages();
    }

    private void InitializeMessages()
    {
        WeakReferenceMessenger.Default.Register<FolderOperationWindow, DialogueWindowCloseMessage>(this,
        (window, message) =>
        {
            window.Close(message.StringValue);
        });
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        textBox.Watermark = Watermark;
        textBox.Text = Text;
    }
}