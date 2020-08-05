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


namespace SmetaApplication.Models.GroupMaterial
{
    public class PriborGroup : AbstractModel
    {
        #region Properies
        public long Id { get; set; }

        public long PriborId { get; set; }

        public long WorkId { get; set; }

        private double count;
        public double Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(); }
        }
        #endregion

        public PriborGroup()
        {
            count = 0;
        }

        public PriborGroup(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            PriborId = long.Parse(dataRow.ItemArray[1].ToString());
            WorkId = long.Parse(dataRow.ItemArray[2].ToString());
            count = int.Parse(dataRow.ItemArray[3].ToString());
        }


        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into PriborGroups " +
                "(PriborId, WorkId, Count )" +
                " Values (" + PriborId + ", " + WorkId + ", " + count + ");";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update PriborGroups Set " +
                "MaterialId = " + PriborId + ", " +
                "WorkId = " + WorkId + ", " +
                "Count = " + count + ", " +
                "Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From PriborGroups Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
