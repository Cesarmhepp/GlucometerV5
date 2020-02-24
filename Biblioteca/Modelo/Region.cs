using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.BD
{
    public class Region
    {
        int idRegion;
        string nombreRegion;

        public int IdRegion { get => idRegion; set => idRegion = value; }
        public string NombreRegion { get => nombreRegion; set => nombreRegion = value; }

        public Region(int idRegion, string nombreRegion)
        {
            this.idRegion = idRegion;
            this.nombreRegion = nombreRegion;
        }

        public Region()
        { 
        
        }



    }
}
