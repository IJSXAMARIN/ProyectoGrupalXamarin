using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Geolocalizacion : ContentPage
    {
        public Geolocalizacion()
        {
            InitializeComponent();
        }

        public async Task<T> GetLoc()
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

                if (location != null)
                {
                    if (!location.IsFromMockProvider)
                    {
                        return (T)Convert.ChangeType(($"Latitude: {location.Latitude}," +
                        $" Longitude: {location.Longitude}," +
                        $" Altitude: {location.Altitude}"), typeof(T));
                    }
                    return null;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}