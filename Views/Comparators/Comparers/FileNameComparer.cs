using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using System.Collections;

namespace DatabaseTask.Views.Comparators.Comparers
{
    public class FileNameComparer : IComparer
    {
        private readonly INodeComparer _comparer;

        public FileNameComparer(INodeComparer comparer)
        {
            _comparer = comparer;
        }

        public int Compare(object? x, object? y)
        {
            if (x is FileProperties fileX && y is FileProperties fileY)
            {
                return _comparer.Compare(fileX.Node, fileY.Node);
            }

            return 0;
        }
    }
}
