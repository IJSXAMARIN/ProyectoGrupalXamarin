using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class City
    {

        [JsonProperty("city_name")]
        public String CityName { get; set; }
    }
}
