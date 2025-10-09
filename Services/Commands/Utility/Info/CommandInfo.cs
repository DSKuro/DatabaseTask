using DatabaseTask.Services.Commands.Utility.Enum;

namespace DatabaseTask.Services.Commands.Utility.Info
{
    public class CommandInfo
    {
        public object? Data { get; set; }
        public CommandType CommandType { get; set; }

        public CommandInfo(object? data, CommandType commandType)
        {
            Data = data;
            CommandType = commandType;
        }
    }
}
