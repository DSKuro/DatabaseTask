using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Messages;

namespace DatabaseTask.Views.Analyse
{
    public partial class DuplicatesFilesWindow : Window
    {
        public DuplicatesFilesWindow()
        {
            InitializeComponent();
            InitializeMessages();
        }


        private void InitializeMessages()
        {
            WeakReferenceMessenger.Default.Register<DuplicatesFilesWindow,
            DuplicatesFilesDialogueCloseMessage>(this, (window, message) =>
            {
                window.Close(message.PathsWithDatabase);
            });
        }
    }
}