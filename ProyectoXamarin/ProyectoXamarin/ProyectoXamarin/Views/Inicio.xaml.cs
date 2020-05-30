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
    public partial class Inicio : ContentPage
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainMasterPage { Detail = new NavigationPage(new Provincias()) };
        }

        private void btnperfil_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainMasterPage { Detail = new NavigationPage(new LogInView()) };
        }

        private void btnmapa_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainMasterPage { Detail = new NavigationPage(new PruebasMapa()) };
        }
    }
}