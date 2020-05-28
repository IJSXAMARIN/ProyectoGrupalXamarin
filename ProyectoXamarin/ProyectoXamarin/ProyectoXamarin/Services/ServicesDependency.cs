﻿using Autofac;
using ProyectoXamarin.Repositories;
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

            this.container = builder.Build();
        }

        public SessionServices SessionService
        {
            get
            {
                return this.container.Resolve<SessionServices>();
            }
        }

    }
}