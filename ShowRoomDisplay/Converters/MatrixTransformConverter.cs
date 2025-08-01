using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ShowRoomDisplay.Converters
{
    public class MatrixTransformConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is Matrix matrix)
                {
                    // Process the matrix as needed  
                    return new MatrixTransform(matrix);
                }
                throw new InvalidOperationException("Value is not a valid Matrix.");
            }
            catch (Exception ex)
            {
                // Handle exception if necessary
                throw new InvalidOperationException("Error converting value to MatrixTransform", ex);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
