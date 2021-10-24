using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RailData.Models
{
    public class IncidentModel
    {
        public DateTime IncidentDate { get; set; }
        public TypeOfIncident IncidentType { get; set; }
        public TypeOfReporter Reporter { get; set; }
        public string Description { get; set; }
    }
}