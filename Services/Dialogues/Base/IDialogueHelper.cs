using Avalonia.Controls;

namespace DatabaseTask.Services.Dialogues.Base
{
    public interface IDialogueHelper
    {
        public TopLevel GetTopLevelForAnyDialogue(object? context);
    }
}
