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


namespace SmetaApplication.Models.Commentary
{
    public class Commentary : AbstractModel
    {
        #region Properies
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
                OnPropertyChanged();
            }
        }

        private double koef;
        public double Koef
        {
            get { return koef; }
            set { koef = value; OnPropertyChanged(); }
        }

        public long WorkSectionId { get; set; }
        #endregion

        public Commentary()
        {

        }
        public Commentary(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            Name = dataRow.ItemArray[1].ToString();
            Koef = double.Parse(dataRow.ItemArray[2].ToString());
            WorkSectionId = long.Parse(dataRow.ItemArray[3].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into Commentaries " +
            "(Name, Koef, WorkSectionId) Values ("
            + "'" + Name + "'," + Helper.ToString(Koef) + ", " + WorkSectionId + ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Commentaries Set " +
               "Name = '" + Name.Replace(',', '.') + "', Koef = " + Helper.ToString(Koef)  +
               " Where Id = " + WorkSectionId;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From Commentaries Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }

        #endregion
    }
}
