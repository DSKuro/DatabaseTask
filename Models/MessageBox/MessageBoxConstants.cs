namespace DatabaseTask.Models.MessageBox
{
    public class MessageBoxConstants
    {
        public string Value { get; private set; }

        private MessageBoxConstants(string value)
        {
            Value = value;
        }

        public static MessageBoxConstants Error { get { return new MessageBoxConstants("Ошибка"); } }
        public static MessageBoxConstants Warning { get { return new MessageBoxConstants("Предупреждение"); } }
    }
}
