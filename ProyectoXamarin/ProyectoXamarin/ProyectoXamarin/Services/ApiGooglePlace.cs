﻿using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Place>> GetPlaces()
        {

            JsonResultGoogleApi jsonResultGoogle = new JsonResultGoogleApi();

            String request = "/json?location=40.416883, -3.703567&radius=50000&type=museum&language=es&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";
            do
            {
                JsonResultGoogleApi _json = new JsonResultGoogleApi();

                // PageToken sirve para cargar los siguientes 20
                if (jsonResultGoogle.NextPageToken != null)
                {
                    request += "&pagetoken=" + jsonResultGoogle.NextPageToken;
                }
                _json = await this.CallApi<JsonResultGoogleApi>(this.globalUrl + request);

                if (_json != null)
                {
                    jsonResultGoogle.Places.AddRange(_json.Places);

                    // Descomentar siguiente linea para que llame a todos
                    //  json.NextPageToken = _json.NextPageToken;

                }
                else
                {
                    jsonResultGoogle.NextPageToken = null;
                }



            } while (jsonResultGoogle.NextPageToken != null);

            return jsonResultGoogle.Places;

        }

        public async Task<T> CallApi<T>(String request)
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


        public async Task PlaceDetail(String placeId)
        {

        }


        public async Task<Stream> LoadImagesPlace(String photoReference)
        {
            using (HttpClient client = new HttpClient())
            {
                // client.BaseAddress = new Uri(this.globalUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                //  client.DefaultRequestHeaders.Accept.Add(header);


                // photoReference = "CmRaAAAAzlJhPzrElPgqR9i_kjgvUxCr8pQlUQw4OHtUGFXLggySP0u7tmgEZdghDl6ayAAhV_w3bBal_7ibJr1424ZiOOkz8smWTJPPxU01PIh0jpf7W9mQA9wXxov6PPD1kkppEhDCfC0Zzffu2aq5xYsIz0wuGhT-peQkb8YQjNWnCfPtvk0Vu6IWuA&key=AIzaSyALJ3CYARJGQksqawJZuGwSF6p6rkoSIeM";


                HttpResponseMessage response = await client.GetAsync("https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + photoReference);

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
    }
}