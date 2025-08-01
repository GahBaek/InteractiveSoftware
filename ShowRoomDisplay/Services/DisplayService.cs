using ShowRoomDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Services
{
    
    public class DisplayService : IDisplayService
    {
        #region Private Property
        private Project _CurrentProject = null;
        private Video _CurrentVideo = null;
        #endregion

        #region Constructor
        public DisplayService()
        {

        }

        #endregion

        #region Event
        public event Action<Project> OnProjectChanged;
        public event Action OnProjectCleaned;

        public event Action<Video> OnVideoChanged;
        public event Action OnVideoCleaned;
        #endregion

        #region Public Property
        public Project CurrentProject
        {
            get => _CurrentProject;
            private set => _CurrentProject = value;
        }

        public Video CurrentVideo
        {
            get => _CurrentVideo;
            private set => _CurrentVideo = value;
        }
        #endregion

        #region Public Functions



        public void CleanProject()
        {

            this.OnProjectCleaned?.Invoke();

        }

        public void SwitchProject(Project project)
        {

            this.CurrentProject = project;
            this.OnProjectChanged?.Invoke(project);


        }


        public void CleanVideo()
        {

            this.OnVideoCleaned?.Invoke();

        }

        public void SwitchVideo(Video video)
        {

            this.CurrentVideo = video;
            this.OnVideoChanged?.Invoke(video);


        }


        #endregion
    }
}
