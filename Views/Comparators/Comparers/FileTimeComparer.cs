using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using System;
using System.Collections;
using System.Globalization;

namespace DatabaseTask.Views.Comparators.Comparers
{
    public class FileTimeComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is FileProperties fileX && y is FileProperties fileY)
            {
                TimeSpan timeX = ParseTime(fileX.Modificated);
                TimeSpan timeY = ParseTime(fileY.Modificated);
                return timeX.CompareTo(timeY);
            }
            return 0;
        }

        private TimeSpan ParseTime(string timeString)
        {
            if (string.IsNullOrEmpty(timeString))
            {
                return TimeSpan.MinValue;
            }

            if (TimeSpan.TryParseExact(timeString, "hh\\:mm", CultureInfo.InvariantCulture, out TimeSpan time))
            {
                return time;
            }

            if (TimeSpan.TryParse(timeString, out time))
            {
                return time;
            }

            return TimeSpan.MinValue;
        }
    }
}
