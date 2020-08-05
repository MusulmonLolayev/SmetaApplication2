using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta.Models
{
    class RedoUndo
    {
        public int Id { get; set; }
        public string PK { get; set; }
        public double? PQ { get; set; }
        public double? Hajm { get; set; }
        public RedoUndo()
        {
            Id = -1;
            PK = "";
            PQ = null;
            Hajm = null;
        }
    }
}
