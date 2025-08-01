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
    public class ThumbMoveBehaivor : Behavior<Thumb>
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
        public static DependencyProperty VideoViewModelProperty = DependencyProperty.Register("VideoViewModel", typeof(VideoViewModel), typeof(ThumbMoveBehaivor));
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
                this.VideoViewModel.X += e.HorizontalChange;
                this.VideoViewModel.Y += e.VerticalChange;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
        #endregion
    }
}
