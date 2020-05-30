using ProyectoXamarin.Base;
using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.ViewModels
{
    public class UsrConectadoViewModel:ViewModelBase
    {
        private Usuario _UsuarioC;
        public Usuario UsuarioC
        {
            get
            {
                return this._UsuarioC;
            }
            set
            {
                this._UsuarioC = value;
                OnPropertyChanged("UsuarioC");
            }
        }
    }
}
