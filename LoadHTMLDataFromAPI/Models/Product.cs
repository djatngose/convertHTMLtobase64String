using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadHTMLDataFromAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public List<Items> Items { get; set; }
    }
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}