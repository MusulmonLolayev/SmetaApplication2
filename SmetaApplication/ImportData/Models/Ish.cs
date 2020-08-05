using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Smeta.Models
{
    public class Ish:IComparable
    {
        public uint Id { get; set; }
        public string Nom { get; set; }

        public string Tarkibi { get; set; }

        public string Izoh { get; set; }
        public uint IshTuriId { get; set; }

        // Polevoy 1, komeralka 2, Lobaratoriya 3
        public uint PKL { get; set; }

        public string NomerNorma { get; set; }
        public string KotegoriyaSlojnost { get; set; }
        public double IshVaqti { get; set; }
        public double TarifStavka { get; set; }
        public string OlchovBirligi { get; set; }

        public static bool operator <(Ish ob1, Ish ob2)
        {
            if (ob1.NomerNorma.CompareTo(ob2.NomerNorma) > 0)
                return false;
            else
                return true;
        }

        public static bool operator >(Ish ob1, Ish ob2)
        {
            if (ob1.NomerNorma.CompareTo(ob2.NomerNorma) > 0)
                return true;
            else
                return false;
        }   

        public int CompareTo(object obj)
        {
            Ish h = obj as Ish;
            return NomerNorma.CompareTo(h.NomerNorma);
        }
    }
}
