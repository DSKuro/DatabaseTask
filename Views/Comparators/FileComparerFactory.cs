using DatabaseTask.Views.Comparators.Comparers;
using DatabaseTask.Views.Comparators.Enum;
using DatabaseTask.Views.Comparators.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DatabaseTask.Views.Comparators
{
    public class FileComparerFactory : IFileComparerFactory
    {
        private readonly Dictionary<FileComparerType, IComparer> _comparers = new();

        public FileComparerFactory(IServiceProvider serviceProvider)
        {
            _comparers.Add(FileComparerType.FileNameComparer, 
                ActivatorUtilities.CreateInstance<FileNameComparer>(serviceProvider));
            _comparers.Add(FileComparerType.FileTimeComparer,
                ActivatorUtilities.CreateInstance<FileTimeComparer>(serviceProvider));
            _comparers.Add(FileComparerType.FileSizeComparer,
                ActivatorUtilities.CreateInstance<FileSizeComparer>(serviceProvider));
        }

        public IComparer CreateFileComparer(FileComparerType type)
        {
            return _comparers[type];
        }
    }
}
