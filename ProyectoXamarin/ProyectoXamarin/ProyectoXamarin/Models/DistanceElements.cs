using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class DistanceElements
    {
        [JsonProperty("elements")]
        public List<DistanceDuration> DistanceDurations;
    }
}
