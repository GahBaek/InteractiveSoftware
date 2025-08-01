using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ShowRoomDisplay.Converters
{
    public class AbsolutePositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3 || !(values[0] is double pos) || !(values[1] is double canvasSize) || !(values[2] is double imageSize))
                return 0;
            return imageSize == 0 ? 0 : pos * (canvasSize / imageSize);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class AbsoluteSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 3 || !(values[0] is double size) || !(values[1] is double canvasSize) || !(values[2] is double imageSize))
                return 0;
            return imageSize == 0 ? 0 : size * (canvasSize / imageSize);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
