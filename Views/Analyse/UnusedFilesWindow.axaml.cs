using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Messages;

namespace DatabaseTask.Views.Analyse
{
    public partial class UnusedFilesWindow : Window
    {
        public UnusedFilesWindow()
        {
            InitializeComponent();
            InitializeMessages();
        }

        private void InitializeMessages()
        {
            WeakReferenceMessenger.Default.Register<UnusedFilesWindow,
            UnusedFilesDialogueCloseMessage>(this, (window, message) =>
            {
                window.Close(message.Paths);
            });
        }
    }
}

