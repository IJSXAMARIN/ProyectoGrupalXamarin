using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class Country
    {
        [JsonProperty("country_name")]
        public String CountryName { get; set; }

        [JsonProperty("country_short_name")]
        public String CountryShortName { get; set; }

        [JsonProperty("country_phone_code")]
        public String CountryPhoneCode { get; set; }
    }
}
