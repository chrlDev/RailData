using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using RailData.Models;


namespace RailData.Controllers
{
    public class ApiController : Controller
    {
        [HttpPost]
        public string CreateIncident (CreateIncidentRequest request)
        {
            CreateIncidentResponse response = new CreateIncidentResponse();
            try
            {
                
                request.Incident.IncidentDate = DateTime.Now;
                using (var ctx = new RailDataEntities())
                {
                    var Entities = new RailDataEntities();
                    var incident = Entities.Incident.Create();
                    var typeOfReporter = Entities.TypeOfReporter.Create();
                    var typeOfIncident = Entities.TypeOfIncident.Create();
                    var incidentImage = Entities.IncidentImage.Create();
                    var gisLocation = Entities.GISPointOfInterest.Create();
                    var maxIncidentId = ctx.Incident.Count();
                    typeOfReporter = ctx.TypeOfReporter.FirstOrDefault(c=>c.Id==request.Reporter.Id);
                    if (ctx.TypeOfReporter.Any(o => o.Id == typeOfReporter.Id && o.ValidFrom > DateTime.Now && o.ValidTo < DateTime.Now))
                    {
                        response.Error = "Not Valid Reporter";
                        response.Success = false;
                        return JsonSerializer.Serialize(response);
                    }
                    else
                    {
                        typeOfReporter = ctx.TypeOfReporter.FirstOrDefault(x => x.Id == typeOfReporter.Id);
                    }
                    typeOfIncident.incidentId = request.IncidentType.Id;
                    if (ctx.TypeOfIncident.Any(o => o.incidentId == typeOfIncident.incidentId && o.ValidFrom > DateTime.Now && o.ValidTo < DateTime.Now))
                    {
                        response.Error = "Not Valid Type";
                        response.Success = false;
                        
                        return JsonSerializer.Serialize(response);
                    }
                    else
                    {
                        typeOfIncident = ctx.TypeOfIncident.FirstOrDefault(x => x.incidentId == typeOfIncident.incidentId);
                    }
                    incident.Id = maxIncidentId;
                    incident.IncidentType = typeOfIncident.incidentId;
                    incident.TypeOfReporter = typeOfReporter;
                    incident.IncidentDate = request.Incident.IncidentDate;
                    incident.Description = request.Incident.Description;
                    //incident.
                    var inc = ctx.Incident.Add(incident);
                    
                    ctx.SaveChanges();
                    response.Id = inc.Id;
                    foreach (var point in request.GeolocationPoint)
                    {
                        var maxgisLocationId = ctx.GISPointOfInterest.Count();
                        gisLocation.Longitude = point.Logitude;
                        gisLocation.Latitude = point.Latitude;
                        gisLocation.IncidentId = inc.Id;
                        gisLocation.Id = maxgisLocationId;
                        var gis = ctx.GISPointOfInterest.Add(gisLocation);
                        ctx.SaveChanges();
                    }

                    foreach (var pic in request.Image)
                    {
                        if (pic.ImageBytes.Length > 0)
                        {
                            var maxImageId = ctx.IncidentImage.Count();
                            incidentImage.DateInserted = DateTime.Now;
                            incidentImage.ImageName = pic.ImageName;
                            incidentImage.IncidentId = inc.Id;
                            byte[] newBytes = Convert.FromBase64String(pic.ImageBytes);
                            incidentImage.ImageByteArray = newBytes;
                            incidentImage.Id = maxImageId;
                            var picture = ctx.IncidentImage.Add(incidentImage);
                            ctx.SaveChanges();
                        }
                    }



                }
                response.Success = true;
                response.Error = "";
                return JsonSerializer.Serialize(response);
            }
            catch(Exception ex)
            {
                response.Error = ex.ToString();
                response.Success = false;
                return JsonSerializer.Serialize(response);
            }
        }

        [HttpPost]
        public string UploadImages(UploadImageRequest request)
        {
            var response = new UploadImageResponse();
            try
            {
                var Entities = new RailDataEntities();
                var incidentImage = Entities.IncidentImage.Create();
                using (var ctx = new RailDataEntities())
                {
                    incidentImage.DateInserted = DateTime.Now;
                    incidentImage.ImageName = request.ImageName;
                    var maxImageId = ctx.IncidentImage.Count();
                    incidentImage.Id = maxImageId;
                    byte[] newBytes = Convert.FromBase64String(request.ImageBytes);
                    incidentImage.ImageByteArray = newBytes;
                    incidentImage.IncidentId = request.IncidentId;
                    ctx.IncidentImage.Add(incidentImage);
                    ctx.SaveChanges();
                    response.Success = true;
                    response.Error = "";
                }
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = ex.ToString();
                return JsonSerializer.Serialize(response);
            }
            
        }

        [HttpGet]
        public string Test()
        {
            return "Success";
        }
    }
}
