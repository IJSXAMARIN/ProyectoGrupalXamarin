using ProyectoXamarin.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Services
{
    public class SessionServices
    {
        public List<Monument> Monumentos { get; set; }
        public SessionServices()
        {
            this.Monumentos = new List<Monument>();
        }
    }
}
