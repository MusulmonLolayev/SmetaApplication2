using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta.Models
{
   public  class Lavozim
    {
        public uint Id { get; set; }
        public string Nom { get; set; }
        public uint tarif_raziryad { get; set; }
        public double tarif_koefsent { get; set; }
        public double tarif_oklad { get; set; }
    }
}
