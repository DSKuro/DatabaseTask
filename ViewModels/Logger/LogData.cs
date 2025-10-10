namespace DatabaseTask.ViewModels.Logger
{
    public class LogData
    {
        public string Time { get; set; }
        public string Operation { get; set; }

        public LogData(string time, string operation)
        {
            Time = time;
            Operation = operation;
        }
    }
}
