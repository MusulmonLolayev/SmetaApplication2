using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;
using SmetaApplication.DbContexts;

namespace SmetaApplication.Models.WorkModels
{
    public class WorkType : AbstractModel
    {
        #region Properies
        [DisplayName("ИД")]
        [Browsable(false)]
        public long Id { get; set; }

        private string name;
        [DisplayName("Наименование")]
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
        
        #endregion

        public WorkType()
        {

        }

        public WorkType(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            Name = dataRow.ItemArray[1].ToString();
        }

        public override string ToString()
        {
            return Name;
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into WorkTypes " +
                "(Name) Values ("
                + "'" + Name + "')";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update WorkTypes Set " +
                "Name = '" + Name + "' " +
                "Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From WorkTypes Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
