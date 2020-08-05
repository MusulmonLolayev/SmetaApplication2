using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using SmetaApplication.DbContexts;
using SmetaApplication.Methods;
using System.Data;
using System.Windows;

namespace SmetaApplication.Models.WorkModels
{
    public class Work : AbstractModel
    {
        #region Properties
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

        private string number;
        [DisplayName("Номер норма")]
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }
        
        // Number by RCN
        private string number2;
        [DisplayName("Номер норма")]
        public string Number2
        {
            get
            {
                return number2;
            }
            set
            {
                number2 = value;
                OnPropertyChanged();
            }
        }

        private string measure;
        [DisplayName("Единица имериния")]
        public string Measure
        {
            get
            {
                return measure;
            }
            set
            {
                measure = value;
                OnPropertyChanged();
            }
        }

        private double pricePay;
        [DisplayName("Заработная плата в сум")]
        public double PricePay
        {
            get
            {
                return pricePay;
            }
            set
            {
                pricePay = value;
                OnPropertyChanged();
            }
        }

        private double? priceMaterial;
        [DisplayName("Стоимость материалов в сум")]
        public double? PriceMaterial
        {
            get
            {
                return priceMaterial;
            }
            set
            {
                priceMaterial = value;
                OnPropertyChanged();
            }
        }

        private double? pricePribor;
        [DisplayName("Амортизация оборудования за месяц в сум")]
        public double? PricePribor
        {
            get
            {
                return pricePribor;
            }
            set
            {
                pricePribor = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Глава ИД")]
        [Browsable(false)]
        public long WorkSectionId
        {
            get;
            set;
        }

        private double time1;
        [DisplayName("Норма время")]
        public double Time1
        {
            get
            {
                return time1;
            }
            set
            {
                time1 = value;
                OnPropertyChanged();
            }
        }

        private double? time2;
        [DisplayName("Норма время")]
        public double? Time2
        {
            get
            {
                return time2;
            }
            set
            {
                time2 = value;
                OnPropertyChanged();
            }
        }

        private double? time3;
        [DisplayName("Норма время")]
        public double? Time3
        {
            get
            {
                return time3;
            }
            set
            {
                time3 = value;
                OnPropertyChanged();
            }
        }

        private double? time4;
        [DisplayName("Норма время")]
        public double? Time4
        {
            get
            {
                return time4;
            }
            set
            {
                time4 = value;
                OnPropertyChanged();
            }
        }

        private double? time5;
        [DisplayName("Норма время")]
        public double? Time5
        {
            get
            {
                return time5;
            }
            set
            {
                time5 = value;
                OnPropertyChanged();
            }
        }

        private double? time6;
        [DisplayName("Норма время")]
        public double? Time6
        {
            get
            {
                return time6;
            }
            set
            {
                time6 = value;
                OnPropertyChanged();
            }
        }

        private double? time7;
        [DisplayName("Норма время")]
        public double? Time7
        {
            get
            {
                return time7;
            }
            set
            {
                time7 = value;
                OnPropertyChanged();
            }
        }

        private double? time8;
        [DisplayName("Норма время")]
        public double? Time8
        {
            get
            {
                return time8;
            }
            set
            {
                time8 = value;
                OnPropertyChanged();
            }
        }

        private double? time9;
        [DisplayName("Норма время")]
        public double? Time9
        {
            get
            {
                return time9;
            }
            set
            {
                time9 = value;
                OnPropertyChanged();
            }
        }

        private double? time10;
        [DisplayName("Норма время")]
        public double? Time10
        {
            get
            {
                return time10;
            }
            set
            {
                time10 = value;
                OnPropertyChanged();
            }
        }

        private double? time11;
        [DisplayName("Норма время")]
        public double? Time11
        {
            get
            {
                return time11;
            }
            set
            {
                time11 = value;
                OnPropertyChanged();
            }
        }

        private double? time12;
        [DisplayName("Норма время")]
        public double? Time12
        {
            get
            {
                return time12;
            }
            set
            {
                time12 = value;
                OnPropertyChanged();
            }
        }

        private double? time13;
        [DisplayName("Норма время")]
        public double? Time13
        {
            get
            {
                return time13;
            }
            set
            {
                time13 = value;
                OnPropertyChanged();
            }
        }

        private double? time14;
        [DisplayName("Норма время")]
        public double? Time14
        {
            get
            {
                return time14;
            }
            set
            {
                time14 = value;
                OnPropertyChanged();
            }
        }

        private double? time15;
        [DisplayName("Норма время")]
        public double? Time15
        {
            get
            {
                return time15;
            }
            set
            {
                time15 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public Work()
        {

        }

        public Work(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            name = dataRow.ItemArray[1].ToString();
            number = dataRow.ItemArray[2].ToString();
            number2 = dataRow.ItemArray[3].ToString();
            measure = dataRow.ItemArray[4].ToString();
            pricePay = double.Parse(dataRow.ItemArray[5].ToString());
            priceMaterial = Helper.ToDoubleNull(dataRow.ItemArray[6].ToString());
            pricePribor = Helper.ToDoubleNull(dataRow.ItemArray[7].ToString());
            WorkSectionId = long.Parse(dataRow.ItemArray[8].ToString());
            time1 = double.Parse(dataRow.ItemArray[9].ToString());
            time2 = Helper.ToDoubleNull(dataRow.ItemArray[10].ToString());
            time3 = Helper.ToDoubleNull(dataRow.ItemArray[11].ToString());
            time4 = Helper.ToDoubleNull(dataRow.ItemArray[12].ToString());
            time5 = Helper.ToDoubleNull(dataRow.ItemArray[13].ToString());
            time6 = Helper.ToDoubleNull(dataRow.ItemArray[14].ToString());
            time7 = Helper.ToDoubleNull(dataRow.ItemArray[15].ToString());
            time8 = Helper.ToDoubleNull(dataRow.ItemArray[16].ToString());
            time9 = Helper.ToDoubleNull(dataRow.ItemArray[17].ToString());
            time10 = Helper.ToDoubleNull(dataRow.ItemArray[18].ToString());
            time11 = Helper.ToDoubleNull(dataRow.ItemArray[19].ToString());
            time12 = Helper.ToDoubleNull(dataRow.ItemArray[20].ToString());
            time13 = Helper.ToDoubleNull(dataRow.ItemArray[21].ToString());
            time14 = Helper.ToDoubleNull(dataRow.ItemArray[22].ToString());
            time15 = Helper.ToDoubleNull(dataRow.ItemArray[23].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into Works " +
                "(Name, Number, Number2, Measure, PricePay, PriceMaterial, PricePribor, WorkSectionId, " +
                "Time1, Time2, Time3, Time4, Time5, Time6, Time7, Time8, Time9, Time10, Time11, Time12, Time13, Time14, Time15 " +
                ") Values ("
                + "'" + name + "', '" + number + "', '" + number2 + "', '" + measure + "', " + Helper.ToString(pricePay) + ", " + 
                Helper.ToStringNull(priceMaterial) + ", " + Helper.ToStringNull(pricePribor) + ", " + WorkSectionId + ", " + 
                Helper.ToString(time1) + ", " + Helper.ToStringNull(time2) + ", " + Helper.ToStringNull(time3) + ", " +
                Helper.ToStringNull(time4) + ", " + Helper.ToStringNull(time5) + ", " + Helper.ToStringNull(time6) + ", " +
                Helper.ToStringNull(time7) + ", " + Helper.ToStringNull(time8) + ", " + Helper.ToStringNull(time9) + ", " +
                Helper.ToStringNull(time10) + ", " + Helper.ToStringNull(time11) + ", " + Helper.ToStringNull(time12) + ", " +
                Helper.ToStringNull(time13) + ", " + Helper.ToStringNull(time14) + ", " + Helper.ToStringNull(time15) + ");";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;

            string query = "Update Works Set " +
                "Name = '" + name + "', " +
                "Number = '" + number + "', " +
                "Number2 = '" + number2 + "', " +
                "Measure = '" + measure + "', " +
                "PricePay = " + Helper.ToString(pricePay) + ", " +
                "PriceMaterial = " + Helper.ToString(priceMaterial) + ", " +
                "PricePribor = " + Helper.ToString(pricePribor) + ", " +
                "WorkSectionId = " + WorkSectionId + ", " +
                "Time1 = " + Helper.ToString(time1) + ", " +
                "Time2 = " + Helper.ToStringNull(time2) + ", " +
                "Time3 = " + Helper.ToStringNull(time3) + ", " +
                "Time4 = " + Helper.ToStringNull(time4) + ", " +
                "Time5 = " + Helper.ToStringNull(time5) + ", " +
                "Time6 = " + Helper.ToStringNull(time6) + ", " +
                "Time7 = " + Helper.ToStringNull(time7) + ", " +
                "Time8 = " + Helper.ToStringNull(time8) + ", " +
                "Time9 = " + Helper.ToStringNull(time9) + ", " +
                "Time10 = " + Helper.ToStringNull(time10) + ", " +
                "Time11 = " + Helper.ToStringNull(time11) + ", " +
                "Time12 = " + Helper.ToStringNull(time12) + ", " +
                "Time13 = " + Helper.ToStringNull(time13) + ", " +
                "Time14 = " + Helper.ToStringNull(time14) + ", " +
                "Time15 = " + Helper.ToStringNull(time15) +
                " Where Id = " + Id;
            //MessageBox.Show(query);
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From Works Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
