using ProyectoXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            item.Titulo = "Mapa";
            item.PaginaHija = typeof(Mapa);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Perfil";
            item.PaginaHija = typeof(Perfil);
            this.MenuPaginas.Add(item);
            item = new MasterPageItem();
            item.Titulo = "Sobre Nosotros";
            item.PaginaHija = typeof(Info);
            this.MenuPaginas.Add(item);
            this.lsvmenupaginas.ItemsSource = this.MenuPaginas;
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Inicio)));
            this.lsvmenupaginas.ItemSelected += Lsvmenupaginas_ItemSelected;
        }

        private void Lsvmenupaginas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MasterPageItem item = (MasterPageItem)e.SelectedItem;
            Type pagina = item.PaginaHija;
            Detail = new NavigationPage((Page)Activator.CreateInstance(pagina));
            IsPresented = false;
        }
    }
}