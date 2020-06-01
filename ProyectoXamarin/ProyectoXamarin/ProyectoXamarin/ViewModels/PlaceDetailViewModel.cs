using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using ProyectoXamarin.Repositories;
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

                RepositoryMonument repo = new RepositoryMonument();

                if (this.DistanceDuration.Distance.Value < 1000)
                {

                    UsrConectadoViewModel viewmodel = new UsrConectadoViewModel();

                   

                    Monumento monumento = new Monumento()
                    {
                        IdMonument = this.PlaceDetail.PlaceId,
                        IdUser = UsrConectadoViewModel.IdUsuario,
                        Image = this.PlaceDetail.Photos.FirstOrDefault().PhotoReference,
                        Latitud = this.PlaceDetail.Geolocation.Location.Latitude,
                        Longitud = this.PlaceDetail.Geolocation.Location.Longitude,
                        NombreMonu = this.PlaceDetail.PlaceName,
                    };

                    await repo.VisitarMonu(monumento);

                }

            }).Wait();
        }

        public  Command CalcularDistancia
        {
            get
            {
                return new Command(() =>
                {
                    Position origin = new Position(GeolocViewModel.Geo.Latitud,GeolocViewModel.Geo.Longitud);
                    Position placePosition = new Position(this.PlaceDetail.Geolocation.Location.Latitude, this.PlaceDetail.Geolocation.Location.Longitude);
                     this.GetDistanceAsync(origin, placePosition);
                });
            }
        }
    }
}
