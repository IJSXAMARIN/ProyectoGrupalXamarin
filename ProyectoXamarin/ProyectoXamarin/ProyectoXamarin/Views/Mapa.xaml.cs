using Plugin.Permissions;
using ProyectoXamarin.Models;
using ProyectoXamarin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Permissions.Abstractions;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Geolocalizacion geo;
        public ApiGooglePlaces api;
        List<Models.Place> places;
        public Mapa()
        {

            InitializeComponent();
            this.geo = new Geolocalizacion();

            this.api = new ApiGooglePlaces();
            this.places = new List<Place>();

            // Mueve el mapa hasta las coordenadas (Position) y con un radio de 50Km (50000)
            Task.Run(async () =>
            {
                AskForPermission();

                Geoloc loc =   await this.geo.GetLoc<Geoloc>();
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position
                (loc.Latitud, loc.Longitud), Xamarin.Forms.Maps.Distance.FromMeters(50000)));
            }).Wait();


            Circle circle = new Circle
            {
                Center = new Position(40.416883, -3.703567),
                Radius = new Xamarin.Forms.Maps.Distance(50000),
                StrokeColor = Color.FromHex("#88FF0000"),
                StrokeWidth = 8,
                FillColor = Color.FromHex("#88FFC0CB")
            };

            MyMap.MapElements.Add(circle);

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.416883, -3.703567), Xamarin.Forms.Maps.Distance.FromMeters(50000)));
            Task.Run(async () => await SetPlacesMap()).Wait();

            swithMap.Toggled += (sender, e) =>
            {
                if (e.Value)
                {
                    MyMap.MapType = MapType.Satellite;
                }
                else
                {
                    MyMap.MapType = MapType.Street;
                }
                // Perform an action after examining e.Value
            };
        }

        private void AskForPermission()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var status = await CrossPermissions.Current.
                    CheckPermissionStatusAsync(Permission.Location);
                    if (status != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.
                            RequestPermissionsAsync(Permission.Location);
                        if (results.ContainsKey(Permission.Location))
                        {
                            status = results[Permission.Location];
                        }
                    }
                    if (status == PermissionStatus.Granted)
                    {
                        await Navigation.PushModalAsync(new Mapa());
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("ERROR!!!!!!!!!!!");
                    Console.Write(ex);
                }
            });
        }

        public async Task SetPlacesMap()
        {

            //  Pin pin = new Pin();

            places = await api.GetPlaces();

            foreach (Place place in places)
            {
                Pin pin = new Pin();
                pin.Address = place.Vicinity;
                pin.Position = new Position(place.Geolocation.Location.Latitude, place.Geolocation.Location.Longitude);
                pin.Label = place.PlaceName;
                pin.AutomationId = place.PlaceId;
                pin.Type = PinType.Place;

                pin.MarkerClicked += async (s, args) =>
                {
                    args.HideInfoWindow = true;
                    string pinName = ((Pin)s).Label;

                    int index = MyMap.Pins.IndexOf(((Pin)s));
                    Place _place = this.places[index];


                    foreach (Photo photo in _place.Photos)
                    {
                        Stream stream = await this.api.LoadImagesPlace(photo.PhotoReference);
                        photo.Image = ImageSource.FromStream(() => stream);
                    }


                    PlaceDetailsModal details = new PlaceDetailsModal();
                    details.BindingContext = _place as Place;

                    await Navigation.PushModalAsync(details, true);

                   
                    // this.myImage.Source = ImageSource.FromStream(() => stream);
                    // await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");

                };
                swithMap.Toggled += (sender, e) =>
                {
                    if (e.Value)
                    {
                        MyMap.MapType = MapType.Satellite;
                    }
                    else
                    {
                        MyMap.MapType = MapType.Street;
                    }
                    // Perform an action after examining e.Value
                };
                MyMap.Pins.Add(pin);
            }



            // var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            //
            //   MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.416883, -3.703567),Distance.FromMeters(5000)));
        }

    }
}