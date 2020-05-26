using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class Duration
    {
        [JsonProperty("text")]
        public String Text { get; set; }


        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
