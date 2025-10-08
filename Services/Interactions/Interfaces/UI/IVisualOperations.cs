using System;

namespace DatabaseTask.Services.Interactions.Interfaces.UI
{
    public interface IVisualOperations<T> where T : EventArgs
    {
        public void SetDropHighlight(T args, string style);
        public void ClearDropHighlight(string style);
    }
}
