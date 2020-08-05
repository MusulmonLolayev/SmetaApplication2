using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmetaApplication.Models.WorkModels;
using System.Collections.ObjectModel;
using System.Windows;
using SmetaApplication.Models.Material;

namespace SmetaApplication.Context
{
    public class WorkContext : INotifyPropertyChanged
    {
        public Work Work { get; set; }

        private WorkSection selectedSection;
        public WorkSection SelectedSection
        {
            get { return selectedSection; }
            set { selectedSection = value; }
        }

        public ObservableCollection<WorkSection> WorkSections
        {
            get;set;
        }


        private WorkType selectedWorkType;
        public WorkType SelectedWorkType
        {
            get { return selectedWorkType; }
            set
            {
                WorkSections.Clear();
                if (value != null)
                {
                    selectedWorkType = value;
                    using (var db = new DbContexts.SmetaDbAppContext())
                    {
                        db.WorkSections.ForEach(x =>
                        {
                            if (x.WorkTypeId == selectedWorkType.Id)
                                WorkSections.Add(x);
                        });
                    }
                    if (WorkSections.Any())
                        SelectedSection = WorkSections[0];
                    OnPropertyChanged("SelectedSection");
                }
            }
        }

        private List<WorkType> workTypes;
        public List<WorkType> WorkTypes
        {
            get { return workTypes; }
            private set { workTypes = value; }
        }


        public ObservableCollection<TeamContext> TeamContexts { get; set; }

        public ObservableCollection<MaterialContext> MaterailContexts { get; set; }

        public ObservableCollection<PriborContext> PriborContexts { get; set; }

        public WorkContext()
        {
            WorkSections = new ObservableCollection<WorkSection>();
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                db.WorkSections.ForEach(x =>
                {
                    WorkSections.Add(x);
                });

                workTypes = db.WorkTypes;
                SelectedWorkType = workTypes[0];
            }

            TeamContexts = new ObservableCollection<TeamContext>();
            MaterailContexts = new ObservableCollection<MaterialContext>();
            PriborContexts = new ObservableCollection<PriborContext>();
            Work = new Work();
        }

        public WorkContext(Work Work)
        {
            this.Work = Work;
            TeamContexts = new ObservableCollection<TeamContext>();
            MaterailContexts = new ObservableCollection<MaterialContext>();
            PriborContexts = new ObservableCollection<PriborContext>();

            WorkSections = new ObservableCollection<WorkSection>();

            using (var db = new DbContexts.SmetaDbAppContext())
            {
                db.WorkTeams.Where(x => x.WorkDemId == Work.Id).ToList().ForEach(x =>
                {
                    TeamContexts.Add(new TeamContext(x));
                });

                List<Material> materials = db.Materials;

                // WOrk types
                WorkTypes = db.WorkTypes;
                selectedWorkType = WorkTypes.Where(x => x.Id == 
                (db.WorkSections.Where(y => y.Id == Work.WorkSectionId)).FirstOrDefault().WorkTypeId).FirstOrDefault();

                //Work sections
                db.WorkSections.Where(x => x.WorkTypeId == selectedWorkType.Id).ToList().ForEach(x => 
                {
                    WorkSections.Add(x);
                });
                selectedSection = WorkSections.Where(x => x.Id == Work.WorkSectionId).FirstOrDefault();

                // Fill Materials
                db.MaterialGroups.Where(x => x.WorkId == Work.Id).ToList().ForEach(x =>
                {
                    var item = new MaterialContext(x);
                    item.Material = materials.Where(y => y.Id == x.MaterialId).FirstOrDefault();

                    MaterailContexts.Add(item);
                });
                List<Pribor> pribors = db.Pribors;
                //Fill Pribors
                db.PriborGroups.Where(x => x.WorkId == Work.Id).ToList().ForEach(x =>
                {
                    var item = new PriborContext(x);
                    item.Pribor = pribors.Where(y => y.Id == x.PriborId).FirstOrDefault();

                    PriborContexts.Add(item);
                });
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