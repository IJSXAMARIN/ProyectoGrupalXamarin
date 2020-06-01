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
        private List<Monumento> _ListaMonu;
        public List<Monumento> ListaMonu
        {
            get
            {
                return this._ListaMonu;
            }
            set
            {
                this._ListaMonu = value;
                OnPropertyChanged("ListaMonu");
            }
        }
    }
}
