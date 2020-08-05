using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmetaApplication.Models.WorkModels;
using System.Windows;

namespace SmetaApplication.ViewModels
{
    public class WorkListView : INotifyPropertyChanged
    {
        public Work Work { get; set; }

        public string WorkName { get; set; }

        public WorkListView(Work Work)
        {
            this.Work = Work;
            //try
            //{
            //    WorkName = WorkSections.Where(x => x.Id == Work.WorkSectionId).First().Name;
            //}
            //catch
            //{
            //    //MessageBox.Show(Work.Number.ToString() + " : " + Work.WorkSectionId.ToString());
            //}
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
