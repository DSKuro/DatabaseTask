using DatabaseTask.Models.DTO;
using System.Collections.Generic;

namespace DatabaseTask.Services.Operations.LoggerOperations.Interfaces
{
    public interface ILoggerOperations
    {
        public void AddLog(LoggerDTO dto);
        public void UpdateStatus(List<bool> results);
        public void ClearAll();
    }
}
