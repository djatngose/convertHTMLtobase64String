using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LoadHTMLDataFromAPI.Controllers
{
    public class HomeController : Controller
    {

        private async Task<string> HttpClientCall()
        {
            //// Create a client
            //HttpClient httpClient = new HttpClient();

            //// Add a new Request Message
            //HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:56081/api/values/5");

            //// Add our custom headers
            //requestMessage.Headers.Add("Content-encoding", "gzip");
            //requestMessage.Headers.Add("Content-encoding", "application/json");

            //// Send the request to the server
            //HttpResponseMessage response = await httpClient.SendAsync(requestMessage);

            //// Just as an example I'm turning the response into a string here
            //string responseAsString = await response.Content.ReadAsStringAsync();
            //return responseAsString;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56081/");
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/values/5");


            await client.SendAsync(request)
                  .ContinueWith(responseTask =>
                  {
                     return responseTask.Result;
                  });
            return null;
        }
        public async  Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";
            string body = string.Empty;
        // var  s = await HttpClientCall();
            return View();
        }
    }
}
