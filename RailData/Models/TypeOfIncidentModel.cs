using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RailData.Models
{
    public class TypeOfIncidentModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}