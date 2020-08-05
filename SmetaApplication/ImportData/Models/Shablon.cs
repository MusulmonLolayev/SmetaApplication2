using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smeta.Models;

namespace Smeta.Models
{
    public class Shablon
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Tarkib { get; set; }
        public string Izoh { get; set; }
        public string IshTuriId { get; set; }

        // Polevoy 0, komeralka 1, Lobaratoriya 2
        public string PKL { get; set; }

        public string NomerNorma { get; set; }
        public string KotegoriyaSlojnost { get; set; }

        public double? TarifStavka { get; set; }
        public string OlchovBirligi { get; set; }
        public string Nomer { get; set; }
        public double? IshVaqti { get; set; }
        public string PovishKoefsent { get; set; }
        public double? PovishQiymat { get; set; }
        public double? OtishKoefsent { get; set; }
        public double? Narx { get; set; }
        public double? Hajm { get; set; }
        public double? Baho { get; set; }
    }
}
