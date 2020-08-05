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

namespace SmetaApplication.Models.Material
{
    public class Pribor : AbstractModel
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

        private string code;
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                OnPropertyChanged();
            }
        }

        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        public string dimension;
        public string Dimension
        {
            get { return dimension; }
            set { dimension = value; OnPropertyChanged(); }
        }

        private double percent;
        public double Percent
        {
            get
            {
                return percent;
            }
            set
            {
                percent = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public Pribor()
        {

        }
        public Pribor(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            Name = dataRow.ItemArray[1].ToString();
            Code = dataRow.ItemArray[2].ToString();
            Price = double.Parse(dataRow.ItemArray[3].ToString());
            Dimension = dataRow.ItemArray[4].ToString();
            Percent = double.Parse(dataRow.ItemArray[5].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into Pribors " +
                "(Name, Code, Price, Dimension, Percent) Values ("
                + "'" + Name + "','" + Code + "'," + Helper.ToString(Price) + ", '" + Dimension + "', " + Percent + ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Pribors Set " +
                "Name = '" + Name + "', Code = '" + Code + "', Price = " + Helper.ToString(Price) +
                ", Dimension = '" + Dimension +
                "', Percent = " + Percent +
                " Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From Pribors Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }

        public bool UpdateByCode()
        {
            string query = "Update Pribors Set " +
                "Name = '" + Name + "', Price = " + Helper.ToString(Price) +
                ", Dimension = '" + Dimension +
                "', Percent = " + Percent +
                " Where Code = " + Code;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }
        #endregion
    }
}
