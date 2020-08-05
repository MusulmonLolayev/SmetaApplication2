using SmetaApplication.Models.Commentary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.ViewModels
{
    public class CommentaryView : INotifyPropertyChanged
    {
        private Commentary commentary;
        public Commentary Commentary
        {
            get { return commentary; }
            set { commentary = value; OnPropertyChanged(); }
        }

        private bool isYes;
        public bool IsYes
        {
            get { return isYes; }
            set { isYes = value; OnPropertyChanged(); }
        }

        public CommentaryView()
        {

        }

        public CommentaryView(Commentary Commentary)
        {
            this.Commentary = Commentary;
            IsYes = false;
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
