using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class State
    {

        [JsonProperty("state_name")]
        public String StateName { get; set; }
    }
}
