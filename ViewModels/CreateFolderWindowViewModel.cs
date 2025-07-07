using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class CreateFolderWindowViewModel : ViewModelMessageBox
    {
        public string FolderName { get; set; }

        public CreateFolderWindowViewModel(IMessageBoxService messageBoxService)
            : base(messageBoxService) { }

        public async Task OnApplyButtonClick()
        {
            await OnApplyButtonClickImpl();
        }

        private async Task OnApplyButtonClickImpl()
        {
            if (FolderName == null || FolderName == "")
            {
                await MessageBoxHelper(new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Имя каталога не может быть пустым",
                    ButtonEnum.Ok), null);
                return;
            }
            WeakReferenceMessenger.Default.Send(new CreateFolderWindowCloseMessage(FolderName));
        }

        public void OnCancelButtonClick()
        {
            WeakReferenceMessenger.Default.Send(new CreateFolderWindowCloseMessage(null));
        }
    }
}
