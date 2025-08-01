using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using ShowRoomDisplay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShowRoomDisplay.ViewModels
{
    public partial class VideoViewModel : ViewModelBase
    {
        #region Constructor
        public VideoViewModel() { 
        
        }
        #endregion


        #region Public Property
        [Property]
        private double _X = 0;

        [Property]
        private double _Y = 0;

        [Property]
        private double _Width = 0;

        [Property]
        private double _Height = 0;

        [Property]
        private double _ImageWidth;

        [Property]
        private double _ImageHeight;
        public Color Color
        {
            get => Video?.Color ?? Colors.Transparent;
            set
            {
                if (Video != null && Video.Color != value)
                {
                    Video.Color = value;
                    OnPropertyChanged(nameof(Video.Color));
                }
            }
        }


        [Property]
        public bool _IsSelected;

        [Property]
        private string _ProjectCreateTime = "";

        [Property]
        private string _VideoCreateTime = "";


        [Property]
        private Video _Video = null;
        #endregion

        #region Event Handler;
        partial void OnVideoChanged(Video value)
        {
            if (_Video != null)
                _Video.PropertyChanged -= Video_PropertyChanged;

            this.Width = value.Width;
            this.Height = value.Height;
            this.Color = value.Color;
            this.X = value.X;
            this.Y = value.Y;

            value.PropertyChanged += Video_PropertyChanged;
        }                                                                     

        private void Video_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Video.Color))
            {
                OnPropertyChanged(nameof(Color));
            }

        }
        partial void OnXChanged(double value)
        {
            this.Video.X = value;
        }

        partial void OnYChanged(double value)
        {
            this.Video.Y = value;
        }

        partial void OnWidthChanged(double value)
        {
            this.Video.Width = value;
        }

        partial void OnHeightChanged(double value)
        {
            this.Video.Height = value;
        }

        #endregion
    }
}
