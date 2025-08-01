using ShowRoomDisplay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Services
{
    public interface IDisplayService
    {
        #region Event
        public event Action<Project> OnProjectChanged;
        public event Action OnProjectCleaned;

        public event Action<Video> OnVideoChanged;
        public event Action OnVideoCleaned;
        #endregion

        #region Public Property
        public Project CurrentProject { get; }

        public Video CurrentVideo { get; }
        #endregion

        #region Public Functions
        public void SwitchProject(Project project);
        public void CleanProject();

        public void SwitchVideo(Video video);
        public void CleanVideo();
        #endregion
    }
}
