using MsBox.Avalonia.Enums;

namespace DatabaseTask.Models.MessageBox
{
    public class MessageBoxOptions
    {
        public string Title
        {
            get;
        } 
        public string Content
        {
            get;
        }
        public ButtonEnum Buttons
        { 
            get; 
        }

        public MessageBoxOptions(string title, string content, ButtonEnum buttons)
        {
            Title = title;
            Content = content;
            Buttons = buttons;
        }
    }
}
