using ConvMVVM2.Core.Attributes;
using ConvMVVM2.Core.MVVM;
using ConvMVVM2.WPF.ViewModels;
using ShowRoomDisplay.Models;
using ShowRoomDisplay.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShowRoomDisplay.ViewModels
{
    public partial class ProjectViewModel : ViewModelBase, ICleanup, IServiceInitializable
    {

        #region Private Property
        private readonly IDialogService dialogService;
        private readonly Func<Video, VideoViewModel> videoVMFactory;
        private readonly Func<List<Video>, List<VideoViewModel>> videoVMSFactory;
        #endregion

        #region Constructor
        public ProjectViewModel(IDialogService dialogService, Func<Video, VideoViewModel> videoVMFactory,
                                Func<List<Video>, List<VideoViewModel>> videoVMSFactory)
        {
            this.dialogService = dialogService;
            this.videoVMFactory = videoVMFactory;
            this.videoVMSFactory = videoVMSFactory;
        }
        #endregion


        #region Public Property
        [Property]
        public ObservableCollection<Video> _Spots = new();
        [Property]
        public Video _SelectedVideo;

        [Property]
        private string _SelectedColorText = "#FF0000";

        public double ViewportWidth { get; set; }
        public double ViewportHeight { get; set; }
        public double ImageWidth { get; set; }
        public double ImageHeight { get; set; }

        
        [Property]
        private Project _Project = null;

        [Property]
        private ObservableCollection<VideoViewModel> _VideoViewModelCollection = new ObservableCollection<VideoViewModel>();



        [Property]
        private ZoomAndPanViewModel _ZoomAndPanViewModel = new ZoomAndPanViewModel();


        [Property]
        private MouseViewModel _MouseViewModel = new MouseViewModel();
        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get => _ImageSource;
            set
            {
                _ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));

                // 이미지 크기 갱신
                if (value is BitmapSource bmp)
                {
                    ImageWidth = bmp.PixelWidth;
                    ImageHeight = bmp.PixelHeight;

                    OnPropertyChanged(nameof(ImageWidth));
                    OnPropertyChanged(nameof(ImageHeight));
                }
            }
        }


        public string Name
        {
            get => this.Project.Name;
            set
            {
                this.Project.Name = value;
                this.OnPropertyChanged(nameof(Name));
            }
        }

        public string DemoVideoPath
        {
            get => this.Project.DemoVideoPath;
            set
            {
                this.Project.DemoVideoPath = value;
                this.OnPropertyChanged(nameof(DemoVideoPath));
            }
        }
        public string ImagePath
        {
            get => Project?.ImagePath;
            set
            {
                if (Project != null)
                {
                    Project.ImagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }

        #endregion

        #region Public Functions
        public void Cleanup()
        {
            if (this.Project == null) return;

            this.Project.OnCreateVideo -= Project_OnCreateVideo;
            this.Project.OnDeleteVideo -= Project_OnDeleteVideo;
        }
        #endregion

        #region Event Handler
        partial void OnProjectChanged(Project project)
        {
            if(project == null)
            {
                return;
            }

            project.OnDeleteVideo += Project_OnDeleteVideo;
            project.OnCreateVideo += Project_OnCreateVideo;

            var vms = this.videoVMSFactory(project.Videos.ToList());
            this.VideoViewModelCollection = new ObservableCollection<VideoViewModel>(vms);
            this.Spots = new ObservableCollection<Video>(project.Videos);

            this.ImagePath = project.ImagePath;
            this.DemoVideoPath = project.DemoVideoPath;

            OnPropertyChanged(nameof(ImagePath));
            OnPropertyChanged(nameof(DemoVideoPath));
            OnPropertyChanged(nameof(Spots));

        }

        private void Project_OnCreateVideo(Video video)
        {
            var vm = this.videoVMFactory(video);
            this.VideoViewModelCollection.Add(vm);
        }

        private void Project_OnDeleteVideo(Video video)
        {
            var vms = this.VideoViewModelCollection.Where(vm => vm.Video == video).ToList();

            foreach (var vm in vms)
                this.VideoViewModelCollection.Remove(vm);
        }
        #endregion

        //BrowseImageCommand
        [RelayCommand]
        public void BrowseImage()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.ImagePath = openFileDialog.FileName;
            }
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(openFileDialog.FileName);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze(); 

            this.ImageSource = bitmap;
        }

        [RelayCommand]
        public void BrowseVideo()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Video Files (*.mp4;*.avi;*.mov)|*.mp4;*.avi;*.mov"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.DemoVideoPath = openFileDialog.FileName;
            }
        }
        [RelayCommand]
        private void SelectSpot(VideoViewModel videoVM)
        {
            foreach (var vm in this.VideoViewModelCollection)
                vm.IsSelected = false;

            if (videoVM != null)
                videoVM.IsSelected = true;

            this.SelectedVideo = videoVM.Video;
        }

        // AddSpot
        [RelayCommand]
        private void AddSpot()
        {
            var newVideo = Project.CreateVideo(100, 100, 50, 50, Colors.Red);
            this.SelectedVideo = newVideo;
        }



        // SelectVideo
        [RelayCommand]
        private void SelectVideo()
        {
            if (this.SelectedVideo == null) return;

            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Video Files (*.mp4;*.avi;*.mov)|*.mp4;*.avi;*.mov"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                this.SelectedVideo.VideoPath = openFileDialog.FileName;
                OnPropertyChanged(nameof(SelectedVideo));
            }
        }

        // DeleteSpot
        [RelayCommand]
        private void DeleteSpot()
        {
            if (SelectedVideo != null)
            {
                _Spots.Remove(SelectedVideo);
                Project?.DeleteVideo(SelectedVideo);

                SelectedVideo = null;
            }
        }

        // ChangeSpotColor
        [RelayCommand]
        private void ChangeSpotColor()
        {
            if (SelectedVideo == null || string.IsNullOrWhiteSpace(SelectedColorText))
                return;

            try
            {
                var color = (Color)ColorConverter.ConvertFromString(SelectedColorText);
                SelectedVideo.Color = color;
                OnPropertyChanged(nameof(SelectedVideo));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"색상 변환 실패: {ex.Message}");
            }
        }

        public void OnServiceInitialized()
        {
            this._MouseViewModel.PreviewWheelEvent += MouseViewModel_PreviewWheelEvent;
            this._MouseViewModel.WheelEvent += MouseViewModel_WheelEvent;

        }
        private void MouseViewModel_WheelEvent(System.Windows.Point point, bool isUp)
        {

            this._ZoomAndPanViewModel.Zoom(point.X, point.Y, isUp);
            this._ZoomAndPanViewModel.FitToViewPort(viewportWidth: this.ViewportWidth,
                                                    viewportHeight: this.ViewportHeight,
                                                    imageWidth: this.ImageWidth,
                                                    imageHeight: this.ImageHeight);
        }
        private void MouseViewModel_PreviewWheelEvent(System.Windows.Point point, bool isUp)
        {
            this._ZoomAndPanViewModel.Zoom(point.X, point.Y, isUp);
        }

    }
}
