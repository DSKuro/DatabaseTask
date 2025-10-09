using DatabaseTask.Services.Commands.Utility.Enum;

namespace DatabaseTask.Models.DTO
{
    public class CommandDTO
    {
        public CommandType Type { get; set; }
        public object? Data { get; set; }

        public CommandDTO(CommandType type, object? data = null)
        {
            Type = type;
            Data = data;
        }
    }
}
