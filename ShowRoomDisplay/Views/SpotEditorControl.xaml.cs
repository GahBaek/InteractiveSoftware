using ConvMVVM2.Core.MVVM;
using ShowRoomDisplay.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShowRoomDisplay.Views
{
    /// <summary>
    /// SpotEditorControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SpotEditorControl : UserControl
    {
        public SpotEditorControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(SpotEditorControl));

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty SpotsProperty =
            DependencyProperty.Register(nameof(Spots), typeof(IEnumerable), typeof(SpotEditorControl));

        public IEnumerable Spots
        {
            get => (IEnumerable)GetValue(SpotsProperty);
            set => SetValue(SpotsProperty, value);
        }

        public static readonly DependencyProperty SelectSpotCommandProperty =
            DependencyProperty.Register(nameof(SelectSpotCommand), typeof(ICommand), typeof(SpotEditorControl));

        public ICommand SelectSpotCommand
        {
            get => (ICommand)GetValue(SelectSpotCommandProperty);
            set => SetValue(SelectSpotCommandProperty, value);
        }

        public static readonly DependencyProperty SelectedSpotProperty =
    DependencyProperty.Register(nameof(SelectedSpot), typeof(object), typeof(SpotEditorControl));

        public object SelectedSpot
        {
            get => GetValue(SelectedSpotProperty);
            set => SetValue(SelectedSpotProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(SpotEditorControl), new PropertyMetadata(false));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
        }

        public static readonly DependencyProperty ImageOriginalWidthProperty =
    DependencyProperty.Register(nameof(ImageOriginalWidth), typeof(double), typeof(SpotEditorControl), new PropertyMetadata(1.0));

        public double ImageOriginalWidth
        {
            get => (double)GetValue(ImageOriginalWidthProperty);
            set => SetValue(ImageOriginalWidthProperty, value);
        }

        public static readonly DependencyProperty ImageOriginalHeightProperty =
            DependencyProperty.Register(nameof(ImageOriginalHeight), typeof(double), typeof(SpotEditorControl), new PropertyMetadata(1.0));

        public double ImageOriginalHeight
        {
            get => (double)GetValue(ImageOriginalHeightProperty);
            set => SetValue(ImageOriginalHeightProperty, value);
        }
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Image image && image.Source is BitmapSource bitmap)
            {
                ImageOriginalWidth = bitmap.PixelWidth;
                ImageOriginalHeight = bitmap.PixelHeight;
            }
        }
        public static readonly DependencyProperty VideoViewModelCollectionProperty =
    DependencyProperty.Register(nameof(VideoViewModelCollection), typeof(IEnumerable<VideoViewModel>), typeof(SpotEditorControl), new PropertyMetadata(null));

        public IEnumerable<VideoViewModel> VideoViewModelCollection
        {
            get => (IEnumerable<VideoViewModel>)GetValue(VideoViewModelCollectionProperty);
            set => SetValue(VideoViewModelCollectionProperty, value);
        }

    }

}
