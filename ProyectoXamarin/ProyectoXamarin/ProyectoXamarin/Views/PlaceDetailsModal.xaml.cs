using ProyectoXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceDetailsModal : ContentPage
    {

        private PlaceDetailViewModel viewModel;

        public PlaceDetailsModal()
        {
            InitializeComponent();

           // this.viewModel = new PlaceDetailViewModel();

        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

    //    private async void btnDescubrir_Clicked(object sender, EventArgs e)
    //    {
    //        string coordenadasPuertaSol = "40.416883, -3.703567";
           
    //        Position origin = new Position(40.416883, -3.703567);

    //        Position placePosition = new Position(this.viewModel.PlaceDetail.Geolocation.Location.Latitude, this.viewModel.PlaceDetail.Geolocation.Location.Longitude);


    //        await this.viewModel.GetDistanceAsync(origin, placePosition);
    //    }
    }
}