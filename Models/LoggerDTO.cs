using DatabaseTask.Services.Collection;

namespace DatabaseTask.Models
{
    public class LoggerDTO
    {
        public LogCategory LogCategory { get; private set; }
        public object[] Parameters { get; private set; }

        public LoggerDTO(LogCategory logCategory, params object[] parameters)
        {
            LogCategory = logCategory;
            Parameters = parameters;
        }
    }
}
