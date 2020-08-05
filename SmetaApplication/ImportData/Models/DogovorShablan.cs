using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta.Models
{
    public class DogovorShablon
    {
        // Tartib raqami
        public string N { get; set; }
        // Dogovor nomeri
        public string ND { get; set; }
        // Obyekt Nomi
        public string Ob { get; set; }
        // Buyurtmachi
        public string Buy { get; set; }
        // Ish turi
        public string IT { get; set; }
        // dogovor sanasi
        public string DD { get; set; }
        // Id
        public string Id { get; set; }
        //IshTuriId
        public string ITI { get; set; }

        public string Baj { get; set; }
    }
}
