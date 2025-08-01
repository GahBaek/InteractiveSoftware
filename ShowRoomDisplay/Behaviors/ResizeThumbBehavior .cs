using ConvMVVM2.WPF.Behaviors.Base;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.ViewModels;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ShowRoomDisplay.Behaviors
{
    /*
    * spot resize Behavior 클래스 : ResizeThumbBehavior
    */
    public class ResizeThumbBehavior : ConvMVVM2.WPF.Behaviors.Base.Behavior<Thumb>
    {
        public static readonly DependencyProperty VideoProperty =
            DependencyProperty.Register(nameof(Video), typeof(VideoViewModel), typeof(ResizeThumbBehavior));

        public VideoViewModel Video
        {
            get => (VideoViewModel)GetValue(VideoProperty);
            set => SetValue(VideoProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DragDelta += OnDragDelta;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.DragDelta -= OnDragDelta;
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            var video = this.Video ?? this.AssociatedObject.DataContext as VideoViewModel;
            if (video == null) return;

            double newWidth = video.Width + e.HorizontalChange;
            double newHeight = video.Height + e.VerticalChange;

            video.Width = newWidth > 20 ? newWidth : 20;
            video.Height = newHeight > 20 ? newHeight : 20;
        }
    }
}