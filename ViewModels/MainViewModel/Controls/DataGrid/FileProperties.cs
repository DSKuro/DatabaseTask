using CommunityToolkit.Mvvm.ComponentModel;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid
{
    public partial class FileProperties : ObservableObject
    {
        private string _name;
        private string _size;
        private string _modificated;
        private string _iconPath;
        private INode _node;

        public string Name
        {   
            get => _name;
            set => SetProperty(ref _name, value); 
        }

        public string Size 
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public string Modificated 
        {
            get => _modificated;
            set => SetProperty(ref _modificated, value);
        }

        public string IconPath
        {
            get => _iconPath;
            set => SetProperty(ref _iconPath, value);
        }

        public INode Node
        {
            get => _node;
            set => SetProperty(ref _node, value);
        }

        public FileProperties(string name, 
            string size, 
            string modificated,
            string iconPath,
            INode node)
        {
            _name = name;
            _size = size;
            _modificated = modificated;
            _iconPath = iconPath;
            _node = node;
        }
    }
}
