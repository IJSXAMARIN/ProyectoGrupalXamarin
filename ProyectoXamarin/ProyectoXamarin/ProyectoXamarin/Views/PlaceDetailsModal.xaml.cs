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
    public partial class PlaceDetailsModal : ContentPage
    {
        public PlaceDetailsModal()
        {
            InitializeComponent();


            //var nameLabel = new Label
            //{
            //   FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            //   FontAttributes = FontAttributes.Bold
            //};

            //nameLabel.SetBinding(Label.TextProperty, "Vicinity");

            //ScrollView scrollView = new ScrollView();


            //List<Image> images = new List<Image>();

            //Image image = new Image
            //{

            //};





            //image.SetBinding(Image.SourceProperty, "Photos");

            //ScrollView scroll = new ScrollView();



            //var dismissButton = new Button { Text = "Dismiss" };

            //dismissButton.Clicked += OnDismissButtonClicked;

            //Content = new StackLayout
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.Center,
            //    Children = {
            //  new StackLayout {
            //    Orientation = StackOrientation.Horizontal,
            //    Children = {
            //      new Label{ Text = "Name:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
            //      nameLabel,
            //      image
            //    }
            //  },

            //  dismissButton
            //}
            //};

            //      var nameLabel = new Label
            //      {
            //          FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
            //          FontAttributes = FontAttributes.Bold
            //      };


            //      nameLabel.SetBinding(Label.TextProperty, "Name");

            //      var dismissButton = new Button { Text = "Dismiss" };

            //      dismissButton.Clicked += OnDismissButtonClicked;

            //      Thickness padding;

            //      switch (Device.RuntimePlatform)
            //      {
            //          case Device.iOS:
            //              padding = new Thickness(0, 40, 0, 0);
            //              break;
            //          default:
            //              padding = new Thickness();
            //              break;
            //      }

            //      Padding = padding;
            //      Content = new StackLayout
            //      {
            //          HorizontalOptions = LayoutOptions.Center,
            //          VerticalOptions = LayoutOptions.Center,
            //          Children = {
            //  new StackLayout {
            //    Orientation = StackOrientation.Horizontal,
            //    Children = {
            //      new Label{ Text = "Name:", FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)), HorizontalOptions = LayoutOptions.FillAndExpand },
            //      nameLabel
            //    }
            //  },

            //  dismissButton
            //}
            //      };
        }

        async void OnDismissButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }
    }
}