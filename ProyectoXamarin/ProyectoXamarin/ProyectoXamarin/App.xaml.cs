using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoXamarin.Views;
using ProyectoXamarin.Services;

namespace ProyectoXamarin
{
    public partial class App : Application
    {

        private static ServicesDependency _Locator;
        public static ServicesDependency Locator
        {
            get
            {
                return _Locator = _Locator ?? new ServicesDependency();
            }
        }

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
