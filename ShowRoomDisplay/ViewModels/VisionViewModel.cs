using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using DevExpress.Xpf.Editors.Helpers;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.ViewModels
{
    public partial class VisionViewModel : ViewModelBase, IServiceInitializable
    {
        #region Private Property
        private readonly IDisplayService displayService;
        #endregion

        #region Constructor
        public VisionViewModel(IDisplayService displayService) {

            this.displayService = displayService;
        }


        #endregion

        #region Public Property
        [Property]
        private string _DemoVideoPath = "";

        #endregion


        #region Event Handler
        public void OnServiceInitialized()
        {

            this.displayService.OnProjectChanged += DisplayService_OnProjectChanged;
            this.displayService.OnProjectCleaned += DisplayService_OnProjectCleaned;
           

            if(this.displayService.CurrentProject != null)
                this.DemoVideoPath = this.displayService.CurrentProject.DemoVideoPath;
        }

        private void DisplayService_OnProjectCleaned()
        {

            this.DemoVideoPath = "";
        }

        private void DisplayService_OnProjectChanged(Models.Project project)
        {
            this.DemoVideoPath = project.DemoVideoPath;
        }
        #endregion
    }
}
