using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta.Models
{
   public class Izoh
    {
        public uint Id { get; set; }
        public string Nom { get; set; }
        public double Koefsent { get; set; }
        public uint IshId { get; set; }
        // IT = 0, Alohida ish uchun, IT = 1, Geologiya uchun umumiy izoh
        public int IT { get; set; }
    }
}
