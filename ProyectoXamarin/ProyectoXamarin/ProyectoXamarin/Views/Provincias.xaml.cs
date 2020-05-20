using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Provincias : ContentPage
    {
        public Provincias()
        {
            InitializeComponent();
            List<Provincia> provincias = new List<Provincia>();
            Provincia c = new Provincia();
            c.Nombre = "Madrid";
            c.Descripcion = "Bocata de calamares";
            c.Imagen = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Flag_of_the_Community_of_Madrid.svg/1200px-Flag_of_the_Community_of_Madrid.svg.png";
            provincias.Add(c);
            c = new Provincia();
            c.Nombre = "Galicia";
            c.Descripcion = "Pulpito a la gallega";
            c.Imagen = "https://www.imazu.es/wp-content/uploads/2018/07/bandera-galicia-1080x675.jpg";
            provincias.Add(c);
            c = new Provincia();
            c.Nombre = "Andalucia";
            c.Descripcion = "ole ole ole";
            c.Imagen = "https://images-na.ssl-images-amazon.com/images/I/31dL1mqhpHL._AC_SY400_.jpg";
            provincias.Add(c);
            this.lsvprovincias.ItemsSource = provincias;
        }
    }
}