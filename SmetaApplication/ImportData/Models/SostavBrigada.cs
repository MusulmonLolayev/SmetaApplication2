using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta.Models
{
   public class SostavBrigada
    {
        public uint Id { get; set; }
        public uint Son { get; set; }
        public double Koef { get; set; }
        public uint LavozimId { get; set; }
        public uint IshId { get; set; }
    }
}
