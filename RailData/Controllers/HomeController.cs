using RailData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RailData.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult IncidentListView()
        {
            var db = new RailDataEntities();
                ViewBag.Message = "Incident List";
            List<ListViewIncidentModel> lvmodel = new List<ListViewIncidentModel>();
            foreach (var inc in db.Incident.ToList())
            {
                var item = new ListViewIncidentModel();
                item.Incident = inc;
                item.IncidentImage = db.IncidentImage.FirstOrDefault(x => x.IncidentId == item.Incident.Id);


                lvmodel.Add(item);
            }

            return View(lvmodel);
        }

        public ActionResult IncidentView(int id)
        {
            var db = new RailDataEntities();
            ViewBag.Message = "Incident List.";
            IncidentDetailView incView = new IncidentDetailView();
            incView.inc = db.Incident.FirstOrDefault(x => x.Id == id);
            incView.img = db.IncidentImage.FirstOrDefault(x => x.IncidentId == incView.inc.Id);
            return View(incView);
        }

        public ActionResult DownloadImage(int id)
        {
            var db = new RailDataEntities();
            byte[] imageData = db.IncidentImage.FirstOrDefault(o => o.IncidentId == id).ImageByteArray;
            return File(imageData, "image/png");
        }


        public ActionResult PostAndShow(int id)
        {
            var db = new RailDataEntities();
            var longitude= db.GISPointOfInterest.FirstOrDefault(x => x.IncidentId == id).Longitude;
            var latitude= db.GISPointOfInterest.FirstOrDefault(x => x.IncidentId == id).Latitude;
            return Redirect(string.Format("https://maps.google.com/?q={0},{1}&output=embed", latitude.ToString("0.000000000000000", System.Globalization.CultureInfo.InvariantCulture),longitude.ToString("0.000000000000000", System.Globalization.CultureInfo.InvariantCulture)));
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Participants Info.";

            return View();
        }
    }
}