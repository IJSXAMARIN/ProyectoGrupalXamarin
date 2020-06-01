using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class Monumento
    {
        public String IdMonument { get; set; }
        public int IdUser { get; set; }
        public String NombreMonu { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Image { get; set; }
    }
}
