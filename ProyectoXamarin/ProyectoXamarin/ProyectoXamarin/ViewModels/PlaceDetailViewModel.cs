using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using ProyectoXamarin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ProyectoXamarin.ViewModels
{
    public class PlaceDetailViewModel : ViewModelBase
    {

        private ApiGooglePlaces api;

        public PlaceDetailViewModel()
        {
            this.api = new ApiGooglePlaces();
            this._DistanceDuration = new DistanceDuration()
            {
                Distance = new Models.Distance()
                {
                    Value = -1,
                    Text = "",
                },
                Duration = new Duration(),
            };

        }

        private DistanceDuration _DistanceDuration;

        public DistanceDuration DistanceDuration
        {
            get => this._DistanceDuration;
            set
            {
                this._DistanceDuration = value;
                OnPropertyChanged("DistanceDuration");
            }
        }

        private PlaceDetails _PlaceDetails;

        public PlaceDetails PlaceDetail
        {
            get => this._PlaceDetails;
            set
            {
                this._PlaceDetails = value;
                OnPropertyChanged("PlaceDetail");
            }
        }


        public void  GetDistanceAsync(Position userPosition, Position placePosition) {

            Task.Run(async() => {
            JsonResultGoogleApi json = await this.api.GetDistanceAsync(userPosition, placePosition);
                this.DistanceDuration = json.DistanceRows.FirstOrDefault().DistanceDurations.FirstOrDefault();
            }).Wait();
        }

        public  Command CalcularDistancia
        {
            get
            {
                return new Command(() =>
                {
                // TODO: Cambiar origin por posicion de geolocalizacion
                    Position origin = new Position(40.416883, -3.703567);
                    Position placePosition = new Position(this.PlaceDetail.Geolocation.Location.Latitude, this.PlaceDetail.Geolocation.Location.Longitude);
                     this.GetDistanceAsync(origin, placePosition);
                });
            }
        }
    }
}
