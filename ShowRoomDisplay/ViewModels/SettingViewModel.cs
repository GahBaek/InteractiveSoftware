using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using DevExpress.Xpo.DB;
using DevExpress.XtraRichEdit.Import.OpenXml;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.ViewModels
{
    public partial class SettingViewModel : ViewModelBase , IServiceInitializable
    {
        #region Private Property
        private readonly IDemonstrationService demonstrationService;
        private readonly IDisplayService displayService;
        private readonly Func<Project, ProjectViewModel> projectFactory;
        private readonly Func<List<Project>, List<ProjectViewModel>> projectsFactory;
        #endregion

        #region Constructor
        public SettingViewModel(IDemonstrationService demonstrationService,
                                IDisplayService displayService,
                                Func<Project, ProjectViewModel> projectFactory,
                                Func<List<Project>, List<ProjectViewModel>> projectsFactory) { 
        
            this.demonstrationService = demonstrationService;
            this.displayService = displayService;
            this.projectFactory = projectFactory;
            this.projectsFactory = projectsFactory;
        }
        #endregion

        #region Collection
        [Property]
        private ObservableCollection<ProjectViewModel> _ProjectViewModelCollection = new ObservableCollection<ProjectViewModel>();
        #endregion

        #region Public Property
        [Property]
        private string _NewProjectName = "";


        [Property]
        private ProjectViewModel _SelectedProject = null;
        #endregion

        #region Command
        [RelayCommand]
        public void CreateProject()
        {
            try
            {
                this.demonstrationService.CreateProject(this.NewProjectName);

            }
            catch(Exception ex)
            {

            }

        }


        [RelayCommand]
        public void DeleteProject()
        {
            try
            {
                if (this.SelectedProject == null) return;

                var model = this.SelectedProject.Project;

                this.demonstrationService.DeleteProject(model);

                this.SelectedProject = null;

            }
            catch (Exception ex)
            {

            }

        }

        [RelayCommand]
        public void StartProject()
        {

            if (this.SelectedProject == null) return;

            try
            {

                var model = this.SelectedProject.Project;
                this.displayService.SwitchProject(model);
               

            }catch(Exception ex)
            {
                
            }


        }

        [RelayCommand]
        public void LoadProject()
        {



            try
            {


                this.demonstrationService.Load();

            }
            catch (Exception ex)
            {

            }


        }



        [RelayCommand]
        public void SaveProject()
        {



            try
            {


                this.demonstrationService.Save();
                this.demonstrationService.Load();
            }
            catch (Exception ex)
            {

            }


        }

        #endregion

        #region Event Handler
        public void OnServiceInitialized()
        {
            this.demonstrationService.OnCreateProject += DemonstrationService_OnCreateProject;
            this.demonstrationService.OnDeleteProject += DemonstrationService_OnDeleteProject;
            this.demonstrationService.OnLoadProject += DemonstrationService_OnLoadProject;
        }

        private void DemonstrationService_OnLoadProject()
        {
            foreach(var vm in this.ProjectViewModelCollection)
            {
                vm.Cleanup();
            }

            this.ProjectViewModelCollection.Clear();
            var models = this.demonstrationService.ProjectCollection;
            var vms = this.projectsFactory(models);

            this.ProjectViewModelCollection = new ObservableCollection<ProjectViewModel>(vms);
            this.SelectedProject = null;
        }

        private void DemonstrationService_OnDeleteProject(Models.Project project)
        {
            var vms = this.ProjectViewModelCollection.Where(vm => vm.Project == project).ToList();
            foreach (var vm in vms)
            {
                vm.Cleanup();
                this.ProjectViewModelCollection.Remove(vm);
            }
        }

        private void DemonstrationService_OnCreateProject(Models.Project project)
        {
            var vm = this.projectFactory(project);
            this.ProjectViewModelCollection.Add(vm);
        }
        #endregion


        
    }
}
