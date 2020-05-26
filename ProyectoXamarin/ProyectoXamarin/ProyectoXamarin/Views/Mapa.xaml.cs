using ProyectoXamarin.Models;
using ProyectoXamarin.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public ApiGooglePlaces api;
        List<Models.Place> places;
        public Mapa()
        {

            InitializeComponent();

            this.api = new ApiGooglePlaces();
            this.places = new List<Place>();

            // Mueve el mapa hasta las coordenadas (Position) y con un radio de 50Km (50000)


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