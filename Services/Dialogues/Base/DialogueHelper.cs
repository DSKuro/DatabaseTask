using Avalonia.Controls;
using System;

namespace DatabaseTask.Services.Dialogues.Base
{
    public class DialogueHelper
    {
        private readonly DialogueManager _dialogueManager;

        public DialogueHelper(DialogueManager dialogueManager) 
        {
            _dialogueManager = dialogueManager;
        }

        public TopLevel GetTopLevelForAnyDialogue(object? context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            TopLevel? topLevel = _dialogueManager.GetTopLevelForContext(context);

            if (topLevel == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return topLevel;
        }
    }
}
