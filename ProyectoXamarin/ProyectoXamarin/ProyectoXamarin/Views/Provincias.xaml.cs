using ProyectoXamarin.Models;
using ProyectoXamarin.ViewModels;
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
        }

        private async void Picker_BindingContextChanged(object sender, EventArgs e)
        {
            //Country country = (Country)this.piki.SelectedItem;
            //String count = country.CountryName;
            //ProvinciasViewModel viewmodel = new ProvinciasViewModel();        
            //await viewmodel.ObtenerState(count);
        }
        private void pikerciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            City ciudad = (City)this.pikerciudad.SelectedItem;
            String ciu = ciudad.CityName;
            ciu = ciu.Replace(' ', '+');
        }
    }
}