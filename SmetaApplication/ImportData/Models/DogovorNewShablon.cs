using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeta.Models
{
    class DogovorNewShablon
    {
        public uint Id { get; set; }
        public uint? Nomer { get; set; }
        public string Name { get; set; }
        public System.Windows.Controls.CheckBox Status { get; set; }
        public DogovorNewShablon()
        {
            Status = new System.Windows.Controls.CheckBox();
        }
    }
}
