using Avalonia.Controls;
using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.Base;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using System.Threading.Tasks;

namespace DatabaseTask.Services.Dialogues.MessageBox
{
    public class MessageBoxService : IMessageBoxService
    {
        private readonly IDialogueHelper _dialogueHelper;
        public MessageBoxService(IDialogueHelper dialogueHelper)
        {
            _dialogueHelper = dialogueHelper;
        }

        public Task<ButtonResult?> ShowMessageBoxAsync(object context,
            MessageBoxOptions options)
        {
            TopLevel topLevel = _dialogueHelper.GetTopLevelForAnyDialogue(context);
            return ShowMessageBoxImpl(options, topLevel);
        }

        private async Task<ButtonResult?> ShowMessageBoxImpl(MessageBoxOptions options,
            TopLevel topLevel)
        {
            IMsBoxWindow<ButtonResult> msgBox = MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
            {
                ButtonDefinitions = options.Buttons,
                ContentTitle = options.Title,
                ContentMessage = options.Content
            });
            return await msgBox.ShowDialog(topLevel as Window);
        }
    }
}
