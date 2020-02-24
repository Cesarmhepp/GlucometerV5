using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Modelo
{
    public class DInsulina
    {
        int idinsulina;
        int fk_idglu;
        int mnna_rap;
        int mnna_regul;
        int mnna_mezcla;
        int tarde_rap;
        int tarde_regul;
        int tarde_mezcla;
        int noche_rap;
        int noche_regul;
        int noche_mezcla;
        int dormir_rap;
        int dormir_regul;
        int dormir_mezcla;

        public DInsulina()
        {

        }

        public DInsulina(int idinsulina, int fk_idglu, int mnna_rap, int mnna_regul, int mnna_mezcla, int tarde_rap, int tarde_regul, int tarde_mezcla, int noche_rap, int noche_regul, int noche_mezcla, int dormir_rap, int dormir_regul, int dormir_mezcla)
        {
            this.Idinsulina = idinsulina;
            this.Fk_idglu = fk_idglu;
            this.Mnna_rap = mnna_rap;
            this.Mnna_regul = mnna_regul;
            this.Mnna_mezcla = mnna_mezcla;
            this.Tarde_rap = tarde_rap;
            this.Tarde_regul = tarde_regul;
            this.Tarde_mezcla = tarde_mezcla;
            this.Noche_rap = noche_rap;
            this.Noche_regul = noche_regul;
            this.Noche_mezcla = noche_mezcla;
            this.Dormir_rap = dormir_rap;
            this.Dormir_regul = dormir_regul;
            this.Dormir_mezcla = dormir_mezcla;
        }

        public int Idinsulina { get => idinsulina; set => idinsulina = value; }
        public int Fk_idglu { get => fk_idglu; set => fk_idglu = value; }
        public int Mnna_rap { get => mnna_rap; set => mnna_rap = value; }
        public int Mnna_regul { get => mnna_regul; set => mnna_regul = value; }
        public int Mnna_mezcla { get => mnna_mezcla; set => mnna_mezcla = value; }
        public int Tarde_rap { get => tarde_rap; set => tarde_rap = value; }
        public int Tarde_regul { get => tarde_regul; set => tarde_regul = value; }
        public int Tarde_mezcla { get => tarde_mezcla; set => tarde_mezcla = value; }
        public int Noche_rap { get => noche_rap; set => noche_rap = value; }
        public int Noche_regul { get => noche_regul; set => noche_regul = value; }
        public int Noche_mezcla { get => noche_mezcla; set => noche_mezcla = value; }
        public int Dormir_rap { get => dormir_rap; set => dormir_rap = value; }
        public int Dormir_regul { get => dormir_regul; set => dormir_regul = value; }
        public int Dormir_mezcla { get => dormir_mezcla; set => dormir_mezcla = value; }
    }

}
