using Biblioteca.BD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Modelo
{
    public class Usuario
    {
        /// ctm que pAJA HACER ESTA WEASDSAKDNKSBFSDBO
        /// 

        private int _idusuario;
        private string _uss;
        private string _pass;
        private DateTime _fNac;
        private string _rut;
        private string idFacebook;
        private Comuna comuna;
        private int fk_TipoU;
        private int fk_idPrevision;
        private int fk_Eferglucosa;
        private int fk_FactorRh;
        private int fk_TipoSangre;
        private int Fk_recordatorios;
        private int Fk_inicionSesion;
        private int fk_tipoPrevision;
        private string nombre;
        private string apellido;



        public Usuario() { }// constructo vacio

        public Usuario(int idusuario, string uss, string pass, DateTime fNac, string rut, string idFacebook, Comuna comuna, int fk_TipoU, int fk_idPrevision, int fk_Eferglucosa, int fk_FactorRh, int fk_TipoSangre, int fk_recordatorios, int fk_inicionSesion, int fk_tipoPrevision, string nombre, string apellido)
        {
            _idusuario = idusuario;
            _uss = uss;
            _pass = pass;
            _fNac = fNac;
            _rut = rut;
            this.idFacebook = idFacebook;
            this.comuna = comuna;
            this.fk_TipoU = fk_TipoU;
            this.fk_idPrevision = fk_idPrevision;
            this.fk_tipoPrevision = fk_tipoPrevision;
            this.fk_Eferglucosa = fk_Eferglucosa;
            this.fk_FactorRh = fk_FactorRh;
            this.fk_TipoSangre = fk_TipoSangre;
            Fk_recordatorios = fk_recordatorios;
            Fk_inicionSesion = fk_inicionSesion;
            this.nombre = nombre;
            this.apellido = apellido;
        }

        public int Idusuario { get => _idusuario; set => _idusuario = value; }
        public string Uss
        {
            get => _uss;
            set
            {
                if (value.Length >= 1)
                {
                    _uss = value;
                }
                else
                {
                    throw new Exception("Debe ingresar un correo valido.");
                }
            }
        }
        public string Pass
        {
            get => _pass;
            set => _pass = value;
        }
        public DateTime FNac
        {
            get => _fNac;
            set
            {

                if (value != null)
                {
                    _fNac = value;
                }
                else
                {
                    throw new Exception("Debe ingresar alguna fecha valida.");
                }

            }
        }
        public string Rut
        {
            get => _rut;
            set
            {
                if (value.Length >= 1)
                {
                    _rut = value;
                }
                else
                {
                    throw new Exception("Debe ingresar un rut valido");
                }
            }

        }
        public string IdFacebook { get => idFacebook; set => idFacebook = value; }
        public Comuna Comuna
        {
            get => comuna;
            set
            {
                try
                {
                    comuna = value;
                }
                catch (Exception ex)
                {
                    comuna = new Comuna(1, "hola");

                    throw new Exception("Debe ingresar un rut valido" + ex.Message);
                }

            }
        }
        public int Fk_TipoU { get => fk_TipoU; set => fk_TipoU = value; }
        public int Fk_idPrevision { get => fk_idPrevision; set => fk_idPrevision = value; }
        public int Fk_Eferglucosa { get => fk_Eferglucosa; set => fk_Eferglucosa = value; }
        public int Fk_TipoSangre { get => fk_TipoSangre; set => fk_TipoSangre = value; }
        public int Fk_recordatorios1 { get => Fk_recordatorios; set => Fk_recordatorios = value; }
        public int Fk_inicionSesion1 { get => Fk_inicionSesion; set => Fk_inicionSesion = value; }
        public string Nombre
        {
            get => nombre;
            set
            {
                if (value.Length >= 1)
                {
                    nombre = value;
                }
                else
                {
                    throw new Exception("Debe ingresar algun nombre.");
                }
            }
        }
        public string Apellido
        {
            get => apellido;
            set
            {
                if (value.Length >= 1)
                {
                    apellido = value;
                }
                else
                {
                    throw new Exception("Debe ingresar algun apellido");
                }
            }
        }

        public int Fk_FactorRh { get => fk_FactorRh; set => fk_FactorRh = value; }
        public int Fk_tipoPrevision { get => fk_tipoPrevision; set => fk_tipoPrevision = value; }
    }
}
