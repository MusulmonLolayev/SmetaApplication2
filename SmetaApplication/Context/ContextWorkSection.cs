using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmetaApplication.Models.WorkModels;
using System.Collections.ObjectModel;
using SmetaApplication.Models.Material;
using SmetaApplication.Models.Commentary;
using System.Windows.Input;
using SmetaApplication.Commands;
using System.Windows;

namespace SmetaApplication.Context
{
    public class ContextWorkSection : INotifyPropertyChanged
    {
        #region Properties
        public WorkSection WorkSection { get; set; }

        private bool? pol;
        public bool? Pol
        {
            get
            {
                pol = WorkSection.Place == 0 ? true : false;
                return pol;
            }
            set
            {
                pol = value;
                OnPropertyChanged();
                OnChangeRadioButton();
            }
        }

        private bool? kam;
        public bool? Kam
        {
            get
            {
                kam = WorkSection.Place == 1 ? true : false;
                return kam;
            }
            set
            {
                kam = value;
                OnPropertyChanged();
                OnChangeRadioButton();
            }
        }

        private bool? lab;
        public bool? Lab
        {
            get
            {
                lab = WorkSection.Place == 2 ? true : false;
                return lab;
            }
            set
            {
                lab = value;
                OnPropertyChanged();
                OnChangeRadioButton();
            }
        }

        private void OnChangeRadioButton()
        {
            if (pol == true)
            {
                WorkSection.Place = 0;
                kam = false;
                lab = false;
            }
            else
            {
                if (kam == true)
                {
                    WorkSection.Place = 1;
                    pol = false;
                    lab = false;
                }
                else
                    if (lab == true)
                {
                    WorkSection.Place = 2;
                    pol = false;
                    kam = false;
                }
            }
        }

        public List<WorkType> WorkTypes { get; set; }

        private WorkType selectedWorkType;
        public WorkType SelectedWorkType
        {
            get { return selectedWorkType; }
            set
            {
                selectedWorkType = value;
                if (selectedWorkType != null && WorkSection != null)
                    WorkSection.WorkTypeId = selectedWorkType.Id;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Commentary> Comentaries { get; set; }

        public Commentary SelectedCommentary { get; set; }

        #endregion

        public ContextWorkSection()
        {
            pol = false;
            kam = false;
            lab = false;
            using (var db = new DbContexts.SmetaDbAppContext())
            {
                WorkTypes = db.WorkTypes;
                if (WorkTypes.Count > 0)
                    selectedWorkType = WorkTypes[0];
            }
            WorkSection = new WorkSection();
            Comentaries = new ObservableCollection<Commentary>();
        }

        public void SetSelectedWorkType()
        {
            SelectedWorkType = WorkTypes.Where(x => x.Id == WorkSection.WorkTypeId).FirstOrDefault();
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
