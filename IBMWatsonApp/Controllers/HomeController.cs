using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IBMWatsonApp.Models;
using WatsonServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using GoogleMaps.LocationServices;
using IBMWatsonApp.Shared;
using Microsoft.AspNetCore.Http;

namespace IBMWatsonApp.Controllers
{
    public class HomeController : Controller
    {
        private string workspaceId = string.Empty;
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LabTest()
        {
            workspaceId = WatsonConstants.WorkSpaceId1;
            return View();
        }

        public IActionResult SymptomDriven(string message, string context)
        {
            workspaceId = WatsonConstants.WorkSpaceId2;
            //var address = "New York";
            //var locationService = new GoogleLocationService();
            //var point = locationService.GetLatLongFromAddress(address);
            //ViewData["Lat"] =  point.Latitude;
            //ViewData["Long"] = point.Longitude;
            workspaceId = WatsonConstants.WorkSpaceId2;
            return View();
        }

        public string GetReponse(string message, string context)
        {
            ConversationHelper helper = new ConversationHelper(WatsonConstants.WorkSpaceId1, WatsonConstants.Username, WatsonConstants.Password);
            var res = helper.GetResponse(message).GetAwaiter().GetResult();
            if (workspaceId == WatsonConstants.WorkSpaceId1)
            {
                if (context != null)
                {
                    JObject jResponse = JObject.Parse(res);
                    JObject jObject = (JObject)jResponse["context"];
                    if ((JValue)jObject["selected_type"] != null)
                        HttpContext.Session.SetString("selected_type", Convert.ToString((JValue)jObject["selected_type"]));
                    if ((JValue)jObject["formatted_selected_date"] != null)
                        HttpContext.Session.SetString("formatted_selected_date", Convert.ToString((JValue)jObject["formatted_selected_date"]));
                    if ((JValue)jObject["formatted_selected_time"] != null)
                        HttpContext.Session.SetString("formatted_selected_time", Convert.ToString((JValue)jObject["formatted_selected_time"]));
                    JValue email = (JValue)jObject["useremail"];
                    if (email != null)
                    {
                        LabTest labTest = new LabTest()
                        {
                            Testtype = HttpContext.Session.GetString("selected_type"),
                            Testdate = HttpContext.Session.GetString("formatted_selected_date"),
                            Testtime = HttpContext.Session.GetString("formatted_selected_time"),
                            Useremail = Convert.ToString(email),
                        };
                        new EmailService().SendMail(labTest);
                    }
                }
            }
            else if (workspaceId == WatsonConstants.WorkSpaceId2)
            {
                if (context != null)
                {
                    JObject jResponse = JObject.Parse(res);
                    JObject jObject = (JObject)jResponse["context"];
                    if ((JValue)jObject["selected_type"] != null)
                        HttpContext.Session.SetString("selected_type", Convert.ToString((JValue)jObject["selected_type"]));
                    if ((JValue)jObject["formatted_selected_date"] != null)
                        HttpContext.Session.SetString("formatted_selected_date", Convert.ToString((JValue)jObject["formatted_selected_date"]));
                    if ((JValue)jObject["formatted_selected_time"] != null)
                        HttpContext.Session.SetString("formatted_selected_time", Convert.ToString((JValue)jObject["formatted_selected_time"]));
                    JValue email = (JValue)jObject["useremail"];
                    if (email != null)
                    {
                        LabTest labTest = new LabTest()
                        {
                            Testtype = HttpContext.Session.GetString("selected_type"),
                            Testdate = HttpContext.Session.GetString("formatted_selected_date"),
                            Testtime = HttpContext.Session.GetString("formatted_selected_time"),
                            Useremail = Convert.ToString(email),
                        };
                        new EmailService().SendMail(labTest);
                    }
                }
            }
            return res.ToString();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
