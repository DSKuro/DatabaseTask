using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace DatabaseTask.Views.Converters
{
    public class DuplicateFirstGroupConverter : IValueConverter
    {
        public IBrush FirstBrush { get; set; } = new SolidColorBrush(Colors.LightYellow);
        public IBrush NonFirstBrush { get; set; } = Brushes.Transparent;

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b && b)
            {
                return FirstBrush;
            }

            return NonFirstBrush;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
