using ShowRoomDisplay.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;


namespace ShowRoomDisplay.Models
{
    public partial class Video : ObservableObject
    {

        #region Public Property
        [ObservableProperty]
        private double x;

        [ObservableProperty]
        private double y;

        [ObservableProperty]
        private double width = 50;

        [ObservableProperty]
        private double height = 50;

        [ObservableProperty]
        private Color color = Colors.Orange;

        [ObservableProperty]
        private string videoPath = string.Empty;



        #endregion

        #region Converter
        public VideoDto ToDto()
        {
            return new VideoDto()
            {
                X = this.x,
                Y = this.y,
                Color = this.color,
                VideoPath = this.videoPath,
                Width = this.width,
                Height = this.height
            };
        }

        public static Video Convert(VideoDto dto)
        {
            return new Video()
            {
                x = dto.X,
                y = dto.Y,
                width = dto.Width,
                height = dto.Height,
                color = dto.Color,
                videoPath = dto.VideoPath,
            };
        }
        #endregion
    }
}
