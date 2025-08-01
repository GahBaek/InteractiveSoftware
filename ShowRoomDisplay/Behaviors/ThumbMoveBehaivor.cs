using ConvMVVM2.WPF.Behaviors.Base;
using ShowRoomDisplay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ShowRoomDisplay.Behaviors
{
    public class ThumbResizeBehaivor : Behavior<Thumb>
    {

        #region Protected Functions
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.DragDelta += AssociatedObject_DragDelta;
        }

  

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.DragDelta -= AssociatedObject_DragDelta;
        }
        #endregion

        #region Dependency Property
        public static DependencyProperty VideoViewModelProperty = DependencyProperty.Register("VideoViewModel", typeof(VideoViewModel), typeof(ThumbResizeBehaivor));
        public VideoViewModel VideoViewModel
        {
            get => (VideoViewModel)GetValue(VideoViewModelProperty);
            set => SetValue(VideoViewModelProperty, value);
        }
        #endregion

        #region Event Handler
        private void AssociatedObject_DragDelta(object sender, DragDeltaEventArgs e)
        {

            if (VideoViewModel == null) return;


            try
            {

                var width = this.VideoViewModel.Width;
                var height = this.VideoViewModel.Height;

                width += e.HorizontalChange;
                height += e.VerticalChange;

                if (width < 10)
                    width = 10;

                if(height < 10)
                    height = 10;

                this.VideoViewModel.Width = width;
                this.VideoViewModel.Height = height;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
