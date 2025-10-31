using DatabaseTask.Views.Comparators.Enum;
using System.Collections;

namespace DatabaseTask.Views.Comparators.Interfaces
{
    public interface IFileComparerFactory
    {
        IComparer CreateFileComparer(FileComparerType type);
    }
}
