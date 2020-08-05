using System.ComponentModel;

namespace SmetaApplication.Filtrs
{
    public class EPlaceWork
    {
        [Description("Code")]
        public int Code { get; set; }

        [Description("Name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
