using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

    }
}
