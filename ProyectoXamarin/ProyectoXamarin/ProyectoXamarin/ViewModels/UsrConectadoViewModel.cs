using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using ProyectoXamarin.Repositories;
using ProyectoXamarin.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class UsrConectadoViewModel:ViewModelBase
    {


        RepositoryMonument repo;

        public UsrConectadoViewModel() => this.repo = new RepositoryMonument();

        private Usuario _UsuarioC;
        public Usuario UsuarioC
        {
            get
            {
                return this._UsuarioC;
            }
            set
            {
                this._UsuarioC = value;
                IdUsuario = this._UsuarioC.IdUser;
                this.CargarImgMonu();
                OnPropertyChanged("UsuarioC");

            }
        }


        public static int IdUsuario;

        private ObservableCollection<Monumento> _ListaMonu;
        public ObservableCollection<Monumento> ListaMonu
        {
            get
            {
                return this._ListaMonu;
            }
            set
            {
                this._ListaMonu = value;
                OnPropertyChanged("ListaMonu");
            }
        }



        public async void CargarImgMonu()
        {
            //MONUMENTOS QUE EL USUARIO HA VISITADO
            List<Monumento> monumentos = await this.repo.GetMonumentos(IdUsuario);
           

            ApiGooglePlaces api = new ApiGooglePlaces();
          

            //    [JsonProperty("photo_reference")]
            //public String PhotoReference { get; set; }

            foreach (Monumento monu in monumentos)
            {

                Stream stream = await api.LoadImagesPlace(monu.Image);

                monu.ImageSource = ImageSource.FromStream(() => stream);


            }

            this.ListaMonu = new ObservableCollection<Monumento>(monumentos);
        }
    }
}
