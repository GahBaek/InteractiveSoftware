using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowRoomDisplay.Models
{
    public class MonitorInfoViewModel
    {
        public string Name { get; set; }  
        public string Resolution { get; set; } 
        public string Position { get; set; }   
        public bool IsPrimary { get; set; }
    }

}
