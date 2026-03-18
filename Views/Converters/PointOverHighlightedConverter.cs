using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace DatabaseTask.Views.Converters
{
    public class PointOverHighlightedConverter : IValueConverter
    {
        public IBrush HighlightBrush { get; set; } = new SolidColorBrush(Color.Parse("#FFCC80"));
        public IBrush NonHighlightBrush { get; set; } =
            Application.Current?.TryGetResource("ButtonBackgroundPointerOver", null, out var brush) is true
           ? (IBrush)brush!
           : new SolidColorBrush(Colors.Transparent);

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool b && b)
            {
                return HighlightBrush;
            }

            return NonHighlightBrush;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
