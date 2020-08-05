using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta.Models
{
   public class Data
    {
        public uint Id { get; set; }
        public double? Hajm { get; set; }
        public string PovishKoefsent { get; set; }
        public double? PovishQiymat { get; set; }
        public uint IshId { get; set; }
        public uint DogovorId { get; set; }
        // Ish vaqti
        public double? IV { get; set; }
        // Tarifniy stavka 
        public double ?TS { get; set; }
    }
}
