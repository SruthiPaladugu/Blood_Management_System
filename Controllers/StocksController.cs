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
    public class StocksController : Controller
    {
        // GET: Stocks
        string Baseurl = @"http://localhost:53746/";


        // GET: Stocks
        public async Task<ActionResult> Index()
        {
            List<Stock> stockList = null;
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
                    HttpResponseMessage ResFromstock = await client.GetAsync("api/Stocks/");
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromstock.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var stockResponse = ResFromstock.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        stockList = JsonConvert.DeserializeObject<List<Stock>>(stockResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }            
            return View(stockList);
        }

        

        public ActionResult LogOut()
        {
            return RedirectToAction("Index", "Home");
        }


        //GET: Stocks/id
        public async Task<ActionResult> Details(int id)
        {
            Stock s = null;
            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromstock = await client.GetAsync("api/Stocks/" + id.ToString());

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromstock.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var stockResponse = ResFromstock.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the list  
                        s = JsonConvert.DeserializeObject<Stock>(stockResponse);
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }
            }
            //returning the employee list to view  
            return View(s);
        }




        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Stock stock)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var json = JsonConvert.SerializeObject(stock);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/Stocks/PostStock", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return View("Thankyou");
                    }
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(500);
                }

                return View(stock);
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
                    HttpResponseMessage ResFromseeker = await client.DeleteAsync("api/Stocks/" + id.ToString());
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




        public async Task<PartialViewResult> SearchUsers(string searchText)
        {
            List<Stock> stockList = null;
            using (var client = new HttpClient())
            { //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    //Sending request to find web api REST service resource Get:Courses & Get:Enrollemnts using HttpClient  
                    HttpResponseMessage ResFromstock = await client.GetAsync("api/Stocks/");


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (ResFromstock.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var stResponse = ResFromstock.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the list  
                        stockList = JsonConvert.DeserializeObject<List<Stock>>(stResponse);


                    }
                }

                catch (Exception)
                {
                    //return new HttpStatusCodeResult(500);
                }
            }
            
            var result = stockList.Where(a => a.BloodGroup.ToLower().Contains(searchText));


            //returning the employee list to view  
            return PartialView("_GridView", result);
        }
    }
}