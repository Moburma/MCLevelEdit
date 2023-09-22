using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Globalization;

namespace MCLevelEdit.DataModel
{
    public class PositionConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            return "0";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var i = 0;
                int.TryParse(value.ToString(), out i);
                return (i < 0 || i > 255 ? 0 : i);
            }
            return 0;
        }
    }

    public class Position : ObservableObject
    {
        private int _x;
        private int _y;
        
        public int X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        public int Y
        {
            get { return _y; }
            set { SetProperty(ref _y, value); }
        }

        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }

    };
}
