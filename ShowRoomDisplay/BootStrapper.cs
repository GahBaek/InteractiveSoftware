using ConvMVVM2.Core.MVVM;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.Services;
using ShowRoomDisplay.ViewModels;
using ShowRoomDisplay.Views;
using ShowRoomDisplay.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay
{
    public class BootStrapper : AppBootstrapper
    {
        protected override void OnStartUp(IServiceContainer container)
        {

        }

        protected override void RegionMapping(IRegionManager layerManager)
        {

            layerManager.Mapping<DashView>("MainContent");
            layerManager.Mapping<MainView>("MainView");

        }

        protected override void RegisterModules()
        {

        }

        protected override void RegisterServices(IServiceCollection serviceCollection)
        {

            //Windows
            serviceCollection.AddSingleton<MainWindowView>();
            serviceCollection.AddInstance<VideoWindowView>();
            serviceCollection.AddInstance<CommonWindowView>();


            //Views

            serviceCollection.AddSingleton<MainView>();
            serviceCollection.AddSingleton<SettingView>();
            serviceCollection.AddSingleton<DashView>();
            serviceCollection.AddSingleton<CheckMonitorConnectionView>();

            //WindowContent
            serviceCollection.AddInstance<DetailView>();
            serviceCollection.AddInstance<VisionView>();


            //ViewModels
            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<SettingViewModel>();
            serviceCollection.AddSingleton<DashViewModel>();
            serviceCollection.AddSingleton<CheckMonitorConnectionViewModel>();


            serviceCollection.AddSingleton<DetailViewModel>();
            serviceCollection.AddSingleton<VisionViewModel>();

            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddInstance<VideoViewModel>();
            serviceCollection.AddInstance<ProjectViewModel>();


            serviceCollection.AddSingleton<Func<Project, ProjectViewModel>>((serviceProvider) =>
            {
                return (model) =>
                {
                    var vm = serviceProvider.GetService<ProjectViewModel>();
                    vm.Project = model;

                    return vm;
                };
            });
            serviceCollection.AddSingleton<Func<List<Project>, List<ProjectViewModel>>>((serviceProvider) =>
            {
                return (models) =>
                {
                    var converter = serviceProvider.GetService<Func<Project, ProjectViewModel>>();
                    var vms = models.Select(model => converter(model)).ToList();
                    return vms;
                };
            });

            serviceCollection.AddSingleton<Func<Video, VideoViewModel>>((serviceProvider) =>
            {
                return (model) =>
                {
                    var vm = serviceProvider.GetService<VideoViewModel>();
                    vm.Video = model;
                    return vm;
                };
            });

            serviceCollection.AddSingleton<Func<List<Video>, List<VideoViewModel>>>((serviceProvider) =>
            {
                var factory = serviceProvider.GetService<Func<Video, VideoViewModel>>();
                return (models) =>
                {
                    var vms = models.Select(model => factory(model)).ToList();

                    return vms;
                };
            });

            //Services
            serviceCollection.AddSingleton<IDemonstrationService, DemonstrationService>();
            serviceCollection.AddSingleton<IDisplayService, DisplayService>();

        }

        protected override void ViewModelMapping(IViewModelMapper viewModelMapper)
        {

        }
    }
}
