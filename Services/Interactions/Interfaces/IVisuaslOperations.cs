using Avalonia;
using System;

namespace DatabaseTask.Services.Interactions.Interfaces
{
    public interface IVisuaslOperations<T> where T : EventArgs
    {
        public void SetDropHighlight(T args, string style);
        public void ClearDropHighlight(string style);
    }
}
