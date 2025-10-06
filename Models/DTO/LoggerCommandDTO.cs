using DatabaseTask.Services.Collection;
using DatabaseTask.Services.Commands.Enum;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.Models.DTO
{
    public class LoggerCommandDTO : CommandDTO
    {
        public LogCategory Category { get; set; }
        public bool IsFirstData { get; set; }
        public object[] Parameters { get; set; }

        public LoggerCommandDTO(CommandType type, Action permission, Func<Task<object?>> getDataFunction,
            LogCategory category,
            bool isFirstData, params object[] parameters)
            : base(type, permission, getDataFunction)
        {
            Category = category;
            IsFirstData = isFirstData;
            Parameters = parameters;
        }
    }
}
