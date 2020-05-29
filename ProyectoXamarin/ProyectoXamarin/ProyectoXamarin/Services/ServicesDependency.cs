using Autofac;
using ProyectoXamarin.Repositories;
using ProyectoXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Services
{
    public class ServicesDependency
    {
        private IContainer container;

        public ServicesDependency()
        {
            this.RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<SessionServices>().SingleInstance();

            builder.RegisterType<RepositoryMonument>();

            //AÑADIR EL VIEWMODEL
            builder.RegisterType<UsuarioViewModel>();
            builder.RegisterType<ProvinciasViewModel>();

            //Vistas
            builder.RegisterType<MainMasterPage>();


            this.container = builder.Build();
        }

        public MainMasterPage MainMasterPage
        {
            get
            {
                return this.container.Resolve<MainMasterPage>();
            }
        }

        public RepositoryMonument RepositoryMonument
        {
            get
            {
                return this.container.Resolve<RepositoryMonument>();
            }
        }

        public SessionServices SessionService
        {
            get
            {
                return this.container.Resolve<SessionServices>();
            }
        }
        public UsuarioViewModel UsuarioViewModel
        {
            get
            {
                return this.container.Resolve<UsuarioViewModel>();
            }
        }
        public ProvinciasViewModel ProvinciasViewModel
        {
            get
            {
                return this.container.Resolve<ProvinciasViewModel>();
            }
        }
    }
}
