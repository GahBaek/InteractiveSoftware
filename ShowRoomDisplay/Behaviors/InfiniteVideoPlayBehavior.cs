using ConvMVVM2.WPF.Behaviors.Base;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShowRoomDisplay.Behaviors
{
    public class InfiniteVideoPlayBehavior : Behavior<MediaElement>
    {
        #region Protected Functions
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.LoadedBehavior = MediaState.Manual;
            this.AssociatedObject.UnloadedBehavior = MediaState.Stop;
            this.AssociatedObject.Stretch = System.Windows.Media.Stretch.Uniform;

            this.AssociatedObject.MediaEnded += AssociatedObject_MediaEnded;

            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
            this.AssociatedObject.Unloaded += AssociatedObject_Unloaded;

        }



        protected override void OnDetaching()
        {
            base.OnDetaching();
            

            this.AssociatedObject.MediaEnded -= AssociatedObject_MediaEnded;

            this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
            this.AssociatedObject.Unloaded -= AssociatedObject_Unloaded;

        }
        #endregion

        #region Dependency Property
        public static DependencyProperty VideoPathProperty = DependencyProperty.Register("VideoPath", typeof(string), typeof(InfiniteVideoPlayBehavior), new UIPropertyMetadata(OnVideoPathChanged));
        public string VideoPath
        {
            get => (string)GetValue(VideoPathProperty);
            set => SetValue(VideoPathProperty, value);
        }
        #endregion

        #region Event Handler

        public static void OnVideoPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {

            if (d == null) return;

            var behaivor = d as InfiniteVideoPlayBehavior;

            if (args.NewValue == null)
            {
                try
                {
                    behaivor.AssociatedObject.Stop();
                    return;
                }
                catch(Exception ex)
                {
                    return;
                }
    
            }

            

            var path = (string)args.NewValue;
            if (path == "")
            {
                try
                {
                    behaivor.AssociatedObject.Stop();
                    return;
                }
                catch (Exception ex)
                {
                    return;
                }
            }



            behaivor.AssociatedObject.Source = new Uri(path);
            behaivor.AssociatedObject.Play();

        }

        private void AssociatedObject_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {

            try
            {
                this.AssociatedObject.Position = TimeSpan.Zero;
                this.AssociatedObject.Play();

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }


        }

        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.AssociatedObject.Play();

            }
            catch(Exception ex)
            {

            }

   
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.AssociatedObject.Stop();
                this.AssociatedObject.Source = null;
               

            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
