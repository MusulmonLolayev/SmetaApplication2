using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Models
{
    public abstract class AbstractModel : IDataBaseAction, INotifyPropertyChanged, ICloneable
    {
        public bool IsUpdated
        {
            get;
            set;
        }
        public bool IsDeleted
        {
            get;
            set;
        }

        #region Prperty change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            IsUpdated = true;
        }

        #endregion

        public abstract bool Delete();

        public abstract void Save();

        public abstract bool Update();

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
