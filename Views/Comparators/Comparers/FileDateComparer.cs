using DatabaseTask.ViewModels.MainViewModel.Controls.DataGrid;
using System;
using System.Collections;

namespace DatabaseTask.Views.Comparators.Comparers
{
    public class FileDateComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            if (x is FileProperties fileX && y is FileProperties fileY)
            {
                DateTime dateX = ParseDateTime(fileX.Modificated);
                DateTime dateY = ParseDateTime(fileY.Modificated);
                return dateX.CompareTo(dateY);
            }
            return 0;
        }

        private DateTime ParseDateTime(string dateTime)
        {
            if (DateTime.TryParse(dateTime, out DateTime result))
            {
                return result;
            }

            if (TimeSpan.TryParse(dateTime, out TimeSpan time))
            {
                return DateTime.Today.Add(time);
            }

            return DateTime.MinValue;
        }
    }
}
