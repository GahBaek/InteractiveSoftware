using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Models.DTOs
{
    public class ProjectDto
    {
        #region Public Property
        public string Name { get; set; } = "";
        public string DemoVideoPath { get; set; } = "";
        public string ImagePath { get; set; } = "";
        #endregion

        #region Collection
        public List<VideoDto> Videos { get; set; } = new List<VideoDto>();
        #endregion
    }
}
