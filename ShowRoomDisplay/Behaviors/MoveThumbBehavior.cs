using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowRoomDisplay.Models;
using System.Windows;
using System.Windows.Controls.Primitives;
using ConvMVVM2.WPF.Behaviors.Base;
using ShowRoomDisplay.ViewModels;

namespace ShowRoomDisplay.Behaviors
{
    public class MoveThumbBehavior : Behavior<Thumb>
    {
        public static readonly DependencyProperty VideoProperty =
      DependencyProperty.Register(nameof(Video), typeof(VideoViewModel), typeof(MoveThumbBehavior));

        public VideoViewModel Video
        {
            get => (VideoViewModel)GetValue(VideoProperty);
            set => SetValue(VideoProperty, value);
        }

        public static readonly DependencyProperty IsEditableProperty =
    DependencyProperty.Register(nameof(IsEditable), typeof(bool), typeof(MoveThumbBehavior), new PropertyMetadata(false));

        public bool IsEditable
        {
            get => (bool)GetValue(IsEditableProperty);
            set => SetValue(IsEditableProperty, value);
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
            if (!IsEditable)
                return;
            var video = this.Video ?? this.AssociatedObject.DataContext as VideoViewModel;

            if (video == null) return;

            video.X += e.HorizontalChange;
            video.Y += e.VerticalChange;
        }
    }
}
