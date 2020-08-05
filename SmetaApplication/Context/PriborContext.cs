using SmetaApplication.Models.GroupMaterial;
using SmetaApplication.Models.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Context
{
    public class PriborContext : INotifyPropertyChanged
    {
        private PriborGroup x;

        public Pribor Pribor { get; set; }

        public PriborGroup PriborGroup { get; set; }

        public PriborContext(Pribor Pribor)
        {
            this.Pribor = Pribor;
            PriborGroup = new PriborGroup();
            PriborGroup.PriborId = Pribor.Id;
        }

        public PriborContext(PriborGroup PriborGroup)
        {
            this.PriborGroup = PriborGroup;
        }

        #region Properties change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        #endregion
    }
}
