using DatabaseTask.Models;
using DatabaseTask.Services.Dialogues.MessageBox;
using System;
using System.Threading.Tasks;

namespace DatabaseTask.ViewModels
{
    public class ViewModelMessageBox : ViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;

        public ViewModelMessageBox(IMessageBoxService messageBoxService)
        {
            _messageBoxService = messageBoxService;
        }

        protected async Task MessageBoxHelper(MessageBoxOptions options, Action callback)
        {
            try
            {
                await _messageBoxService.ShowMessageBoxAsync(this, options);
            }
            finally
            {
                if (callback != null)
                {
                    callback.Invoke();
                }
            }
        }

        protected void ErrorCallback()
        {
            Environment.Exit(1);
        }
    }
}
