using LoadHTMLDataFromAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Web;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using HtmlAgilityPack;
using TheArtOfDev.HtmlRenderer.WinForms;
using LoadHTMLDataFromAPI.Filter;

namespace LoadHTMLDataFromAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }



        // GET api/values/5
        [HttpGet]
        //[CompressFilter]
        public HttpResponseMessage Get(int id)
        {
            List<Product> pro = new List<Product>();
            List<Items> items = new List<Items>();
            items.Add(new Items { Id = 1, Name = "haha" });
            items.Add(new Items { Id = 2, Name = "gaga" });
            pro.Add(new Product { Id = 1, OrderNumber = "12345", Items = items });
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/Reports/22.html")))

            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{data}", new JavaScriptSerializer().Serialize(pro)); //replacing the required things  


            //var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
            //var jpegBytes = htmlToImageConv.GenerateImageFromFile(System.Web.HttpContext.Current.Server.MapPath("~/Reports/22.html"), "jpeg");
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(body);
            string base64String = Convert.ToBase64String(plainTextBytes);

            return Request.CreateResponse(HttpStatusCode.OK, base64String); ;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
