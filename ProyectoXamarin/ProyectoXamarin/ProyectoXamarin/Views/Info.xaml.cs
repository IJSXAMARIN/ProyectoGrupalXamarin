using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Info : ContentPage
    {
        public Info()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {

            String url = "https://www.linkedin.com/in/jesussaenzjimenez/";
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }

        private async void btnsara_Clicked(object sender, EventArgs e)
        {
            String url = "https://www.linkedin.com/in/sara-bachiller-desarrollo-web/";
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }

        private async void btnivan_Clicked(object sender, EventArgs e)
        {
            String url = "https://www.linkedin.com/in/ivan-sanchez-victoria/";
            await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
        }
    }
}