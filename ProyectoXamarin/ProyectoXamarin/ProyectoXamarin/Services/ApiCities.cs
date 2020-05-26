using Newtonsoft.Json;
using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoXamarin.Services
{
    public class ApiCities
    {

        // https://www.universal-tutorial.com/rest-apis/free-rest-api-for-country-state-city



        public  static async Task<String> GetToken()
        {

            String url = "https://www.universal-tutorial.com/api/getaccesstoken";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();


                client.DefaultRequestHeaders.Add("api-token", "7o6uMlE27DOXaHCpYbqRxLH1kAzGh9Oy2UekDzp7czuJ3Vyi5mGdsDN6FyYdkTxOLcY");
                client.DefaultRequestHeaders.Add("user-email", "d@d.com");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                HttpResponseMessage response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {

                    String json = await response.Content.ReadAsStringAsync();

                    var token = JsonConvert.DeserializeObject<Dictionary<string,string>>(json);

                    return token.FirstOrDefault().Value;
                }

                return "";
            }
        }

        public static  async Task<List<Country>> GetCountries() 
        {

            String token = await GetToken();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();


                String url = "https://www.universal-tutorial.com/api/countries/";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = client.GetAsync(url).Result;
                List<Country> countries = new List<Country>();

                if (response.IsSuccessStatusCode)
                {

                    countries = await response.Content.ReadAsAsync<List<Country>>();

                    return countries;
                }
                else 
                {
                    return countries;
                }

            }
        }

        public static async Task<List<State>> GetStates(String country)
        {

            String token = await GetToken();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();


                String url = "https://www.universal-tutorial.com/api/states/" + country;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = client.GetAsync(url).Result;
                List<State> states = new List<State>();

                if (response.IsSuccessStatusCode)
                {

                    states = await response.Content.ReadAsAsync<List<State>>();

                    return states;
                }
                else
                {
                    return states;
                }

            }
        }

        public static async Task<List<City>> GetCities(String state)
        {

            String token = await GetToken();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();


                String url = "https://www.universal-tutorial.com/api/cities/" + state;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = client.GetAsync(url).Result;
                List<City> cities = new List<City>();

                if (response.IsSuccessStatusCode)
                {

                    cities = await response.Content.ReadAsAsync<List<City>>();

                    return cities;
                }
                else
                {
                    return cities;
                }

            }
        }
    }
}
