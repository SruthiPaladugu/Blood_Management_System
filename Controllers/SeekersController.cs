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
    public class SeekersController : Controller
    {
        string Baseurl = @"http://localhost:53746/";


        // GET: Donors
        public async Task<ActionResult> Index()
        {
            List<Seeker> seekList = null;
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
                    HttpResponseMessage ResFromseeker = await client.GetAsync("api/Seekers/");
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromseeker.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var seekResponse = ResFromseeker.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        seekList = JsonConvert.DeserializeObject<List<Seeker>>(seekResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            //returning the employee list to view  
            return View(seekList);
        }

        //GET: Donors/id
        public async Task<ActionResult> Details(int id)
        {
            Seeker seeker = null;
            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromseeker = await client.GetAsync("api/Seekers/" + id.ToString());

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromseeker.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var seekResponse = ResFromseeker.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        seeker = JsonConvert.DeserializeObject<Seeker>(seekResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            //returning the employee list to view  
            return View(seeker);
        }
        public ActionResult ListOfStock()
        {
            return RedirectToAction("Index", "Stocks");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Seeker data)
        {
            using (var client = new HttpClient())
            {

                int id = data.SeekerId;
                int phone = data.SeekerPhone;

                //HTTP GET
                var responseTask = client.GetAsync(Baseurl + string.Format("api/Seekers?id={0}&phone={1}", id.ToString(), phone.ToString()));
                responseTask.Wait();

                var result = responseTask.Result;
                Seeker seek = null;
                if (result.IsSuccessStatusCode)
                {
                    var seekResponse = result.Content.ReadAsStringAsync().Result;

                    seek = JsonConvert.DeserializeObject<Seeker>(seekResponse);
                    if (seek != null) //web api sent error response 
                    {
                        //log response status here..
                        return RedirectToAction("Details", new { id = seek.SeekerId });
                    }
                }
                if (seek == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Credentials");
                }
                return View();

            }

        }


        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Seeker seeker)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var json = JsonConvert.SerializeObject(seeker);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/Seekers/PostSeeker", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("NewPage", "Seekers");
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return View(seeker);
            }
        }

        public ActionResult NewPage()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            Seeker donor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //HTTP GET
                    var response = client.GetAsync("api/Seekers/" + id.ToString());
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var don = result.Content.ReadAsStringAsync().Result;
                        donor = JsonConvert.DeserializeObject<Seeker>(don);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return View(donor);
            }
        }

        public ActionResult Update(Seeker seeker)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var json = JsonConvert.SerializeObject(seeker);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    //HTTP Post
                    var response = client.PutAsync("api/Seekers/" + seeker.SeekerId, content);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var don = result.Content.ReadAsStringAsync().Result;
                        seeker = JsonConvert.DeserializeObject<Seeker>(don);

                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return RedirectToAction("Index");
            }
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
                    HttpResponseMessage ResFromseeker = await client.DeleteAsync("api/Seekers/" + id.ToString());
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
    }
}