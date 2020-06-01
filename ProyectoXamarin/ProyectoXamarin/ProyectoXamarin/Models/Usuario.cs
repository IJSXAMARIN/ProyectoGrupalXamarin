using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class Usuario
    {
        public int IdUser { get; set; }
        public String NombreUs { get; set; }
        public String Email { get; set; }
        public String NickName { get; set; }
        public string Password { get; set; }

        public String Salt { get; set; }

        public List<Monumento> Monumentos { get; set; }
    }
}
