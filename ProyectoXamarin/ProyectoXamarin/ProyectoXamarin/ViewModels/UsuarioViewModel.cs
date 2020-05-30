using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using ProyectoXamarin.Repositories;
using ProyectoXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class UsuarioViewModel : ViewModelBase
    {
        //Para el perfil del usuario
        RepositoryMonument repo;

        public UsuarioViewModel(RepositoryMonument repo)
        {
            this.repo = repo;
            this.Usuario = new Usuario();
        }

        public Command RegistroUser
        {
            get
            {
                return new Command(async () =>
                {
                    if (Usuario.NickName == null || Usuario.Password == null
                    || Usuario.NombreUs == null || Usuario.Password == null)
                    {
                        String mensaje = "Faltan campos por completar";
                        Registro view = new Registro(mensaje);
                        await
                            Application.Current.MainPage.Navigation.PushModalAsync(view);
                    }
                    else
                    {
                        await this.repo.InsertarUsuario(Usuario.NombreUs,
                            Usuario.Email, Usuario.NickName, Usuario.Password);
                        //MessagingCenter.Send(App.Locator.UsuarioViewModel, "REFRESH");

                        //await Application.Current.MainPage.
                        //    DisplayAlert("WARNING", "Insertado", "Ok");
                        LogInView view = new LogInView();
                        App.Current.MainPage = new MainMasterPage { Detail = new NavigationPage(view) };
                    }
                });
            }
        }

        public Command Login
        {
            get
            {
                return new Command(async () =>
                {
                    if (Usuario.NickName == null || Usuario.Password == null)
                    {
                        String mensaje = "No puedes validarte";
                        LogInView view = new LogInView(mensaje);
                    }
                    else
                    {
                        if (await this.repo.GetToken(Usuario.NickName,
                            Usuario.Password) != null)
                        {
                            String token = await this.repo.GetToken(Usuario.NickName, Usuario.Password);
                            Perfil perfil = new Perfil(); 
                            UsrConectadoViewModel viewModel = App.Locator.UsrConectadoViewModel;
                            viewModel.UsuarioC = await this.repo.PerfilUsuario(token);                                     
                            perfil.BindingContext = viewModel;
                            //await Application.Current.MainPage.Navigation.PushModalAsync(perfil);
                            App.Current.MainPage = new MainMasterPage { Detail = new NavigationPage(perfil)};
                            //MessagingCenter.Send(App.Locator.UsuarioViewModel, "REFRESH");
                            //await Application.Current.MainPage.
                            //    DisplayAlert("WARNING", "Has sido logueado", "Ok");

                        }
                        String mensaje = "Credenciales Incorrectas";
                        LogInView view = new LogInView(mensaje);
                    }
                });
            }
        }

        private  Usuario _Usuario;
        public   Usuario Usuario
        {
            get
            {
                return this._Usuario;
            }
            set
            {
                this._Usuario = value;
                OnPropertyChanged("Usuario");
            
            }
        }

    }
}
