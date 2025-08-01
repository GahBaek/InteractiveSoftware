using ShowRoomDisplay.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Services
{
    public interface IDemonstrationService
    {
        #region Collection
        List<Project> ProjectCollection { get; }
        #endregion

        #region Public Functions
        public void Save();

        public void Load();

        public void CreateProject(string projectName);
        public void DeleteProject(Project project);
        #endregion

        #region Event
        public event Action<Project> OnDeleteProject;

        public event Action<Project> OnCreateProject;

        public event Action OnLoadProject;
        #endregion
    }
}
