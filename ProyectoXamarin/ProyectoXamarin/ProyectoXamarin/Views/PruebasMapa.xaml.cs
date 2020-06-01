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

            this.viewModel = new PlacesViewModel();

            Task.Run(async () => await SetPlacesMap(null)).Wait();

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

        public PruebasMapa(String cityName)
        {
            InitializeComponent();

            this.viewModel = new PlacesViewModel();

            Task.Run(async () => await SetPlacesMap(cityName)).Wait();

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

        public async Task SetPlacesMap(String cityName)
        {

            MyMap.Pins.Clear();

          
            Position position = new Position();

            if (String.IsNullOrEmpty(cityName))
            {
                await this.viewModel.GetPlaces(GeolocViewModel.Geo);

                position = new Position(GeolocViewModel.Geo.Latitud, GeolocViewModel.Geo.Longitud);

                this.footerLayout.IsVisible = true;

            }
            else
            {
                await this.viewModel.GetPlacesByCity(cityName);
                position = await this.viewModel.GetPositionCity(cityName);

                this.footerLayout.IsVisible = false;
            }

            if(String.IsNullOrEmpty(cityName))
            {
                Circle circle = new Circle
                {

                    Center = position,
                    Radius = new Xamarin.Forms.Maps.Distance(500),
                    StrokeColor = Color.FromHex("#88FF0000"),
                    StrokeWidth = 8,
                    FillColor = Color.FromHex("#88FFC0CB")
                };

                MyMap.MapElements.Add(circle);
            }
           

          
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Xamarin.Forms.Maps.Distance.FromMeters(2000)));

            foreach (Place place in this.viewModel.Places)
            {
                Pin pin = new Pin();
                pin.Address = place.Vicinity;
                pin.Position = new Position(place.Geolocation.Location.Latitude, place.Geolocation.Location.Longitude);
                pin.Label = place.PlaceName;
                pin.AutomationId = place.PlaceId;
                pin.Type = place.Visitado ? PinType.SavedPin : PinType.Place;
              //  pin.Visitado = place.Visitado;
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

            _viewModel.PlaceDetail = new PlaceDetails()
            {
                // TODO: LLamar bbdd y ver si ya está guardado
                Descubierto = _place.Visitado,
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
            Task.Run(() => this.SetPlacesMap(null).Wait());
        }

        private async void btnSimulacion_Clicked(object sender, EventArgs e)
        {
            //string coordenadasPuertaSol = "40.416883, -3.703567";
            Geoloc loc = new Geoloc();
            GeolocViewModel.Geo.Latitud = 40.416883;
            GeolocViewModel.Geo.Longitud = -3.703567;

            await this.SetPlacesMap(null);
        }
    }
}