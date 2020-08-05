using SmetaApplication.DbContexts;
using SmetaApplication.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Models.Team
{
    public class WorkTeam : AbstractModel
    {
        #region Properies
        [Browsable(false)]
        public long Id { get; set; }

        private long workDemId;
        [Browsable(false)]
        public long WorkDemId
        {
            get
            {
                return workDemId;
            }
            set
            {
                workDemId = value;
                OnPropertyChanged();
            }
        }

        private long postId;
        [Browsable(false)]
        public long PostId
        {
            get
            {
                return postId;
            }
            set
            {
                postId = value;
                OnPropertyChanged();
            }
        }

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }

        private double koef;
        public double Koef
        {
            get { return koef; }
            set { koef = value; OnPropertyChanged(); }
        }

        #endregion

        public WorkTeam()
        {
            koef = 1;
        }

        public WorkTeam(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            workDemId = long.Parse(dataRow.ItemArray[1].ToString());
            postId = long.Parse(dataRow.ItemArray[2].ToString());
            count = int.Parse(dataRow.ItemArray[3].ToString());
            koef = long.Parse(dataRow.ItemArray[4].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into WorkTeams " +
                "(WorkDemId, PostId, Count, Koef) Values ("
                 + WorkDemId + ", " + PostId + ", " + Count + ", " + Helper.ToString(Koef) + ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update WorkTeams Set " +
                "WorkDemId = " + WorkDemId + ", PostId = " + PostId + ", Count = " + Count +
                ", Koef = " + Koef +
                " Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From WorkTeams Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
