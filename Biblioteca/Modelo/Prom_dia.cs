using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Modelo
{
    public class Prom_dia
    {
        int promedio;
        DateTime fecha;

        public Prom_dia()
        {

        }

        public Prom_dia(int promedio, DateTime fecha)
        {
            this.Promedio = promedio;
            this.Fecha = fecha;
        }

        public int Promedio
        {
            get
            {

                return promedio;
            }
            set
            {

                promedio = value;
            }
        }

        public DateTime Fecha 
        {
            get 
            { 
                return fecha; 

            }
            set
            {
                if (value ==null)
                {
                    fecha = DateTime.MinValue;
                }
                else { fecha = value; }
                
            } 
        }
    }
}
