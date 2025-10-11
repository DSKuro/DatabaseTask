using DatabaseTask.Services.Commands.Utility.Enum;

namespace DatabaseTask.Models.DTO
{
    public class CommandInfo
    {
        public CommandType CommandType { get; set; }
        public object[] Data { get; set; }

        public CommandInfo(CommandType commandType, params object[] data)
        {
            CommandType = commandType;
            Data = data;
        }
    }
}
