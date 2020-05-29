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
        ProvinciasViewModel viewModel;

        public Provincias()
        {
            InitializeComponent();

            this.viewModel = new ProvinciasViewModel();
            Task.Run(async () => await this.viewModel.ObtenerCountry());
       
        }


        private void pickerCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country country = (Country)this.pickerCountry.SelectedItem;

            this.viewModel.CargarPicker("Country", country.CountryName);
            //await this.viewModel.ObtenerState(country.CountryName);
        }
        private void pickerState_SelectedIndexChanged(object sender, EventArgs e)
        {
            State state = (State)this.pickerState.SelectedItem;

            this.viewModel.CargarPicker("State", state.StateName);
            //await this.viewModel.ObtenerState(country.CountryName);
        }

        private async void pickerCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            City city = (City)this.pickerCity.SelectedItem;
            String ciu = city.CityName;
            ciu = ciu.Replace(' ', '+');


            PruebasMapa view = new PruebasMapa(ciu);

            await Navigation.PushModalAsync(view, true);
        }
    }
}

/*
  String ciu = ciudad.CityName;
            ciu = ciu.Replace(' ', '+');

            PlacesViewModel viewModel = new PlacesViewModel();

            await viewModel.GetPlacesByCity(ciu);

            PruebasMapa view = new PruebasMapa();

            await Navigation.PushModalAsync(view, true);
 */
