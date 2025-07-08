using CommunityToolkit.Mvvm.Messaging;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.MessageBox;
using DatabaseTask.Services.Messages;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public partial class FolderOperationWindowViewModel : ViewModelMessageBox
    {
        public string FolderName { get; set; }

        public FolderOperationWindowViewModel(IMessageBoxService messageBoxService)
            : base(messageBoxService) { }

        public async Task OnApplyButtonClick()
        {
            await OnApplyButtonClickImpl();
        }

        private async Task OnApplyButtonClickImpl()
        {
            if (FolderName == null || FolderName == "")
            {
                await MessageBoxHelper("FolderOperationDialogue", new MessageBoxOptions(
                    MessageBoxConstants.Error.Value, "Имя каталога не может быть пустым",
                    ButtonEnum.Ok), null);
                return;
            }
            WeakReferenceMessenger.Default.Send(new FolderOperationWindowCloseMessage(FolderName));
        }

        public void OnCancelButtonClick()
        {
            WeakReferenceMessenger.Default.Send(new FolderOperationWindowCloseMessage(null));
        }
    }
}
