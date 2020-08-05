using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smeta;

namespace Smeta.Models
{
   public  class Dogovor
    {
        public uint Id { get; set; }
        public string NomerDogovor { get; set; }

        public DateTime Sana { get; set; }

        public string ObyektNom { get; set; }
        public string KlentNom { get; set; }
        public string PKK { get; set; }
        public double PQ { get; set; }
        public string Bajaruvchi { get; set; }
        public string IshTuriId { get; set; }
        public double PK { get; set; }
    }
}
