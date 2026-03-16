using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Globalization;

namespace DatabaseTask.Views.Converters
{
    public class PointOverHighlightedConverter : IValueConverter
    {
        public IBrush HighlightBrush { get; set; } = new SolidColorBrush(Color.Parse("#FFE4E4"));
        public IBrush NonHighlightBrush { get; set; } = (IBrush)Application.Current.Resources["ButtonBackgroundPointerOver"];

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
