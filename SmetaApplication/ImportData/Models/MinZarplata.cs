using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Controls;

namespace Smeta.Models
{
    public class MinZarplata
    {
        public uint Id { get; set; }
        public double Qiymat { get; set; }
        public DateTime Sana { get; set; }
        public string Asos { get; set; }
        public CheckBox  Status{ get; set; }

        public MinZarplata()
        {
            Status = new CheckBox();
        }
    }
}
