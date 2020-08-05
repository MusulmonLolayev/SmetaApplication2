using SmetaApplication.DbContexts;
using SmetaApplication.Models.WorkModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Filtrs
{
    public sealed class WorkFilter
    {
        public event EventHandler PropertyChanged;

        private WorkType selectedWorkType;
        public WorkType SelectedWorkType
        {
            get { return selectedWorkType; }
            set
            {
                selectedWorkType = value;
                OnPropertyChanged();
            }
        }
        public List<WorkType> WorkTypes { get; set; }

        private EPlaceWork selectedPlace;
        public EPlaceWork SelectedPlace
        {
            get { return selectedPlace; }
            set
            {
                selectedPlace = value;
                OnPropertyChanged();
            }
        }
        public List<EPlaceWork> Places { get; set; }

        private WorkSection selectedSection;
        public WorkSection SelectedSection
        {
            get { return selectedSection; }
            set
            {
                selectedSection = value;
                if (selectedSection != null)
                    PropertyChanged?.Invoke(this, null);
            }
        }
        public ObservableCollection<WorkSection> Sections { get; set; }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                if (!string.IsNullOrEmpty(searchText))
                    PropertyChanged?.Invoke(this, null);
            }
        }

        public WorkFilter()
        {
            using (var db = new SmetaDbAppContext())
            {
                WorkTypes = db.WorkTypes;
                selectedWorkType = WorkTypes[0];
            }
            Places = new List<EPlaceWork>()
            {
                new EPlaceWork()
                {
                    Code = 0,
                    Name = "Полевые"
                },
                new EPlaceWork()
                {
                    Code = 1,
                    Name = "Камеральные"
                },
                new EPlaceWork()
                {
                    Code = 2,
                    Name = "Лабораторные"
                }
            };
            selectedPlace = Places[0];
            Sections = new ObservableCollection<WorkSection>();
            OnPropertyChanged();
        }

        private void OnPropertyChanged()
        {
            Sections.Clear();
            using (var db = new SmetaDbAppContext())
            {
                db.WorkSections.Where(x => x.WorkTypeId == selectedWorkType.Id && x.Place == selectedPlace.Code)
                    .ToList().ForEach(x =>
                    {
                        Sections.Add(x);
                    });
            }
        }
    }
}
