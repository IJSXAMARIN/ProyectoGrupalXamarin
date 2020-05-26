using ProyectoXamarin.Maps;
using ProyectoXamarin.Models;
using ProyectoXamarin.Services;
using ProyectoXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PruebasMapa : ContentPage
    {

        private PlacesViewModel viewModel;

        public PruebasMapa()
        {
            InitializeComponent();

            ApiCities.Prueba();
            this.viewModel = new PlacesViewModel();


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
            };
        }

        public async Task SetPlacesMap()
        {


            // var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            //   MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.416883, -3.703567),Distance.FromMeters(5000)));

            Circle circle = new Circle
            {
                // TODO: La posición es la geolocation del móvil
                Center = new Position(40.416883, -3.703567),
                Radius = new Xamarin.Forms.Maps.Distance(1000),
                StrokeColor = Color.FromHex("#88FF0000"),
                StrokeWidth = 8,
                FillColor = Color.FromHex("#88FFC0CB")
            };

            MyMap.MapElements.Add(circle);

            // TODO: La posición es la geolocation del móvil
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(40.416883, -3.703567), Xamarin.Forms.Maps.Distance.FromMeters(2000)));

            await this.viewModel.GetPlaces(new Position(1.0, 1.0));

            foreach (Place place in this.viewModel.Places)
            {

                CustomPin pin = new CustomPin();
                pin.Address = place.Vicinity;
                pin.Position = new Position(place.Geolocation.Location.Latitude, place.Geolocation.Location.Longitude);
                pin.Label = place.PlaceName;
                pin.AutomationId = place.PlaceId;
                pin.Visitado = place.Visitado;
              //  pin.Type = PinType.SavedPin;
              
                pin.MarkerClicked += async (s, args) => { await Pin_MarkerClicked(s, args); };
                MyMap.Pins.Add(pin);

            }
        }


        private async Task Pin_MarkerClicked(object sender, PinClickedEventArgs e)
        {

            string placeId = ((Pin)sender).AutomationId;

            Place _place = this.viewModel.GetPlace(placeId);


            // Comprobar si ya se ha "descubierto". Si lo tenemos guardado en la database
            _place = await this.viewModel.LoadImagePlace(_place);


            PlaceDetailsModal details = new PlaceDetailsModal();

            PlaceDetailViewModel _viewModel = new PlaceDetailViewModel();
            //_viewModel.PlaceDetail = (PlaceDetails)_place;

            _viewModel.PlaceDetail = new PlaceDetails()
            {
                // TODO: LLamar bbdd y ver si ya está guardado

                Descubierto = false,
                Geolocation = _place.Geolocation,
                Photos = _place.Photos,
                Id = _place.Id,
                PlaceId = _place.PlaceId,
                PlaceName = _place.PlaceName,
                Vicinity = _place.Vicinity,

            };

            details.BindingContext = _viewModel;

            await Navigation.PushModalAsync(details, true);
        }




        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            Task.Run(() => this.SetPlacesMap().Wait());
        }
    }
}