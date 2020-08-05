using SmetaApplication.Methods;
using SmetaApplication.Models.WorkModels;
using SmetaApplication.Models.Commentary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using SmetaApplication.Context;

namespace SmetaApplication.ViewModels
{
    public class WorkDemView : ModelView
    {
        private delegate void SizeChanged();

        #region Properties
        public long Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Diff
        {
            get;
            set;
        }

        private string number;
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        private string number2;
        public string Number2
        {
            get
            {
                return number2;
            }
            set
            {
                number2 = value;
                OnPropertyChanged("Number2");
            }
        }

        private string measure;
        public string Measure
        {
            get
            {
                return measure;
            }
            set
            {
                measure = value;
                OnPropertyChanged("Measure");
            }
        }

        private double pricePay;
        public double PricePay
        {
            get
            {
                return Math.Round(pricePay * 1.25, 2);
            }
            set
            {
                pricePay = value;
                OnPropertyChanged("PricePay");
            }
        }

        public double? PriceMaterial
        {
            get
            {
                if (Materials.Count > 0)
                    return Materials.Where(x => x.IsYes).Sum(x => x.Count * x.Material.Price);
                return null;
            }
            private set
            {
            }
        }

        public double? PricePribor
        {
            get
            {
                if (Pribors.Count == 0)
                    return null;

                return Pribors.Where(x => x.IsYes).ToList().Sum(x => Math.Round(x.Pribor.Price * x.Pribor.Percent / 100 / 12, 2));
            }
            private set
            {
            }
        }

        public long WorkSectionId
        {
            get;
            set;
        }

        private double time;
        public double Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public void FillOrClearTeamWorkers(bool v)
        {
            if (v)
            {
                using (var db = new DbContexts.SmetaDbAppContext())
                {
                    db.WorkTeams.Where(x => x.WorkDemId == Id).ToList().ForEach(x =>
                    {
                        TeamContexts.Add(new TeamContext(x));
                    });
                }
            }
            else
            {
                TeamContexts.Clear();
            }
        }

        /// <summary>
        /// Extra properties for view
        /// </summary>
        /// 

        private double? size;
        public double? Size
        {
            get
            {
                return size;
            }
            set
            {
                //MessageBox.Show(Size.ToString());
                size = value;
                LoadOrClear();
                OnPropertyChanged("Size");
                OnPropertyChanged("PricePayAllView");
                OnPropertyChanged("PriceMaterialForSize");
                OnPropertyChanged("PricePriborForSize");
                OnPropertyChanged("PriceMaterial");
                OnPropertyChanged("PricePriborInHour");
                OnPropertyChanged("AllCost");
                EnterSize?.Invoke(this, null);
            }
        }

        public ObservableCollection<TeamContext> TeamContexts { get; set; }

        public double? Koef
        {
            get
            {
                if (Commentaries.Count > 0)
                {
                    double p = 1;
                    Commentaries.Where(x => x.IsYes).ToList().ForEach(x =>
                    {
                        p *= x.Commentary.Koef;
                    });
                    return p;
                }
                return null;
            }
            private set
            {
                /*koef = value*/;
                OnPropertyChanged("Koef");
            }
        }

        public ObservableCollection<CommentaryView> Commentaries { get; set; }

        private double? polviyDovol;
        public double? PolviyDovol
        {
            get
            {
                return polviyDovol;
            }
            set
            {
                polviyDovol = value;
                OnPropertyChanged("PolviyDovol");
                OnPropertyChanged("PricePayAllView");
            }
        }

        private double? pricePayAll;
        public double? PricePayAllView
        {
            get
            {
                double? d = size * PricePay * time * (Koef == null ? 1 : Koef) + (polviyDovol == null ? 0 : polviyDovol);
                if (d != null)
                    d = Math.Round((double)d, 2);
                pricePayAll = d;
                return d;
            }
            private set
            {
                pricePayAll = value;
            }
        }

        public double? PricePayAll { get; set; }

        public ObservableCollection<MaterialGroupDemView> Materials { get; set; }

        public double? PriceMaterialForSize
        {
            get
            {
                if (Materials.Count == 0 || size == null)
                    return null;
                double? s = 0;
                Materials.Where(x => x.IsYes).ToList().ForEach(x =>
                {
                    if (x.ForAllObject)
                        s += Math.Round(x.Material.Price * x.Count, 2);
                    else
                        s += Math.Round((double)(size * x.Material.Price * x.Count), 2);
                });
                return s;
            }
            private set
            {

            }
        }

        public ObservableCollection<PriborGroupView> Pribors { get; set; }

        public double? PricePriborInHour
        {
            get
            {
                double? d = PricePribor / Helper.ToHourKoef;
                if (size != null && d != null)
                    d = Math.Round((double)d, 2);
                else d = 0;
                return d;
            }
            private set
            {
            }
        }

        public double? PricePriborForSize
        {
            get
            {
                double? d = 0;
                if (size != null)
                    d = Pribors.
                        Where(x => x.IsYes).ToList().
                        Sum(x =>
                        Math.Round(Math.Round(x.Pribor.Price * x.Pribor.Percent / 100 / 12 / Helper.ToHourKoef, 2) * (double)size * time, 2));
                else return null;
                return d;
            }
            private set
            {

            }
        }

        public double? AllCost
        {
            get
            {
                return PricePayAllView + PriceMaterialForSize + PricePriborForSize;
            }
            private set
            {

            }
        }

        private int diff;

        #endregion

        public WorkDemView()
        {
            
        }
        
        public WorkDemView(Work Work, int diff)
        {
            #region copy all properties
            Id = Work.Id;
            name = Work.Name;
            Diff = Helper.Diffucults[diff];
            this.diff = diff;
            number = Work.Number;
            number2 = Work.Number2;
            measure = Work.Measure;
            pricePay = Work.PricePay;
            PriceMaterial = Work.PriceMaterial;
            PricePribor = Work.PricePribor;
            WorkSectionId = Work.WorkSectionId;
            time = (double)Helper.Time(Work, diff);
            #endregion

            // loads commentaries, materials and pribors
            Commentaries = new ObservableCollection<CommentaryView>();
            Materials = new ObservableCollection<MaterialGroupDemView>();
            Pribors = new ObservableCollection<PriborGroupView>();
            TeamContexts = new ObservableCollection<TeamContext>();
        }

        private void LoadOrClear()
        {
            if (size != null && !(Commentaries.Any() && Materials.Any() && Pribors.Any()))
            {
                using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                {
                    Materials.Clear();
                    db.MaterialGroups.Where(x => x.WorkId == Id).ToList().ForEach(x =>
                    {
                        Materials.Add(new MaterialGroupDemView(x, diff + 1));
                    });
                    Pribors.Clear();
                    db.PriborGroups.Where(x => x.WorkId == Id).ToList().ForEach(x =>
                    {
                        Pribors.Add(new PriborGroupView(x));
                    });
                    Commentaries.Clear();
                    db.Commentaries.Where(x => x.WorkSectionId == WorkSectionId).ToList().ForEach(x =>
                    {
                        Commentaries.Add(new CommentaryView(x));
                    });
                }
            }
            else
            {
                Commentaries.Clear();
                Materials.Clear();
                Pribors.Clear();
            }
        }

        public event EventHandler EnterSize;

        public void ChangedKoef()
        {
            OnPropertyChanged("Koef");
            OnPropertyChanged("PricePayAllView");
            OnPropertyChanged("PriceMaterialForSize");
            OnPropertyChanged("PricePriborForSize");
            OnPropertyChanged("PriceMaterial");
            OnPropertyChanged("PricePriborInHour");
            OnPropertyChanged("AllCost");
        }
    }
    class WorkDemViewArgs
    {
        public WorkDemView WorkDemView { get; set; }
        public int Index { get; set; }
    }
}