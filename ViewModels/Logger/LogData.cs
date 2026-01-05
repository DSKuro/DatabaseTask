using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DatabaseTask.ViewModels.Logger
{
    public class LogData : INotifyPropertyChanged
    {
        private string? _imagePath;

        public string Time { get; set; }
        public string Operation { get; set; }
        public string? ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public LogData(string time, string operation, string? imagePath = "")
        {
            Time = time;
            Operation = operation;
            ImagePath = imagePath;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
