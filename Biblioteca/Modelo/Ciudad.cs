using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.BD
{
    public class Ciudad : Region
    {
        int idCiudad;
        string nombreCiudad;

        public Ciudad(int idCiudad, string nombreCiudad)
        {
            this.idCiudad = idCiudad;
            this.nombreCiudad = nombreCiudad;
        }

        public Ciudad()
        {

        }

        public int IdCiudad { get => idCiudad; set => idCiudad = value; }
        public string NombreCiudad { get => nombreCiudad; set => nombreCiudad = value; }
    }
}
