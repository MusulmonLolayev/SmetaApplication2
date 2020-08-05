using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace SmetaApplication.Models.Report
{
    [Serializable]
    public class ReportStructure
    {
        public int N { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public double? Percentage { get; set; }

        public ReportStructure()
        {
            Percentage = null;
        }
    }
}
