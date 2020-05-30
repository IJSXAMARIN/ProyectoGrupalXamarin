using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace ProyectoXamarin.Services
{
    public class ApiGooglePlaces
    {

     

        private readonly string globalUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch";

        private readonly string ApiKey = "AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";

        private readonly string fields = "fields=museum,tourist-attraction,point_of_interest";

        private readonly string coordenadasPuertaSol = "40.416883, -3.703567";

        private readonly string pruebaLocation = "location=40.416883, -3.703567";


        // "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=40.416883, -3.703567&radius=50000&type=museum&language=es&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM"
        // "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=CmRaAAAAzlJhPzrElPgqR9i_kjgvUxCr8pQlUQw4OHtUGFXLggySP0u7tmgEZdghDl6ayAAhV_w3bBal_7ibJr1424ZiOOkz8smWTJPPxU01PIh0jpf7W9mQA9wXxov6PPD1kkppEhDCfC0Zzffu2aq5xYsIz0wuGhT-peQkb8YQjNWnCfPtvk0Vu6IWuA&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM"

        string request = "";
        public ApiGooglePlaces()
        {
         

            this.request = pruebaLocation + "&" + fields + "&key=" + ApiKey;

        }

        public async Task<List<Place>> GetPlaces(Geoloc geo) // TODO: El método tiene que recibir -> Position position
        {
          //  Geoloc geo = await this.geo.GetLoc<Geoloc>();

            JsonResultGoogleApi jsonResultGoogle = new JsonResultGoogleApi();


            String request = "/json?location=" + geo.Latitud.ToString().Replace(',', '.') + "," + geo.Longitud.ToString().Replace(',', '.')
                + "&radius=5000&type=museum&language=es&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";
            do
            {
                JsonResultGoogleApi _json = new JsonResultGoogleApi();

                // PageToken sirve para cargar los siguientes 20
                if (jsonResultGoogle.NextPageToken != null)
                {
                    request += "&pagetoken=" + jsonResultGoogle.NextPageToken;
                }
                _json = await this.GetPlaces<JsonResultGoogleApi>(this.globalUrl + request);

                if (_json != null)
                {
                    jsonResultGoogle.Places.AddRange(_json.Places);

                    // Descomentar siguiente linea para que llame a todos
                    jsonResultGoogle.NextPageToken = _json.NextPageToken;

                }
                else
                {
                    jsonResultGoogle.NextPageToken = null;
                }



            } while (jsonResultGoogle.NextPageToken != null);

            return jsonResultGoogle.Places;

        }

        public async Task<T> GetPlaces<T>(String request)
        {

            using (HttpClient client = new HttpClient())
            {
                // client.BaseAddress = new Uri(this.globalUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                //  client.DefaultRequestHeaders.Accept.Add(header);


                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }


        //public async Task PlaceDetail(String placeId)
        //{

        //}


        public async Task<Stream> LoadImagesPlace(String photoReference)
        {
            using (HttpClient client = new HttpClient())
            {
        

                client.DefaultRequestHeaders.Accept.Clear();
            
                String url = "https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + photoReference + "&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    // Si devuelve un Status Code 403. Forbiden, es porque se ha superado el número de llamadas permitida y devuelve una imágen por defecto
                    // https://developers.google.com/places/web-service/photos?hl=es-419
                    Stream p = await response.Content.ReadAsStreamAsync();

                    return p;
                }


                else return null;

            }
        }

        public async Task<JsonResultGoogleApi> GetDistanceAsync(Position userPosition, Position placePosition) 
        {
           

                // units=metric => Para que devuelva en Km y metros
                // language=es 
                // key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM"
                // travel_mode=walking 

                String userLatitude = userPosition.Latitude.ToString().Replace(',','.');
                String userLongitude = userPosition.Longitude.ToString().Replace(',', '.');

                String placeLatitude = placePosition.Latitude.ToString().Replace(',', '.');
                String placeLongitude = placePosition.Longitude.ToString().Replace(',', '.');

                String url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=" + userLatitude + "," + userLongitude + "&destinations=" + placeLatitude + "," + placeLongitude + "&travel_mode=walking&language=es&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";

               

            using (HttpClient client = new HttpClient())
            {
            
                client.DefaultRequestHeaders.Accept.Clear();
             
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<JsonResultGoogleApi>();
                }
                else
                {
                    return new JsonResultGoogleApi();
                }
            }
        }

        public async Task<List<Place>> GetPlacesByCity(String city)
        {
            String url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + city + "&type=museum&language=es&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";


            JsonResultGoogleApi jsonResultGoogle = new JsonResultGoogleApi();

          
            do
            {
                JsonResultGoogleApi _json = new JsonResultGoogleApi();

                // PageToken sirve para cargar los siguientes 20
                if (jsonResultGoogle.NextPageToken != null)
                {
                    request += "&pagetoken=" + jsonResultGoogle.NextPageToken;
                }
                _json = await this.GetPlaces<JsonResultGoogleApi>(url);

                if (_json != null)
                {
                    jsonResultGoogle.Places.AddRange(_json.Places);

                    // Descomentar siguiente linea para que llame a todos
                    //jsonResultGoogle.NextPageToken = _json.NextPageToken;

                }
                else
                {
                    jsonResultGoogle.NextPageToken = null;
                }


            } while (jsonResultGoogle.NextPageToken != null);

            return jsonResultGoogle.Places;
        }

        public async Task<Position> GetPositionCity(String cityName)
        {
            String url = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=" + cityName + "&language=es&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";


            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    JsonResultGoogleApi json =  await response.Content.ReadAsAsync<JsonResultGoogleApi>();

                    return new Position(json.Places.FirstOrDefault().Geolocation.Location.Latitude, json.Places.FirstOrDefault().Geolocation.Location.Longitude);
                }
                else
                {
                    return new Position();
                }
            }
        }
       


    }
}

