using Avalonia.Controls;

namespace DatabaseTask.Services.Dialogues.Base
{
    public interface IDialogueManager
    {
        public TopLevel? GetTopLevelForContext(object context);
    }
}
