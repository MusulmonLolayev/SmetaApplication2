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
using System.Windows;

namespace SmetaApplication.Models.Amount
{
    public class Post : AbstractModel
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

        private int raz;
        [DisplayName("Разряд")]
        public int Raz
        {
            get
            {
                return raz;
            }
            set
            {
                raz = value;
                OnPropertyChanged();
            }
        }

        private double koef;
        [DisplayName("Коэффициент")]
        public double Koef
        {
            get { return koef; }
            set { koef = value; OnPropertyChanged(); }
        }

        private double pay;
        [DisplayName("Оклад")]
        public double Pay
        {
            get { return pay; }
            set { pay = value; OnPropertyChanged(); }
        }
        #endregion

        public Post()
        {

        }
        public Post(DataRow data)
        {
            //MessageBox.Show(data.ItemArray[2].ToString());
            Id = int.Parse(data.ItemArray[0].ToString());
            Name = data.ItemArray[1] as string;
            Raz = int.Parse(data.ItemArray[2].ToString());
            Koef = double.Parse(data.ItemArray[3].ToString());
            Pay = double.Parse(data.ItemArray[4].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into Posts " +
                "(Name, Raz, Koef, Pay) Values ("
                + "'" + Name + "'," + Raz + "," + Helper.ToString(Koef) + ", " + Helper.ToString(Koef) + ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Posts Set " +
                "Name = '" + Name + "', Raz = " + raz + ", Koef = " + Helper.ToString(koef) + ", Pay = " + Helper.ToString(pay) + " " +
                "Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From Posts Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
