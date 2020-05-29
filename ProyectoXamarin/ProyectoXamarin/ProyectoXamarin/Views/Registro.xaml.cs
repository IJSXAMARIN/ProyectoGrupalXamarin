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
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
        }

        public Registro(string mensaje)
        {
            InitializeComponent();
            this.lblMensaje.Text = mensaje;
        }

        private async void btnRedireccion_Clicked_1(object sender, EventArgs e)
        {
            LogInView view = new LogInView();
            await
                Application.Current.MainPage.Navigation.PushModalAsync(view);
        }
    }
}