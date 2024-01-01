using BloodManagementSystem_MVC_.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BloodManagementSystem_MVC_.Controllers
{
    public class AdminsController : Controller
    {
        string Baseurl = @"http://localhost:53746/";
        // GET: Admins
        public async Task<ActionResult> Index()
        {
            List<Admin> donList = null;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromdonor = await client.GetAsync("api/Donors/");
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromdonor.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var donResponse = ResFromdonor.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        donList = JsonConvert.DeserializeObject<List<Admin>>(donResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            //returning the employee list to view  
            return View(donList);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin data)
        {
            using (var client = new HttpClient())
            {

                string id = data.AdminId;
                string password = data.Adminpassword;

                //HTTP GET
                var responseTask = client.GetAsync(Baseurl + string.Format("api/Admins?id={0}&password={1}", id, password));
                responseTask.Wait();
                var result = responseTask.Result;
                Admin don = null;
                if (result.IsSuccessStatusCode)
                {
                    var donResponse = result.Content.ReadAsStringAsync().Result;
                    don = JsonConvert.DeserializeObject<Admin>(donResponse);
                    if (don != null) //web api sent error response 
                    {
                        //log response status here..
                        return RedirectToAction("Details", new { id = don.AdminId });
                    }
                }
                if (don == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credentials");
                }
                return View();
            }
        }

        public async Task<ActionResult> Details(string id)
        {
            Admin admin = null;
            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromseeker = await client.GetAsync("api/Admins/" + id);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromseeker.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var seekResponse = ResFromseeker.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        admin = JsonConvert.DeserializeObject<Admin>(seekResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            
            return View(admin);
        }
        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DonorsView()
        {
            return RedirectToAction("Index", "Donors");
        }
        public ActionResult SeekersView()
        {
            return RedirectToAction("Index", "Seekers");
        }
        public ActionResult StockView()
        {
            return RedirectToAction("Index", "Stocks");
        }
        public ActionResult RequestsView()
        {
            return RedirectToAction("Index", "Requests");
        }

    }
}