using SmetaApplication.Models.Commentary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmetaApplication.Context
{
    public class PropertyKoefView : PropertyMainView, ICloneable, INotifyPropertyChanged
    {
        public Commentary Commentary
        {
            get;set;
        }

        public PropertyKoefView()
        {
            Commentary = new Commentary();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
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
