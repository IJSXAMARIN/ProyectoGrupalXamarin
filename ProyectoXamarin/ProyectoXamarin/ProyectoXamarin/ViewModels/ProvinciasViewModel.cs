using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProyectoXamarin.ViewModels
{
    public class ProvinciasViewModel : ViewModelBase
    {

        public ProvinciasViewModel()
        {
            this.Provincias = this.GenerarProvincias();
        }

        private ObservableCollection<Provincia> _Provincias;

        public ObservableCollection<Provincia> Provincias
        {
            get => this._Provincias;

            set
            {
                this._Provincias = value;
                OnPropertyChanged("Provincias");
            }
        }
        private ObservableCollection<Provincia> GenerarProvincias()
        {
            ObservableCollection<Provincia> provincias = new ObservableCollection<Provincia>();

            Provincia prov = new Provincia()
            {
            Nombre = "Madrid",
            Descripcion = "Bocata de calamares",
            Imagen = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Flag_of_the_Community_of_Madrid.svg/1200px-Flag_of_the_Community_of_Madrid.svg.png"
        };

            provincias.Add(prov);

            prov = new Provincia()
            {
             Nombre = "Galicia",
            Descripcion = "Pulpito a la gallega",
            Imagen = "https://www.imazu.es/wp-content/uploads/2018/07/bandera-galicia-1080x675.jpg"
        };

            provincias.Add(prov);

            prov = new Provincia()
            {
              Nombre = "Andalucia",
              Descripcion = "ole ole ole",
              Imagen = "https://images-na.ssl-images-amazon.com/images/I/31dL1mqhpHL._AC_SY400_.jpg"
        };

            provincias.Add(prov);

            return provincias;
        }

    }
}
