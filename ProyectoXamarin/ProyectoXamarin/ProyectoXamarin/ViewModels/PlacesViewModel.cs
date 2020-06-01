using ProyectoXamarin.Models;
using ProyectoXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using ProyectoXamarin.Base;
using System.Linq;
using Xamarin.Forms.Maps;
using System.Net.NetworkInformation;
using Xamarin.Forms;
using System.IO;
using ProyectoXamarin.Repositories;

namespace ProyectoXamarin.ViewModels
{
    public class PlacesViewModel : ViewModelBase
    {
        ApiGooglePlaces api;

        private ObservableCollection<Place> _Places;

        public ObservableCollection<Place> Places
        {
            get => this._Places;

            set
            {
                this._Places = value;
                OnPropertyChanged("Places");
            }
        }



        public PlacesViewModel() => this.api = new ApiGooglePlaces();



        public async Task GetPlaces(Geoloc currentPosition)
        {
            // TODO: Pasarle la posición a la api
            List<Place> places = await this.api.GetPlaces(currentPosition);

            RepositoryMonument repo = new RepositoryMonument();

            foreach (Place place in places)
            {
                place.Visitado = repo.MonumentoVisitado(UsrConectadoViewModel.IdUsuario, place.PlaceId).Result;
            }

            /*  Parallel.ForEach(places, place =>
              {
                  place.Visitado = repo.MonumentoVisitado(0, place.PlaceId).Result;

              });*/



            this.Places = new ObservableCollection<Place>(places);
        }

        public async Task GetPlacesByCity(String city)
        {

            // TODO: Pasarle la posición a la api
            List<Place> places = await this.api.GetPlacesByCity(city);
            this.Places = new ObservableCollection<Place>(places);
        }

        public async Task<Position> GetPositionCity(String city)
        {
            return await this.api.GetPositionCity(city);
        }

        public Place GetPlace(String placeId)
        {
            return this._Places.FirstOrDefault(z => z.PlaceId == placeId);
        }

       public async Task<Place> LoadImagePlace(Place place)
       {

            foreach(Photo photo in place.Photos)
            {
                Stream stream = await this.api.LoadImagesPlace(photo.PhotoReference);

                photo.Image = ImageSource.FromStream(() => stream);
            };

            return place;
        }
    }
}
