using DatabaseTask.Models.Categories;
using DatabaseTask.Services.Commands.Utility.Enum;

namespace DatabaseTask.Models.DTO
{
    public class LoggerCommandDTO : CommandDTO
    {
        public LogCategory Category { get; set; }
        public object[] Parameters { get; set; }

        public LoggerCommandDTO(CommandType type,
            LogCategory category,
            object? data = null,
            params object[] parameters)
            : base(type, null)
        {
            Category = category;
            Parameters = parameters;
        }
    }
}
