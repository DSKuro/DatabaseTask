using DatabaseTask.Services.Commands.Enum;

namespace DatabaseTask.Services.Commands.Info
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
