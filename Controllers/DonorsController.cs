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
    public class DonorsController : Controller
    {
        string Baseurl = @"http://localhost:53746/";


        // GET: Donors
        public async Task<ActionResult> Index()
        {
            List<Donor> donList = null;
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
                        donList = JsonConvert.DeserializeObject<List<Donor>>(donResponse);
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

        //GET: Donors/id
        public async Task<ActionResult> Details(int id)
        {
            Donor donList = null;
            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromdonor = await client.GetAsync("api/Donors/" + id.ToString());

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromdonor.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var donResponse = ResFromdonor.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        donList = JsonConvert.DeserializeObject<Donor>(donResponse);
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
        public ActionResult Login(Donor data)
        {
            using (var client = new HttpClient())
            {

                int id = data.DonorId;
                int phone = data.DonorPhone;

                //HTTP GET
                var responseTask = client.GetAsync(Baseurl + string.Format("api/Donors?id={0}&phone={1}", id.ToString(), phone.ToString()));
                responseTask.Wait();

                var result = responseTask.Result;
                Donor don = null;
                if (result.IsSuccessStatusCode)
                {
                    var donResponse = result.Content.ReadAsStringAsync().Result;

                    don = JsonConvert.DeserializeObject<Donor>(donResponse);
                    if (don != null) //web api sent error response 
                    {
                        //log response status here..
                        return RedirectToAction("Details", new { id = don.DonorId });
                    }
                }
                if (don == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credentials");
                }
                return View();

            }

        }




        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Donor donor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var json = JsonConvert.SerializeObject(donor);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/Donors/PostDonor", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("NewPage", "Donors");
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return View(donor);
            }
        }

        public ActionResult NewPage()
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
                    HttpResponseMessage ResFromdonor = await client.DeleteAsync("api/Donors/" + id.ToString());
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromdonor.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   

                        var donResponse = ResFromdonor.Content.ReadAsStringAsync().Result;
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



        public ActionResult Edit(int id)
        {
            Donor donor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HTTP GET
                    var response = client.GetAsync("api/Donors/" + id.ToString());
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var don = result.Content.ReadAsStringAsync().Result;
                        donor = JsonConvert.DeserializeObject<Donor>(don);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return View(donor);
            }
        }

        public ActionResult Update(Donor donor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var json = JsonConvert.SerializeObject(donor);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    //HTTP Post
                    var response = client.PutAsync("api/Donors/"+donor.DonorId, content);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var don = result.Content.ReadAsStringAsync().Result;
                        donor = JsonConvert.DeserializeObject<Donor>(don);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("Index");
            }
        }
    }
}