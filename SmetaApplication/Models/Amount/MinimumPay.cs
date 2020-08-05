using SmetaApplication.Attributs;
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

namespace SmetaApplication.Models.Amount
{
    public class MinimumPay : AbstractModel
    {
        
        #region Properties
        [DisplayName("ИД")]
        [Browsable(false)]
        public long Id { get; set; }

        private double pay;
        [DisplayName("Минимум зарплата")]
        public double Pay
        {
            get
            {
                return pay;
            }
            set
            {
                pay = value;
                OnPropertyChanged("Pay");
            }
        }

        private DateTime date = DateTime.Today;
        [DisplayName("Дата")]
        [DateTimeKind(DateTimeKind.Utc)]
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private string content;
        [DisplayName("Приказ")]
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

        private bool? status;
        [DisplayName("Статус")]
        public bool? Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }
        #endregion

        public MinimumPay()
        {

        }

        public MinimumPay(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            pay = double.Parse(dataRow.ItemArray[1].ToString());
            date = Helper.ToDateTime(dataRow.ItemArray[2]);
            content = dataRow.ItemArray[3] as string;
            status = Helper.ToBool(dataRow.ItemArray[4]);
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into MinimumPays " +
                "(Pay, Date, Content, Status) " +
                " Values(" + 
                Helper.ToString(Pay) + ",'" + Helper.ToString(Date) + "','" + Content + "'," + Helper.ToInt(status) + ")";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update MinimumPays Set " +
                " Pay = " + Helper.ToString(Pay) +
                ", Date = '" + Helper.ToString(Date) +
                "', Content = '" + Content + "'" +
                ", Status = " + Helper.ToInt(status) +
                " Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From MinimumPays Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
