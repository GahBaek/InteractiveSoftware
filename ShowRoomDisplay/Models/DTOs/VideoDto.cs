using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShowRoomDisplay.Models.DTOs
{
    public class VideoDto
    {

        #region Public Property
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;

        public double Width { get; set; } = 0;
        public double Height { get; set; } = 0;

        public Color Color { get; set; } = Colors.Orange;

        public string VideoPath { get; set; } = "";
        #endregion
    }
}
