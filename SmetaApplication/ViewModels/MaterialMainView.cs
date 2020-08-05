using SmetaApplication.Models.Commentary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmetaApplication.Context;
using SmetaApplication.Models.Material;

namespace SmetaApplication.ViewModels
{
    public class MaterialMainView : PropertyMainView, ICloneable, INotifyPropertyChanged
    {
        public Material Material { get; set; }

        public double Count { get; set; }

        public MaterialMainView()
        {
            
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
