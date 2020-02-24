using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Modelo
{
    public class Glucosa
    {
        private int idGlucosa;
        // desayuno
        private TimeSpan H_A_Des;
        private int N_A_Des;
        private TimeSpan H_D_Des;
        private int N_D_Des;
        // almuerzo
        private TimeSpan H_A_Alm;
        private int N_A_Alm;
        private TimeSpan H_D_Alm;
        private int N_D_Alm;
        //Cena
        private TimeSpan H_A_Cen;
        private int N_A_Cen;
        private TimeSpan H_D_Cen;
        private int N_D_Cen;
        // dormir
        private TimeSpan H_A_Dor;
        private int N_A_Dor;
        private TimeSpan H_D_Dor;
        private int N_D_Dor;
        private string obs;
        private double latitud, longitud;
        private DateTime fecha;
        private int fk_id_US;

        public Glucosa() { }// constructor vacio 

        public Glucosa(int idGlucosa, TimeSpan h_A_Des, int n_A_Des, TimeSpan h_D_Des, int n_D_Des, TimeSpan h_A_Alm, int n_A_Alm, TimeSpan h_D_Alm, int n_D_Alm, TimeSpan h_A_Cen, int n_A_Cen, TimeSpan h_D_Cen, int n_D_Cen, TimeSpan h_A_Dor, int n_A_Dor, TimeSpan h_D_Dor, int n_D_Dor, string obs, double latitud, double longitud, DateTime fecha, int fk_id_US)
        {
            this.idGlucosa = idGlucosa;
            H_A_Des = h_A_Des;
            N_A_Des = n_A_Des;
            H_D_Des = h_D_Des;
            N_D_Des = n_D_Des;
            H_A_Alm = h_A_Alm;
            N_A_Alm = n_A_Alm;
            H_D_Alm = h_D_Alm;
            N_D_Alm = n_D_Alm;
            H_A_Cen = h_A_Cen;
            N_A_Cen = n_A_Cen;
            H_D_Cen = h_D_Cen;
            N_D_Cen = n_D_Cen;
            H_A_Dor = h_A_Dor;
            N_A_Dor = n_A_Dor;
            H_D_Dor = h_D_Dor;
            N_D_Dor = n_D_Dor;
            this.obs = obs;
            this.latitud = latitud;
            this.longitud = longitud;
            this.fecha = fecha;
            this.fk_id_US = fk_id_US;
        }

        public int IdGlucosa { get => idGlucosa; set => idGlucosa = value; }
        public TimeSpan H_A_Des1// hora antes del desayuno
        {
            get => H_A_Des;
            set
            {
                if (value == null)
                {
                    H_A_Des = TimeSpan.MinValue;
                }
                else { H_A_Des = value; }

            }
        }
        public int N_A_Des1 { get => N_A_Des; set => N_A_Des = value; }//vlos int nuca son null
        public TimeSpan H_D_Des1
        {
            get => H_D_Des;
            set
            {
                if (value == null)
                {
                    H_D_Des = TimeSpan.MinValue;
                }
                else { H_D_Des = value; }

            }
        }
        public int N_D_Des1 { get => N_D_Des; set => N_D_Des = value; }
        public TimeSpan H_A_Alm1
        {
            get => H_A_Alm;
            set
            {
                if (value == null)
                {
                    H_A_Alm = TimeSpan.MinValue;
                }
                else { H_A_Alm = value; }

            }
        }
        public int N_A_Alm1 { get => N_A_Alm; set => N_A_Alm = value; }
        public TimeSpan H_D_Alm1
        {
            get => H_D_Alm;
            set
            {
                if (value == null)
                {
                    H_D_Alm = TimeSpan.MinValue;
                }
                else { H_D_Alm = value; }

            }
        }
        public int N_D_Alm1 { get => N_D_Alm; set => N_D_Alm = value; }
        public TimeSpan H_A_Cen1
        {
            get => H_A_Cen;
            set
            {
                if (value == null)
                {
                    H_A_Cen = TimeSpan.MinValue;
                }
                else { H_A_Cen = value; }

            }
        }
        public int N_A_Cen1 { get => N_A_Cen; set => N_A_Cen = value; }
        public TimeSpan H_D_Cen1
        {
            get => H_D_Cen;
            set
            {
                if (value == null)
                {
                    H_D_Cen = TimeSpan.MinValue;
                }
                else { H_D_Cen = value; }

            }
        }
        public int N_D_Cen1 { get => N_D_Cen; set => N_D_Cen = value; }
        public TimeSpan H_A_Dor1
        {
            get => H_A_Dor;
            set
            {
                if (value == null)
                {
                    H_A_Dor = TimeSpan.MinValue;
                }
                else { H_A_Dor = value; }

            }
        }
        public int N_A_Dor1 { get => N_A_Dor; set => N_A_Dor = value; }
        public TimeSpan H_D_Dor1
        {
            get => H_D_Dor;
            set
            {
                if (value == null)
                {
                    H_D_Dor = TimeSpan.MinValue;
                }
                else { H_D_Dor = value; }
            }
        }
        public int N_D_Dor1 { get => N_D_Dor; set => N_D_Dor = value; }
        public string Obs
        {
            get
            {
                if (obs == null || obs == "" || obs == " ")
                {
                    obs = "Escriba sus observaciones";
                }
               return obs;
            }
            set
            {
                if (value == null || value=="" ||value==" " )
                {
                    obs = "Escriba sus observaciones";
                }
                else { obs = value; }

            }
        }
        public double Latitud { get => latitud; set => latitud = value; }
        public double Longitud { get => longitud; set => longitud = value; }
        public DateTime Fecha
        {
            get => fecha;
            set
            {
                if (value == null)
                {
                    fecha = DateTime.Now;
                }
                else { fecha = value; }

            }
        }
        public int Fk_id_US { get => fk_id_US; set => fk_id_US = value; }

    }
}
