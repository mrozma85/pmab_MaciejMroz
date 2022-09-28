using System;
using System.Collections.Generic;
using System.Text;

namespace OTOMOTO.Models
{
    public class VehicleDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string Company { get; set; }
        public DateTime DatePosted { get; set; }
        public string Condition { get; set; }
        public bool IsFeatured { get; set; }
        public string Location { get; set; }
        public List<Image> Images { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string FullImageUrl => $"http://ccvehicle.azurewebsites.net/{ImageUrl}";
    }
    public class Image
    {
        public int Id { get; set; } 
        public string ImageUrl { get; set; }
        public int VehicleId { get; set; }
        public object ImageArray { get; set; }
        public string FullImageUrl => $"http://ccvehicle.azurewebsites.net/{ImageUrl}";
    }
}
