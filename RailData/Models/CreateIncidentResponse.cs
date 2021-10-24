using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RailData.Models
{
    public class CreateIncidentResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public int Id { get; set; }
    }
}