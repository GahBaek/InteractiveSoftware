using Newtonsoft.Json;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.Models.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Services
{
    public class DemonstrationService : IDemonstrationService
    {

        #region Constructor
        public DemonstrationService() {

            try
            {
                this.Load();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Public Property

        #endregion

        #region Event

        public event Action<Project> OnDeleteProject;
        public event Action<Project> OnCreateProject;
        public event Action OnLoadProject;
        #endregion

        #region Collection
        public List<Project> ProjectCollection { get; } = new List<Project>();
        #endregion


        #region Public Functions
        public void CreateProject(string projectName)
        {
            try
            {
                var project = new Project()
                {
                    Name = projectName,
                };

                this.ProjectCollection.Add(project);

                OnCreateProject?.Invoke(project);
                this.Save();
            }catch(Exception ex)
            {

            }
        }

        public void DeleteProject(Project project)
        {
            try
            {

                this.ProjectCollection.Remove(project);
          
                this.OnDeleteProject?.Invoke(project);
                this.Save();

            }
            catch (Exception ex)
            {

            }
        }

        public void Load()
        {
            try
            {
                var currentPath = AppDomain.CurrentDomain.BaseDirectory;
                var settingPath = Path.Combine(currentPath, "Settings");
                var jsonPath = Path.Combine(settingPath, "Project.json");

                var jsonContent = File.ReadAllText(jsonPath);

                var dtos = JsonConvert.DeserializeObject<List<ProjectDto>>(jsonContent);
                var models = dtos.Select(dto => Project.Convert(dto)).ToList();

                this.ProjectCollection.Clear();

                foreach (var model in models)
                    this.ProjectCollection.Add(model);

                this.OnLoadProject?.Invoke();

            }
            catch (Exception ex)
            {

            }
        }

        public void Save()
        {
            try
            {
                var dtos = this.ProjectCollection.Select(model => model.ToDto()).ToList();
                var jsonContent = JsonConvert.SerializeObject(dtos);

                var currentPath = AppDomain.CurrentDomain.BaseDirectory;
                var settingPath = Path.Combine(currentPath, "Settings");
                var jsonPath = Path.Combine(settingPath, "Project.json");

                if (!Directory.Exists(settingPath))
                    Directory.CreateDirectory(settingPath);


                File.WriteAllText(jsonPath, jsonContent);
                
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

    }
}
