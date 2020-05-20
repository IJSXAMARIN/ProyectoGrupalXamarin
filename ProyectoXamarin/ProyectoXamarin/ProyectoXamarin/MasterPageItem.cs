using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin
{
    public class MasterPageItem
    {
        public String Titulo { get; set; }
        public Type PaginaHija { get; set; }
        public String MenuPagina { get; set; }
    }
}
