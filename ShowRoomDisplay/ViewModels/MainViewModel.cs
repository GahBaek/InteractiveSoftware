using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {

        #region Private Property
        private readonly IRegionManager regionManager;
        private readonly IDialogService dialogService;
        #endregion

        #region Constructor
        public MainViewModel(IRegionManager regionManager,
                             IDialogService dialogService) { 
        
            this.regionManager = regionManager;
            this.dialogService = dialogService;
        
        }
        #endregion

        #region Public Property

        #endregion

        #region Command
        [RelayCommand]
        private void SwitchView(string viewName)
        {
            try
            {

                this.regionManager.Navigate("MainContent", viewName);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
