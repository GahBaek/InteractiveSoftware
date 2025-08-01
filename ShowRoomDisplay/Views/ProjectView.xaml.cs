using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShowRoomDisplay.Views
{
    /// <summary>
    /// ProjectView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
        }

        #region Dependency Property
        public static DependencyProperty ProjectNameProerty = DependencyProperty.Register("ProjectName", typeof(string), typeof(ProjectView));
        public string ProjectName
        {
            get => (string)GetValue(ProjectNameProerty);
            set => SetValue(ProjectNameProerty, value);
        }


        public static DependencyProperty ProjectCreateTimeProerty = DependencyProperty.Register("ProjectCreateTime", typeof(string), typeof(ProjectView));
        public string ProjectCreateTime
        {
            get => (string)GetValue(ProjectCreateTimeProerty);
            set => SetValue(ProjectCreateTimeProerty, value);
        }

        public static DependencyProperty DemoVideoPathProerty = DependencyProperty.Register("DemoVideoPath", typeof(string), typeof(ProjectView));
        public string DemoVideoPath
        {
            get => (string)GetValue(DemoVideoPathProerty);
            set => SetValue(DemoVideoPathProerty, value);
        }

        public static DependencyProperty ProjectImagePathProerty = DependencyProperty.Register("ProjectImagePath", typeof(string), typeof(ProjectView));
        public string ProjectImagePath
        {
            get => (string)GetValue(ProjectImagePathProerty);
            set => SetValue(ProjectImagePathProerty, value);
        }

        public static DependencyProperty OpenDemoVideoPathCommandProperty = DependencyProperty.Register("OpenDemoVideoPathCommand", typeof(ICommand), typeof(ProjectView));
        public ICommand OpenDemoVideoPathCommand
        {
            get => (ICommand)GetValue(OpenDemoVideoPathCommandProperty);
            set => SetValue(OpenDemoVideoPathCommandProperty, value);
        }


        public static DependencyProperty OpenProjectImagePathCommandProperty = DependencyProperty.Register("OpenProjectImagePathCommand", typeof(ICommand), typeof(ProjectView));
        public ICommand OpenProjectImagePathCommand
        {
            get => (ICommand)GetValue(OpenProjectImagePathCommandProperty);
            set => SetValue(OpenProjectImagePathCommandProperty, value);
        }
        #endregion
    }
}
