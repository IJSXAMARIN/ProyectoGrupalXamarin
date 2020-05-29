using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ProyectoXamarin.Services
{
    public class Geolocalizacion
    {
        public async Task<T> GetLoc<T>()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                //PASAR LA UBICACIÓN DEL USUARIO CON EL AWAIT DE ARRIBA
                //COGER LA LOCALIZACIÓN DEL MONUMENTO PARA MOSTRAR AL USUARIO
                //LOS METROS QUE LE FALTAN PARA LLEGAR (calcular distancia)
                //Location boston = new Location(42.358056, -71.063611);
                //Location sanFrancisco = new Location(37.783333, -122.416667);
                //double miles = Location.CalculateDistance(boston, sanFrancisco, DistanceUnits.Miles);
                Geoloc posicion = new Geoloc();
                if (location != null)
                {
                    if (location.IsFromMockProvider)
                    {
                        // location is from a mock provider
                        //Console.WriteLine($"Latitude: {location.Latitude}," +
                        //    $" Longitude: {location.Longitude}," +
                        //    $" Altitude: {location.Altitude}");
                        posicion.Latitud = location.Latitude;
                        posicion.Longitud = location.Longitude;

                        return (T)Convert.ChangeType(posicion, typeof(T));
                    }
                    return default(T);
                }
                return default(T);
            }
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Handle not supported on device exception
            //}
            //catch (FeatureNotEnabledException fneEx)
            //{
            //    // Handle not enabled on device exception
            //}
            //catch (PermissionException pEx)
            //{
            //    // Handle permission exception
            //}
            catch (Exception ex)
            {
                Console.WriteLine("----------------------------------ERROR----------------");
                Console.WriteLine(ex);
                // Unable to get location
                return (T)Convert.ChangeType(ex, typeof(T));
            }
        }
    }
}
