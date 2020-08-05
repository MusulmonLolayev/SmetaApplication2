using SmetaApplication.DbContexts;
using SmetaApplication.Methods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Models.Material
{
    public class Material : AbstractModel
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
                OnPropertyChanged();
            }
        }

        private string code;
        [DisplayName("Код")]
        //[StringLength()]
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
        [DisplayName("Стоимость")]
        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        private string dimension;
        [DisplayName("Единица измерение")]
        public string Dimension
        {
            get { return dimension; }
            set { dimension = value; OnPropertyChanged(); }
        }
        #endregion

        public Material()
        {

        }

        public Material(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            Name = dataRow.ItemArray[1].ToString();
            Code = dataRow.ItemArray[2].ToString();
            Price = double.Parse(dataRow.ItemArray[3].ToString());
            Dimension = dataRow.ItemArray[4].ToString();
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into Materials " +
                "(Name, Code, Price, Dimension) Values ("
                + "'" + Name + "','" + Code + "'," + Helper.ToString(Price) + ", '" + Dimension + "')";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Materials Set " +
                "Name = '" + Name + "', Code = '" + Code + "', Price = " + Helper.ToString(Price) +
                ", Dimension = '" + Dimension + "' " +
                "Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From Materials Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
