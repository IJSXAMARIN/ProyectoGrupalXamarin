using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class JsonResultGoogleApi
    {

        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonProperty("results")]
        public List<Place> Places { get; set; }


        [JsonProperty("rows")]
        public List<DistanceElements> DistanceRows { get; set; }

        public JsonResultGoogleApi()
        {
            this.Places = new List<Place>();
        }
    }
}
