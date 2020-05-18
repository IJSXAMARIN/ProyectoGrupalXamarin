using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoXamarin.Models
{
    public class Photo
    {
        [JsonProperty("photo_reference")]
        public String PhotoReference { get; set; }

        [JsonIgnore]
        public ImageSource Image { get; set; }
    }
}
