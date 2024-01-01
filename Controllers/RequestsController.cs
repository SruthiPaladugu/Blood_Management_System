using BloodManagementSystem_MVC_.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BloodManagementSystem_MVC_.Controllers
{
    public class RequestsController : Controller
    {
        string Baseurl = @"http://localhost:53746/";

        // GET:Requests
        public async Task<ActionResult> Index()
        {
            List<Request> reqList = null;
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
                    HttpResponseMessage ResFromseeker = await client.GetAsync("api/Requests/");
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromseeker.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var seekResponse = ResFromseeker.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        reqList = JsonConvert.DeserializeObject<List<Request>>(seekResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            //returning the employee list to view  
            return View(reqList);
        }

        //GET: Donors/id
        public async Task<ActionResult> Details(int id)
        {
            Request req = null;
            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromseeker = await client.GetAsync("api/Requests/" + id.ToString());

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromseeker.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var seekResponse = ResFromseeker.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        req = JsonConvert.DeserializeObject<Request>(seekResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            //returning the employee list to view  
            return View(req);
        }





        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Request req)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var json = JsonConvert.SerializeObject(req);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/Requests/PostRequest", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return View("Thankyou");
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return View(req);
            }
        }

        public ActionResult Thankyou()
        {
            return View();
        }


        public async Task<ActionResult> Delete(int id)
        {

            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromseeker = await client.DeleteAsync("api/Requests/" + id.ToString());
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromseeker.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   

                        var seekResponse = ResFromseeker.Content.ReadAsStringAsync().Result;
                        return View();
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }

            return RedirectToAction("Index");
        }


        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }



    }
}