using SmetaApplication.DbContexts;
using SmetaApplication.Methods;
using SmetaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Models.WorkModels
{
    public class WorkData : AbstractModel
    {
        #region Properies
        public long Id { get; set; }

        private long workId;
        public long WorkId
        {
            get
            {
                return workId;
            }
            set
            {
                workId = value;
                OnPropertyChanged("WorkId");
            }
        }

        private long contractId;
        public long ContractId
        {
            get
            {
                return contractId;
            }
            set
            {
                contractId = value;
                OnPropertyChanged("ContractId");
            }
        }

        private int workDem;
        public int WorkDem
        {
            get
            {
                return workDem;
            }
            set
            {
                workDem = value;
                OnPropertyChanged("WorkDem");
            }
        }

        private double? size;
        public double? Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        private string commentIds;
        public string CommentIds
        {
            get
            {
                return commentIds;
            }
            set
            {
                commentIds = value;
                OnPropertyChanged("CommentIds");
            }
        }
        #endregion

        public WorkData()
        {

        }

        public WorkData(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            workId = long.Parse(dataRow.ItemArray[1].ToString());
            contractId = long.Parse(dataRow.ItemArray[2].ToString());
            workDem = int.Parse(dataRow.ItemArray[3].ToString());
            size = double.Parse(dataRow.ItemArray[4].ToString());
            commentIds = dataRow.ItemArray[5].ToString();
        }

        public WorkData(WorkDemView WorkDemView, long contractId)
        {
            workId = WorkDemView.Id;
            this.contractId = contractId;
            workDem = Helper.Diffucults.FindIndex(x => x == WorkDemView.Diff);
            size = WorkDemView.Size;
            string s = "";
            WorkDemView.Commentaries.Where(x => x.IsYes).ToList().ForEach(x =>
            {
                s += x.Commentary.Id + ",";
            });
            if (s != "")
            {
                s.Remove(s.Length - 1);
            }
            commentIds = s;
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into WorkData " +
                "(WorkId, ContractId, WorkDem, Size, CommentIds) Values ("+ 
                "" + workId + ", " +
                "" + contractId + ", " +
                "" + workDem + ", " +
                "" + Helper.ToString(size) + ", " +
                "'" + CommentIds + "'" +
                ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update WorkData Set " +
                "WorkId = " + workId + ", " +
                "ContractId = " + contractId + ", " +
                "WorkDem = " + workDem + ", " +
                "Size = " + Helper.ToString(size) + ", " +
                "CommentIds = '" + commentIds + "' " +
                "Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From WorkData Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
