using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmetaApplication.Models.Amount;
using System.Windows.Controls;

namespace SmetaApplication.ViewModels
{
    public class MinimumPayView : INotifyPropertyChanged
    {
        public MinimumPay MinimumPay
        {
            get;set;
        }

        public MinimumPayView(MinimumPay MinimumPay)
        {
            this.MinimumPay = MinimumPay;
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
