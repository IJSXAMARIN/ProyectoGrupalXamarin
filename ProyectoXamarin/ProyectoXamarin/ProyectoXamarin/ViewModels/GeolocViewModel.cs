using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class GeolocViewModel : ViewModelBase
    {
        private static Geoloc _Geo;
        public static Geoloc Geo
        {
            get
            {
                return _Geo;
            }

            set
            {
                _Geo = value;
                //OnPropertyChanged("Geo");
            }
        }

        public static void AskForPermission()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var status = await CrossPermissions.Current.
                    CheckPermissionStatusAsync(Permission.Location);
                    if (status != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.
                            RequestPermissionsAsync(Permission.Location);
                        if (results.ContainsKey(Permission.Location))
                        {
                            status = results[Permission.Location];
                        }
                    }
                    if (status == PermissionStatus.Granted)
                    {
                        PruebasMapa view = new PruebasMapa();

                        await Application.Current.MainPage.
                                Navigation.PushModalAsync(view);

                        // view = new PruebasMapa(this.Geo);

                    }
                }
                catch (Exception ex)
                {
                    Console.Write("ERROR!!!!!!!!!!!");
                    Console.Write(ex);
                }
            });
        }

        public Command GetGeo
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        var task = Geolocation.GetLastKnownLocationAsync();
                        Geo = new Geoloc();
                        task.ContinueWith(x =>
                        {
                            var location = x.Result;
                            if (location != null)
                            {
                                Geo.Latitud = location.Latitude;
                                Geo.Longitud = location.Longitude;

                                AskForPermission();
                            }
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });
            }
        }

    }
}
