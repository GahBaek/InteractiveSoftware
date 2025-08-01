using ShowRoomDisplay.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Models
{
    public class Project
    {
        #region Private Property
        private List<Video> _Vidoes = new List<Video>();
        #endregion

        #region Public Property
        public string Name { get; set; } = "";
        public string DemoVideoPath { get; set; } = "";
        public string ImagePath { get; set; } = "";
        #endregion

        #region Collection
        public IReadOnlyList<Video> Videos
        {
            get => _Vidoes;
        }
        #endregion

        #region Event
        public event Action<Video> OnCreateVideo;

        public event Action<Video> OnDeleteVideo;

        #endregion

        #region Public Functions
        public Video CreateVideo(double x, double y, double width, double height, System.Windows.Media.Color color)
        {
            try
            {
                var video = new Video()
                {
                    Color = color,
                    Width = width,
                    Height = height,
                    X = x,
                    Y = y,
                };

                this._Vidoes.Add(video);
                this.OnCreateVideo?.Invoke(video);

                return video;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateVideo] 예외 발생: {ex.Message}");
                return null;
            }
        }



        public void DeleteVideo(Video video)
        {
            try
            {

                this._Vidoes.Remove(video);

                this.OnDeleteVideo(video);

            }catch(Exception ex)
            {

            }

        }

        public void UpdateVideos(List<Video> vidoes)
        {

            this._Vidoes = vidoes;
        }
        #endregion

        #region Converter
        public ProjectDto ToDto()
        {
            return new ProjectDto()
            {
                DemoVideoPath = DemoVideoPath,
                ImagePath = ImagePath,
                Name = Name,
                Videos = this.Videos.Select(video => video.ToDto()).ToList()
            };

        }

        public static Project Convert(ProjectDto dto)
        {
            var project = new Project()
            {
                ImagePath = dto.ImagePath,
                Name = dto.Name,
                DemoVideoPath = dto.DemoVideoPath,
            };

            var videos = dto.Videos.Select(dto => Video.Convert(dto)).ToList();

            project.UpdateVideos(videos);

            return project;
        }
        #endregion
    }
}
