using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoXamarin.Views;

namespace ProyectoXamarin
{
    public partial class App : Application
    {

   

        public App()
        {
            InitializeComponent();

            MainPage = new MainMasterPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
