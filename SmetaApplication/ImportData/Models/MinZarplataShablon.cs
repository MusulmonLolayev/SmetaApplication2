using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Smeta.Models
{
   public class MinZarplataShablon
    {
        public string Id { get; set; }
        public string Sum { get; set; }
        public string Sana { get; set; }
        public string Asos { get; set; }
        public CheckBox Status { get; set; }
        public string N { get; set; }
        public  MinZarplataShablon()
        {
            Status = new CheckBox() { IsChecked = true };
        }
    }
}
