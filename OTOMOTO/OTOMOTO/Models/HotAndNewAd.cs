using System;
using System.Collections.Generic;
using System.Text;

namespace OTOMOTO.Models
{
    public class HotAndNewAd
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public bool IsFeatured { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => $"http://ccvehicle.azurewebsites.net/{ImageUrl}";

    }
}
