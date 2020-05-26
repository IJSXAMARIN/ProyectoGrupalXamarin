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


        public async Task GetPlaces(Position currentPosition)
        {
            // TODO: Pasarle la posición a la api
            List<Place> places = await this.api.GetPlaces();
            this.Places = new ObservableCollection<Place>(places);
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

                photo.Image = photo.Image = ImageSource.FromStream(() => stream);
            };

            return place;
        }
    }
}
