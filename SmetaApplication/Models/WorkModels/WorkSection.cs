using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;
using SmetaApplication.DbContexts;
using SmetaApplication.Methods;

namespace SmetaApplication.Models.WorkModels
{
    public class WorkSection : AbstractModel
    {
        #region Properies
        [DisplayName("ИД")]
        [Browsable(false)]
        public long Id { get; set; }

        private string name;
        [DisplayName("Имя")]
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

        private string content;
        [DisplayName("Содержание")]
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("ИД типы работ")]
        [Browsable(false)]
        public long WorkTypeId
        {
            get;
            set;
        }

        private int place;
        [DisplayName("Место работа")]
        public int Place
        {
            get
            {
                return place;
            }
            set
            {
                place = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public WorkSection()
        {

        }
        public WorkSection(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            name = dataRow.ItemArray[1].ToString();
            content = dataRow.ItemArray[2].ToString();
            WorkTypeId = long.Parse(dataRow.ItemArray[3].ToString());
            place = int.Parse(dataRow.ItemArray[4].ToString());
        }

        public override string ToString()
        {
            return Name;
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into WorkSections " +
                "(Name, Content, WorkTypeId, Place) Values ("
                + "'" + Name + "','" + content + "'," + WorkTypeId + ", " + place + ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update WorkSections Set " +
                "Name = '" + Name + "', Content = '" + content + "', WorkTypeId = " + WorkTypeId +
                ", Place = " + Place +
                " Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From WorkSections Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
