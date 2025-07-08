namespace DatabaseTask.Services.Messages
{ 
    public class DialogueWindowCloseMessage
    {
        public string StringValue { get; }

        public DialogueWindowCloseMessage(string stringValue)
        {
            StringValue = stringValue;
        }
    }
}
