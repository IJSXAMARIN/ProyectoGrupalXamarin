using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class Place
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("name")]
        public String PlaceName { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonProperty("place_id")]
        public String PlaceId { get; set; }

        [JsonProperty("vicinity")]
        public String Vicinity { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geolocation { get; set; }
    }
}
