using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using ProyectoXamarin.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.ViewModels
{
    public class UsuarioViewModel : ViewModelBase
    {
        //Para el perfil del usuario
        RepositoryMonument repo;

        public UsuarioViewModel(RepositoryMonument repo)
        {
            this.repo = repo;
        }

        private Usuario _User;
        public Usuario User
        {
            get
            {
                return this._User;
            }
            set
            {
                this._User = value;
                OnPropertyChanged("User");
            }
        }
    }
}
