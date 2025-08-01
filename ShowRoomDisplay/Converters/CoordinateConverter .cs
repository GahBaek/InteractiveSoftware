using System;
using System.Globalization;
using System.Windows.Data;

namespace ShowRoomDisplay.Converters
{
    public class CoordinateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double originalX = (double)values[0];
            double imageWidth = (double)values[1];
            double canvasWidth = (double)values[2];

            if (imageWidth == 0) return 0;

            double ratio = canvasWidth / imageWidth;
            return originalX * ratio;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}