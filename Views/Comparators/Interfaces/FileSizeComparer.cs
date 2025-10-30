using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using System.Collections;

namespace DatabaseTask.Views.Comparators.Interfaces
{
    public class FileSizeComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is FileProperties fileX && y is FileProperties fileY)
            {
                long sizeX = ParseSize(fileX.Size);
                long sizeY = ParseSize(fileY.Size);
                return sizeX.CompareTo(sizeY);
            }
            return 0;
        }

        private static long ParseSize(string size)
        {
            if (string.IsNullOrEmpty(size)) return 0;

            // Обрабатываем форматы: "123 KB", "1.5 MB", etc.
            var parts = size.Split(' ');
            if (parts.Length < 2) return 0;

            if (double.TryParse(parts[0], out var number))
            {
                return parts[1].ToUpperInvariant() switch
                {
                    "KB" => (long)(number * 1024),
                    "MB" => (long)(number * 1024 * 1024),
                    "GB" => (long)(number * 1024 * 1024 * 1024),
                    _ => (long)number
                };
            }
            return 0;
        }
    }
}
