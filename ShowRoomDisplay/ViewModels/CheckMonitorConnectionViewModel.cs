using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using ShowRoomDisplay.Models;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace ShowRoomDisplay.ViewModels
{
    public partial class CheckMonitorConnectionViewModel : ViewModelBase
    {

        public CheckMonitorConnectionViewModel()
        {
            LoadMonitors();
        }

        public ObservableCollection<MonitorInfoViewModel> Monitors { get; } = new();

        private void LoadMonitors()
        {
            Monitors.Clear();

            var screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];
                var info = new MonitorInfoViewModel
                {
                    Name = $"{(screen.Primary ? "주 모니터" : "외부 모니터")} {i + 1}",
                    Resolution = $"{screen.Bounds.Width} x {screen.Bounds.Height}",
                    Position = $"X={screen.Bounds.X}, Y={screen.Bounds.Y}",
                    IsPrimary = screen.Primary
                };
                Monitors.Add(info);
            }
        }


        [RelayCommand]
        public void RefreshMonitors()
        {
            LoadMonitors();
        }

    }
}
