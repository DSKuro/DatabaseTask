using DatabaseTask.Models.Categories;

namespace DatabaseTask.Models.DTO
{
    public class LoggerDTO
    {
        public LogCategory LogCategory { get; private set; }
        public string[] Parameters { get; private set; }

        public LoggerDTO(LogCategory logCategory, params string[] parameters)
        {
            LogCategory = logCategory;
            Parameters = parameters;
        }
    }
}
