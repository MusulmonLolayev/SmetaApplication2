using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmetaApplication.Context
{
    public abstract class PropertyMainView : INotifyPropertyChanged
    {
        private bool status;
        public bool Status
        {
            get { return status; }
            set { status = value;
                OnPropertyChanged();
            }
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
