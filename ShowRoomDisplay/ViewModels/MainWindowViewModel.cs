using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        #region Private Property
        private readonly IDialogService dialogService;
        #endregion

        #region Constructor
        public MainWindowViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        #endregion

        #region Command
        [RelayCommand]
        public void PopupWindow(string windowName)
        {
            try
            {
                switch (windowName)
                {
                    case "Vision":
                        {
                            this.dialogService.Show("VideoWindowView", "VisionView", 500, 500);
                            break;
                        }

                    case "Detail":
                        {
                            System.Console.WriteLine("DetailView 진입 시도");
                            this.dialogService.Show("VideoWindowView", "DetailView", 500, 500);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {

            }

        }


        #endregion
    }
}
