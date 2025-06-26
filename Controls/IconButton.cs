using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace DatabaseTask.Controls
{
    public class IconButton : Button
    {
        public static readonly StyledProperty<string> IconSourceProperty =
            AvaloniaProperty.Register<IconButton, string>(nameof(IconSource));

        public string IconSource
        {
            get => GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        public static readonly StyledProperty<double> IconSizeProperty =
            AvaloniaProperty.Register<IconButton, double>(nameof(IconSize), 16.0);

        public double IconSize
        {
            get => GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public static readonly StyledProperty<double> SpacingProperty =
            AvaloniaProperty.Register<IconButton, double>(nameof(Spacing), 8.0);

        public double Spacing
        {
            get => GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public static readonly StyledProperty<string> CssProperty =
            AvaloniaProperty.Register<IconButton, string>(nameof(Css));

        public string Css
        {
            get => GetValue(CssProperty);
            set => SetValue(CssProperty, value);
        }

        public static readonly StyledProperty<Orientation> OrientationProperty =
            AvaloniaProperty.Register<IconButton, Orientation>(
                nameof(Orientation),
                Orientation.Horizontal);

        public Orientation Orientation
        {
            get => GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }
    }
}
