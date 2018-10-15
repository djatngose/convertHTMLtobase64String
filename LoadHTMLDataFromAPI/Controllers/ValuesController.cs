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

using LoadHTMLDataFromAPI.Filter;
using Pechkin;
using HiQPdf;
using System.IO.Compression;

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
        [CompressFilter]
        public string Get(int id)
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

            //var htmlToImageConv = new NReco.PdfGenerator.HtmlToPdfConverter();
            //var jpegBytes = htmlToImageConv.GeneratePdfFromFile(System.Web.HttpContext.Current.Server.MapPath("~/Reports/22.html"), body);
            Stream str = null;
            var htmlToImageConv = new NReco.PdfGenerator.HtmlToPdfConverter();
            // htmlToImageConv.Zoom = 1;
            htmlToImageConv.Size = NReco.PdfGenerator.PageSize.A4;
            htmlToImageConv.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
            
            var jpegBytes =    htmlToImageConv.GeneratePdfFromFile(System.Web.HttpContext.Current.Server.MapPath("~/Reports/22.html"), "");
            // var jpegBytes = convertHTMLToImage(body);

            //var jpegBytes = System.Text.Encoding.UTF8.GetBytes(body);
           string base64String = Convert.ToBase64String(jpegBytes);

//            byte[] bytes = Convert.FromBase64String(base64String);
//            System.IO.FileStream stream =
//new FileStream(System.Web.HttpContext.Current.Server.MapPath("~/Reports/33344.pdf"), FileMode.CreateNew);
//            System.IO.BinaryWriter writer =
//                new BinaryWriter(stream);
//            writer.Write(bytes, 0, bytes.Length);
//            writer.Close();
            return base64String;
        }
        public byte[] compressImg(byte[] inputBytes)
        {
            var jpegQuality = 85;
            Image image;
            Byte[] outputBytes;
            using (var inputStream = new MemoryStream(inputBytes))
            {
                image = Image.FromStream(inputStream);
                var jpegEncoder = ImageCodecInfo.GetImageDecoders()
                  .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, jpegQuality);
               
                using (var outputStream = new MemoryStream())
                {
                    image.Save(outputStream, jpegEncoder, encoderParameters);
                    outputBytes = outputStream.ToArray();
                }
            }
            return outputBytes;
        }
        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory,
                    CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
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
        public byte[] convertHTMLToImage(string body)
        {
            // create the HTML to Image converter 
            HtmlToImage htmlToImageConverter = new HtmlToImage();
            // set a demo serial number
            htmlToImageConverter.SerialNumber = "YCgJMTAE-BiwJAhIB-EhlWTlBA-UEBRQFBA-U1FOUVJO-WVlZWQ==";
            //// set browser width 
            //htmlToImageConverter.BrowserWidth = int.Parse(textBoxBrowserWidth.Text);

            //// set browser height if specified, otherwise use the default 
            //if (textBoxBrowserHeight.Text.Length > 0)
            //    htmlToImageConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

            //// set HTML Load timeout 
            //htmlToImageConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

            // set whether the resulted image is transparent (has effect only when the output format is PNG) 
            htmlToImageConverter.TransparentImage = false;

            // convert to image 
            System.Drawing.Image imageObject = null;
    
            var imageBuffer = htmlToImageConverter.ConvertHtmlToMemory(body, System.Web.HttpContext.Current.Server.MapPath("~/Reports/22.html"));
           // byte[] imageBuffer = GetImageBuffer(imageObject);

            return imageBuffer;


        }
        private byte[] GetImageBuffer(System.Drawing.Image imageObject)
        {
            // create a memory stream where to save the image
            System.IO.MemoryStream imageMemoryStream = new System.IO.MemoryStream();

            // save the image to memory stream
            imageObject.Save(imageMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            // get a copy of the image buffer to allow image disposing
            byte[] imageBuffer = new byte[imageMemoryStream.Length];
            Array.Copy(imageMemoryStream.GetBuffer(), imageBuffer, imageBuffer.Length);

            // close the memory stream
            imageMemoryStream.Close();

            return imageBuffer;
        }
    }
}
