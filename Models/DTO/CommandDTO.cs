using DatabaseTask.Services.Commands.Enum;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.Models.DTO
{
    public class CommandDTO
    {
        public CommandType Type { get; set; }
        public Action Permission { get; set; }
        public Func<Task<object?>> GetDataFunction { get; set; }

        public CommandDTO(CommandType type, Action permission, Func<Task<object?>> getDataFunction)
        {
            Type = type;
            Permission = permission;
            GetDataFunction = getDataFunction;
        }
    }
}
