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
    public partial class LogInView : ContentPage
    {
        public LogInView()
        {
            InitializeComponent();
        }

        public LogInView(String mensaje)
        {
            InitializeComponent();
            this.lblMensaje.Text = mensaje;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new MainMasterPage { Detail = new NavigationPage(new Registro()) };
        }
    }
}