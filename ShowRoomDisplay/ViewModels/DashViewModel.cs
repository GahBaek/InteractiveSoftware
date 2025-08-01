using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using DevExpress.Charts.Designer.Native;
using DevExpress.Mvvm.DataAnnotations;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.ViewModels
{
    public partial class DashViewModel : ConvMVVM2.Core.MVVM.ViewModelBase, IServiceInitializable
    {
            #region Private Property
            private readonly IDemonstrationService demonstrationService;
            private readonly IDisplayService displayService;
            private readonly IDialogService dialogService;
            private readonly Func<Project, ProjectViewModel> projectFactroy;
            private readonly Func<List<Project>, List<ProjectViewModel>> projectsFactroy;
            #endregion

            #region Constructor
            public DashViewModel(IDemonstrationService demonstrationService,
                                 IDisplayService displayService,
                                 IDialogService dialogService,
                                 Func<Project, ProjectViewModel> projectFactroy,
                                 Func<List<Project>, List<ProjectViewModel>> projectsFactroy)
            {

                this.demonstrationService = demonstrationService;
                this.displayService = displayService;
                this.dialogService = dialogService;

                this.projectFactroy = projectFactroy;
                this.projectsFactroy = projectsFactroy;
            }
            #endregion

            #region Collection
            [Property]
            private ObservableCollection<ProjectViewModel> _ProjectViewModelCollection = new ObservableCollection<ProjectViewModel>();
            public ObservableCollection<ProjectViewModel> ProjectList => _ProjectViewModelCollection;

        #endregion

        #region Public Property


        public IEnumerable<VideoViewModel> VideoViewModelCollection
    => SelectedProject?.VideoViewModelCollection ?? Enumerable.Empty<VideoViewModel>();

        [Property]
            private ProjectViewModel _SelectedProject = null;
        [Property]
        private string _ImagePath;
        public IEnumerable<VideoViewModel> Spots => SelectedProject?.VideoViewModelCollection ?? Enumerable.Empty<VideoViewModel>();

        #endregion

        #region Command

        [RelayCommand]
            public void StartProject()
            {

                if (this.SelectedProject == null) return;

                try
                {

                    var model = this.SelectedProject.Project;
                    this.displayService.SwitchProject(model);

                }
                catch (Exception ex)
                {

                }


            }

            #endregion

            #region Event Handler

            partial void OnSelectedProjectChanged(ProjectViewModel value)
        {
            if (value != null)
            {
                ImagePath = value.ImagePath;
                this.displayService.SwitchProject(value.Project);
            }
            OnPropertyChanged(nameof(Spots));
            OnPropertyChanged(nameof(VideoViewModelCollection));
        }
        public void OnServiceInitialized()
            {


                this.demonstrationService.OnCreateProject += DemonstrationService_OnCreateProject;
                this.demonstrationService.OnDeleteProject += DemonstrationService_OnDeleteProject;
                this.demonstrationService.OnLoadProject += DemonstrationService_OnLoadProject;
            this.demonstrationService.Load();
            this.DemonstrationService_OnLoadProject();
            }

        private void DemonstrationService_OnLoadProject()
        {
            foreach (var vm in _ProjectViewModelCollection)
                vm.Cleanup();

            _ProjectViewModelCollection.Clear();

            var models = demonstrationService.ProjectCollection;

            if (models == null || models.Count == 0)
            {
                Debug.WriteLine("[DashViewModel] No projects loaded.");
                return;
            }

            var vms = this.projectsFactroy(models);

            foreach (var vm in vms)
                _ProjectViewModelCollection.Add(vm);

            SelectedProject = null;
        }

        private void DemonstrationService_OnDeleteProject(Models.Project project)
            {

                var vms = this._ProjectViewModelCollection.Where(vm => vm.Project == project).ToList();
                foreach (var vm in vms)
                {
                    vm.Cleanup();
                    this._ProjectViewModelCollection.Remove(vm);
                }




            }

            private void DemonstrationService_OnCreateProject(Models.Project project)
            {

                var vm = this.projectFactroy(project);
                this._ProjectViewModelCollection.Add(vm);
            }
        #endregion


        [RelayCommand]
        private void SelectVideo(VideoViewModel vm)
        {
            if (vm == null)
            {
                Debug.WriteLine("[DashViewModel] SelectVideo: vm is null");
                return;
            }

            Debug.WriteLine($"[DashViewModel] SelectVideo: {vm.Video?.VideoPath}");
            foreach (var spot in Spots)
                spot.IsSelected = false;

            // 현재 선택 항목만 true
            vm.IsSelected = true;

            Debug.WriteLine($"[DashViewModel] SelectVideo: {vm.Video?.VideoPath}");
            displayService.SwitchVideo(vm.Video);
        }

    }
}
