using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ProyectoXamarin.Models
{
    public class Monumento
    {
        public String IdMonument { get; set; }
        public int IdUser { get; set; }
        public String Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Image { get; set; }

        public ImageSource ImageSource { get; set; }
    }
}
