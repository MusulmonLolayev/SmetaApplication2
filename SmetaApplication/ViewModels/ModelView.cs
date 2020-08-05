using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.ViewModels
{
    public class ModelView : INotifyPropertyChanged, ICloneable
    {
        #region Properties
        private int? n;
        public int? N
        {
            get { return n; }
            set { n = value; OnPropertyChanged("N"); }
        }
        #endregion

        #region Prperty change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
