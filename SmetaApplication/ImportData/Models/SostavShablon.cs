using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportData.Models;
using Smeta.Models;
using System.Windows.Controls;

namespace Smeta.Models
{
    public class SostavShablon
    {
        public string Id { get; set; }
        public string N { get; set; }
        //Lavozim nomi
        public string Nom { get; set; }
        // Ishchi soni
        public string Son { get; set; }
        // Tarifniy razryad
        public string TR { get; set; }
        //Tarifniy oklad
        public string TO { get; set; }
        //Oyiga ishlash vaqti 168 soat
        public string OV { get; set; }
        // Soatiga qancha to'lanadi...
        public string SO { get; set; }
        public string Koef { get; set; }
        public string LavozimId { get; set; }

        public ComboBox cbLavozim { get; set; }

        public SostavShablon()
        {
            cbLavozim = new ComboBox();
            List<Lavozim> Llist = ReadXml.SelectLavozim(null, null);
            cbLavozim.DisplayMemberPath = "Nom";
            cbLavozim.ItemsSource = Llist.OrderBy(x => x.tarif_raziryad).Select(x => x).ToList<Lavozim>();
            Koef = 1.ToString();
            Son = 1.ToString();
            OV = "168";
            Id = "0";
        }
    }
}
