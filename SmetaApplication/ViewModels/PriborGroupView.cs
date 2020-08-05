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
using SmetaApplication.Models.Material;

namespace SmetaApplication.ViewModels
{
    public class PriborGroupView : ModelView
    {
        #region Properies
        public long Id { get; set; }

        public Pribor Pribor { get; set; }

        public double count;
        public double Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(); }
        }

        // Extra properties for view
        public bool IsYes { get; set; }

        public double? PricePriborInHour { get
            {
                return Helper.ToAmortizationinHour(Pribor) * Count;
            } }

        private bool status;
        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        #endregion

        public PriborGroupView(PriborGroup PriborGroup)
        {
            Id = PriborGroup.Id;
            count = PriborGroup.Count;
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                Pribor = db.Pribors.Where(x => x.Id == PriborGroup.PriborId).SingleOrDefault();
            }
            IsYes = true;

        }
    }
}
