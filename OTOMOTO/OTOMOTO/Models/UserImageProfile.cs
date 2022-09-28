using System;
using System.Collections.Generic;
using System.Text;

namespace OTOMOTO.Models
{
    public class UserImageProfile
    {
        public string ImageUrl { get; set; }
        public string FullImagePath => $"http://ccvehicle.azurewebsites.net/{ImageUrl}";
    }
}
