using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.BD
{
    public class Comuna : Ciudad
    {
        int idComuna;
        string nombreComuna;

        public Comuna(int idComuna, string nombreComuna)
        {
            this.IdComuna = idComuna;
            this.NombreComuna = nombreComuna;
        }

        public Comuna()
        {

        }

        public int IdComuna { get => idComuna; set => idComuna = value; }
        public string NombreComuna { get => nombreComuna; set => nombreComuna = value; }
    }
}
