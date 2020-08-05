using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Smeta.Models
{
    public class ShablonContext
    {
        public string Id { get; set; }
        public string N { get; set; }
        public string Nom { get; set; }
        public string Koef { get; set; }

        public CheckBox Status { get; set; }

        public ShablonContext()
        {
            Status = new CheckBox()
            {
                IsChecked = true
            };
        }
    }
}
