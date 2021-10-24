using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RailData.Models
{
    public class IncidentImageModel
    {
        public string ImageBytes { get; set; }
        public string ImageName { get; set; }
        public DateTime DateInserted { get; set; }
        public DateTime TimeInserted { get; set; }
        public int IncidentId { get; set; }
    }
}