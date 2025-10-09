using DatabaseTask.Models.MessageBox;
using MsBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Dialogues.MessageBox
{
    public interface IMessageBoxService
    {
        public Task<ButtonResult?> ShowMessageBoxAsync(object context, MessageBoxOptions options);
    }
}
