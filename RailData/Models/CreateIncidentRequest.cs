using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RailData.Models
{
    public class CreateIncidentRequest
    {
        public IncidentModel Incident { get; set; }
        public TypeOfIncidentModel IncidentType { get; set; }
        public GISPointOfInterestModel[] GeolocationPoint { get; set; }
        public ReporterTypeModel Reporter { get; set; }
        public IncidentImageModel[] Image { get; set; }

    }
}