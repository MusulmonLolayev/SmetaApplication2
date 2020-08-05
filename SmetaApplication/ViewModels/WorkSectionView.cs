using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmetaApplication.Models.WorkModels;
using SmetaApplication.Methods;
using System.Windows;

namespace SmetaApplication.ViewModels
{
    public class WorkSectionView : INotifyPropertyChanged
    {
        public WorkSection WorkSection { get; set; }

        private string book;
        [DisplayName("Книга работ")]
        public string Book
        {
            get
            {
                 using (var db = new DbContexts.SmetaDbAppContext())
                {
                    return book = db.WorkTypes.Where(x => x.Id == WorkSection.WorkTypeId).Single().Name;
                };
            }
        }

        private string place;
        [DisplayName("Место работа")]
        public string Place
        {
            get
            {
                return Helper.getName(WorkSection.Place); ;
            }
        }

        public WorkSectionView(WorkSection workSection)
        {
            this.WorkSection = workSection;
            using (var db = new DbContexts.SmetaDbAppContext())
            {
                book = db.WorkTypes.Where(x => x.Id == WorkSection.WorkTypeId).Single().Name;
            }
            place = Helper.getName(WorkSection.Place);
        }

        #region Properties change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            //using (var db = new DbContexts.SmetaDbAppContext())
            //{
            //    book = db.WorkTypes.Where(x => x.Id == WorkSection.WorkTypeId).Single().Name;
            //}
            //place = Helper.getName(WorkSection.Place);
        }
        #endregion
    }
}
