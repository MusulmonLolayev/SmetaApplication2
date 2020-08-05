using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SmetaApplication.Models.WorkModels;
using SmetaApplication.Methods;
using SmetaApplication.Models.GroupMaterial;
using System;
using System.Collections.ObjectModel;
using SmetaApplication.Context;
using System.Windows;

namespace SmetaApplication.ViewModels
{
    public class SelectedWorkView : INotifyPropertyChanged, ICloneable
    {
        #region Properties
        private Work work;
        public Work Work
        {
            get { return work; }
            set
            {
                work = value;
                OnPropertyChanged();
            }
        }

        private string diffucult;
        [DisplayName("Категория сложности")]
        public string Diffucult
        {
            get { return diffucult; }
            set { diffucult = value; OnPropertyChanged(); }
        }

        private double time;
        [DisplayName("Норма")]
        public double Time
        {
            get { return time; }
            set { time = value; OnPropertyChanged(); }
        }

        private double? koef;
        [DisplayName("Коэффициенты")]
        public double? Koef
        {
            get { return koef; }
            set {
                koef = value;
                if (koef != null)
                {
                    PayWithKoef = Work.PricePay * koef;
                    ChangeAllPay();
                }
                OnPropertyChanged();
            }
        }

        private double? payWithKoef;
        [DisplayName("ВСЕГО заработная плата")]
        public double? PayWithKoef
        {
            get { return payWithKoef; }
            set { payWithKoef = value; OnPropertyChanged();}
        }

        private double allPay;
        [DisplayName("Стоимость  продукции")]
        public double AllPay
        {
            get { return allPay; }
            set { allPay = value; OnPropertyChanged(); }
        }

        private double count;
        [DisplayName("Объем")]
        public double Count
        {
            get { return count; }
            set
            {
                count = value;
                if (count != 0)
                    Price = count * AllPay;
                OnPropertyChanged();
            }
        }

        private double price;
        [DisplayName("Стоимость  продукции")]
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        public int Diff;

        public ObservableCollection<PropertyKoefView> Commentary { get; set; }

        public ObservableCollection<MaterialMainView> Materials { get; set; }

        public ObservableCollection<PriborGroupView> Pribors { get; set; }

        #endregion

        public SelectedWorkView(Work Work, int Diff)
        {
            this.Work = (Work)Work.Clone();
            this.Diff = Diff;
            PayWithKoef = Work.PricePay;
            Commentary = new ObservableCollection<PropertyKoefView>();
            Materials = new ObservableCollection<MaterialMainView>();
            Pribors = new ObservableCollection<PriborGroupView>();
            SetDefault();
            ChangeAllPay();

            
        }

        private void ChangeAllPay()
        {
            AllPay = 0;
            if (PayWithKoef != null)
                AllPay += (double)PayWithKoef;
            if (Work.PriceMaterial != null)
                AllPay += (double)Work.PriceMaterial;
            if (Work.PricePribor != null)
                AllPay += (double)Work.PricePribor;
            AllPay *= 1.21 * 1.1;
            if (count != 0)
            {
                Price = AllPay * count;
            }
        }

        public void RefreshChangedPribor()
        {
            Work.PricePribor = Pribors.Where(x => x.Status).
                Select(x => x.Pribor.Price * x.Count * Time * x.Pribor.Percent / 12 / 30 / 24 / 100).Sum();
            ChangeAllPay();
        }

        private void SetDefault()
        {
            Time = (double)Helper.Time(Work, Diff);
            // Vaqtni bir soatga to'langan pulga hamma vaqtni ko'paytirib
            if (Diff == 1)
                Work.PricePay *= Time;
            Diffucult = Helper.Diffucults[Diff - 1];
            //if (Diff > 1)
            //{
                using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                {
                    // Read Material group
                    List<MaterialGroup> list = db.MaterialGroups.ToList().Where(x => x.WorkId == Work.Id).ToList();
                    Work.PriceMaterial = 0;
                    list.ForEach(x =>
                    {
                        var current = db.Materials.ToList().Where(y => y.Id == x.MaterialId).First();
                        double d = (double)Helper.Count(x, Diff);
                        Work.PriceMaterial += d * current.Price;
                        Materials.Add(new MaterialMainView()
                        {
                            Count = d,
                            Material = current,
                            Status = true
                        });
                    });

                    // Read Pribor group
                    List<PriborGroup> list1 = db.PriborGroups.ToList().Where(x => x.WorkId == Work.Id).ToList();
                    Work.PricePribor = 0;
                    list1.ForEach(x =>
                    {
                        var current = db.Pribors.ToList().Where(y => y.Id == x.PriborId).First();
                        Work.PricePribor += current.Price * x.Count * current.Percent / 100 / 12 / 30 / 24;
                        //Pribors.Add(new PriborGroupView()
                        //{
                        //    Count = x.Count,
                        //    Pribor = current,
                        //    Status = true
                        //});
                    });
                }
            //}
        }

        public void RefreshChangedMaterial()
        {
            Work.PriceMaterial = Materials.Where(x => x.Status).
                Select(x => x.Material.Price * x.Count).Sum();
            ChangeAllPay();
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

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
