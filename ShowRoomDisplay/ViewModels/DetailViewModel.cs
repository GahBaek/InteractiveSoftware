using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using ShowRoomDisplay.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.ViewModels
{
    public partial class DetailViewModel : ViewModelBase, IServiceInitializable
    {
        #region Private Property
        private readonly IDisplayService displayService;
        #endregion

        #region Constructor
        public DetailViewModel(IDisplayService displayService)
        {

            this.displayService = displayService;
        }


        #endregion

        #region Public Property
        [Property]
        private string _VideoPath = "";

        #endregion


        #region Event Handler
        public void OnServiceInitialized()
        {

            this.displayService.OnVideoChanged += DisplayService_OnVideoChanged;
            this.displayService.OnVideoCleaned += DisplayService_OnVideoCleaned;


            if (this.displayService.CurrentVideo != null)
                this.VideoPath = this.displayService.CurrentVideo.VideoPath;
        }

        private void DisplayService_OnVideoCleaned()
        {

            this.VideoPath = "";
        }

        private void DisplayService_OnVideoChanged(Models.Video video)
        {
            this.VideoPath = video.VideoPath;
        }
        #endregion
    }
}
