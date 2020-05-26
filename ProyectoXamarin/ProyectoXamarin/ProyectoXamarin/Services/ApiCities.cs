using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProyectoXamarin.Services
{
    public class ApiCities
    {

        // https://www.universal-tutorial.com/rest-apis/free-rest-api-for-country-state-city



        public static async void Prueba()
        {

            String url = "https://www.universal-tutorial.com/api/getaccesstoken";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();


                client.DefaultRequestHeaders.Add("api-token", "AuXnFjES43NqbdODZoc1anLtpO9op_9HsA7hqU56HJoxlbbNrMsUAzmsp6cqoZ0HhWQ");
                client.DefaultRequestHeaders.Add("user-email", "abc@gmail.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    String token = await response.Content.ReadAsStringAsync();

                    var t = JsonConvert.DeserializeObject(token);

                }
            }
        }

    }
}
