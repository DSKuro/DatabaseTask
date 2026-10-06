using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Services.Messages;

namespace DatabaseTask.Views.Analyse
{
    public partial class FilesSelectionWindow : Window
    {
        public FilesSelectionWindow()
        {
            InitializeComponent();
            InitializeMessages();
        }

        private void InitializeMessages()
        {
            WeakReferenceMessenger.Default.Register<FilesSelectionWindow,
                AnalyseFilesDialogueCloseMessage>(this, (window, message) =>
                {
                    window.Close(message.Paths);
                });
        }
    }
}
