using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RailData.Models
{
    public class UploadImageRequest
    {
        public string ImageName { get; set; }
        public int IncidentId { get; set; }
        public string ImageBytes { get; set; }

    }
}