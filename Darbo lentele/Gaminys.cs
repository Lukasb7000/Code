using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darbo_lentele
{
    class Gaminys
    {
        public string Pavadinimas { get; set; }
        public string Papildomas_Pavadinimas { get; set; }
        public string Detales_nr { get; set; }
        public int Vienetai { get; set; }
        public int Ikainis { get; set; }
        public double Suma { get; set; }

        public Gaminys (string pav,string pap,string nr, int vienetai,int ikainis,double suma)
        {
            Pavadinimas = pav;
            Papildomas_Pavadinimas = pap;
            Detales_nr = nr;
            Vienetai = vienetai;
            Ikainis = ikainis;
            Suma = suma;

        }
        public Gaminys(double suma)
        {
            Suma = suma;
        }
        public double skaiciuoti()
        {
            return Vienetai * Ikainis;
        }
    }
}
