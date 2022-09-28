using System;
using System.Collections.Generic;
using System.Text;

namespace OTOMOTO.Models
{
    public class Vehicle
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public string Engine { get; set; }
        public string Color { get; set; }
        public string Company { get; set; }
        public DateTime DatePosted { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public int Userid { get; set; }
        public int Categoryid { get; set; }
    }
}
