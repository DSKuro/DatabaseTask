using DatabaseTask.Services.Comparer.Interfaces;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes;
using DatabaseTask.ViewModels.MainViewModel.Controls.Nodes.Interfaces;

namespace DatabaseTask.Services.Comparer
{
    public class NodeComparer : INodeComparer
    {
        private IWindowsFileComparer _fileComparer;

        public NodeComparer(IWindowsFileComparer fileComparer)
        {
            _fileComparer = fileComparer;
        }

        public int Compare(INode? x, INode? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x is NodeViewModel left && y is NodeViewModel right)
            {
                if (left.IsFolder && !right.IsFolder)
                {
                    return -1;
                }

                if (!left.IsFolder && right.IsFolder)
                {
                    return 1;
                }

                return _fileComparer.Compare(left.Name, right.Name);
            }
            return 0;
        }
    }
}
