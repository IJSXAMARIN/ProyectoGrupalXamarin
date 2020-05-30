using ProyectoXamarin.Models;
using ProyectoXamarin.ViewModels;
using ProyectoXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMasterPage : MasterDetailPage
    {
        public ObservableCollection<MasterPageItem> MenuPaginas { get; set; }
        public MainMasterPage()
        {
            InitializeComponent();
            this.MenuPaginas = new ObservableCollection<MasterPageItem>();
            MasterPageItem item = new MasterPageItem();
            item.Titulo = "Inicio";
            item.PaginaHija = typeof(Inicio);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Provincias";
            item.PaginaHija = typeof(Provincias);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Mapa";
            item.PaginaHija = typeof(PruebasMapa);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Perfil";
            item.PaginaHija = typeof(Perfil);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Sobre Nosotros";
            item.PaginaHija = typeof(Info);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Registro";
            item.PaginaHija = typeof(Registro);
            this.MenuPaginas.Add(item);
            this.lsvmenupaginas.ItemsSource = this.MenuPaginas;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Inicio)));
            this.lsvmenupaginas.ItemSelected += Lsvmenupaginas_ItemSelected;
        }

        private void Lsvmenupaginas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            IsPresented = false;
            MasterPageItem item = (MasterPageItem)e.SelectedItem;
            if (item.PaginaHija == typeof(PruebasMapa))
            {
                try
                {
                    var task = Geolocation.GetLastKnownLocationAsync();
                    GeolocViewModel.Geo = new Geoloc();
                    task.ContinueWith(x =>
                    {
                        var location = x.Result;
                        if (location != null)
                        {
                            GeolocViewModel.Geo.Latitud = location.Latitude;
                            GeolocViewModel.Geo.Longitud = location.Longitude;
                            GeolocViewModel.AskForPermission();
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            Type pagina = item.PaginaHija;
            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina));
        }
    }
}
