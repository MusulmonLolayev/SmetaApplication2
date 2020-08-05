using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Smeta.Models
{
    public class PovishKoefsent
    {
        public uint Id { get; set; }
        public DateTime Sana { get; set; }
        public double Qiymat { get; set; }
        public string Izoh { get; set; }
        public CheckBox Status { get; set; }
        public PovishKoefsent()
        {
            Status = new CheckBox()
            {
                IsChecked = false
            };
        }
    }
}
