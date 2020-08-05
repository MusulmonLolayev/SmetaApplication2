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
    public class MaterialGroup : AbstractModel
    {
        #region Properies
        public long Id { get; set; }

        public long MaterialId { get; set; }

        public long WorkId { get; set; }

        // 1 for all objects, a 0 for for each objects
        public bool ForAllObject { get; set; }

        private double count1;
        public double Count1
        {
            get { return count1; }
            set
            {
                count1 = value;
                count2 = value;
                count3 = value;
                count4 = value;
                count5 = value;
                OnPropertyChanged();
            }
        }

        private double? count2;
        public double? Count2
        {
            get { return count2; }
            set { count2 = value; OnPropertyChanged(); }
        }

        private double? count3;
        public double? Count3
        {
            get { return count3; }
            set { count3 = value; OnPropertyChanged(); }
        }

        private double? count4;
        public double? Count4
        {
            get { return count4; }
            set { count4 = value; OnPropertyChanged(); }
        }

        private double? count5;
        public double? Count5
        {
            get { return count5; }
            set { count5 = value; OnPropertyChanged(); }
        }

        private double? count6;
        public double? Count6
        {
            get { return count6; }
            set { count6 = value; OnPropertyChanged(); }
        }

        private double? count7;
        public double? Count7
        {
            get { return count7; }
            set { count7 = value; OnPropertyChanged(); }
        }

        private double? count8;
        public double? Count8
        {
            get { return count8; }
            set { count8 = value; OnPropertyChanged(); }
        }

        private double? count9;
        public double? Count9
        {
            get { return count9; }
            set { count9 = value; OnPropertyChanged(); }
        }

        private double? count10;
        public double? Count10
        {
            get { return count10; }
            set { count10 = value; OnPropertyChanged(); }
        }

        private double? count11;
        public double? Count11
        {
            get { return count11; }
            set { count11 = value; OnPropertyChanged(); }
        }

        private double? count12;
        public double? Count12
        {
            get { return count12; }
            set { count12 = value; OnPropertyChanged(); }
        }

        private double? count13;
        public double? Count13
        {
            get { return count13; }
            set { count13 = value; OnPropertyChanged(); }
        }

        private double? count14;
        public double? Count14
        {
            get { return count14; }
            set { count14 = value; OnPropertyChanged(); }
        }

        private double? count15;
        public double? Count15
        {
            get { return count15; }
            set { count15 = value; OnPropertyChanged(); }
        }
        #endregion

        public MaterialGroup()
        {

        }

        public MaterialGroup(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            MaterialId = long.Parse(dataRow.ItemArray[1].ToString());
            WorkId = long.Parse(dataRow.ItemArray[2].ToString());
            ForAllObject = (bool)Helper.ToBool(dataRow.ItemArray[3].ToString());
            count1 = double.Parse(dataRow.ItemArray[4].ToString());
            count2 = Helper.ToDoubleNull(dataRow.ItemArray[5].ToString());
            count3 = Helper.ToDoubleNull(dataRow.ItemArray[6].ToString());
            count4 = Helper.ToDoubleNull(dataRow.ItemArray[7].ToString());
            count5 = Helper.ToDoubleNull(dataRow.ItemArray[8].ToString());
            count6 = Helper.ToDoubleNull(dataRow.ItemArray[9].ToString());
            count7 = Helper.ToDoubleNull(dataRow.ItemArray[10].ToString());
            count8 = Helper.ToDoubleNull(dataRow.ItemArray[11].ToString());
            count9 = Helper.ToDoubleNull(dataRow.ItemArray[12].ToString());
            count10 = Helper.ToDoubleNull(dataRow.ItemArray[13].ToString());
            count11 = Helper.ToDoubleNull(dataRow.ItemArray[14].ToString());
            count12 = Helper.ToDoubleNull(dataRow.ItemArray[15].ToString());
            count13 = Helper.ToDoubleNull(dataRow.ItemArray[16].ToString());
            count14 = Helper.ToDoubleNull(dataRow.ItemArray[17].ToString());
            count15 = Helper.ToDoubleNull(dataRow.ItemArray[18].ToString());
        }

        #region Data base actions
        public override void Save()
        {
            string query = "Insert Into MaterialGroups " +
                "(MaterialId, WorkId, ForAllObject, " +
                "Count1, Count2, Count3, Count4, Count5, Count6, Count7, Count8," +
                " Count9, Count10, Count11, Count12, Count13, Count14, Count15)" +
                " Values (" + MaterialId + ", " + WorkId + ", " + Helper.ToInt(ForAllObject) + ", " +
                Helper.ToString(count1) + ", " + Helper.ToStringNull(count2) + ", " + Helper.ToStringNull(count3) + ", " +
                Helper.ToStringNull(count4) + ", " + Helper.ToStringNull(count5) + ", " + Helper.ToStringNull(count6) + ", " +
                Helper.ToStringNull(count7) + ", " + Helper.ToStringNull(count8) + ", " + Helper.ToStringNull(count9) + ", " +
                Helper.ToStringNull(count10) + ", " + Helper.ToStringNull(count11) + ", " + Helper.ToStringNull(count12) + ", " +
                Helper.ToStringNull(count13) + ", " + Helper.ToStringNull(count14) + ", " + Helper.ToStringNull(count15) + ");";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;
            string query = "Update Works Set " +
                "MaterialId = " + MaterialId + ", " +
                "WorkId = " + WorkId + ", " +
                "ForAllObject = " + Helper.ToInt(ForAllObject) + ", " +
                "Count1 = " + Helper.ToString(count1) + ", " +
                "Count2 = " + Helper.ToStringNull(count2) + ", " +
                "Count3 = " + Helper.ToStringNull(count3) + ", " +
                "Count4 = " + Helper.ToStringNull(count4) + ", " +
                "Count5 = " + Helper.ToStringNull(count5) + ", " +
                "Count6 = " + Helper.ToStringNull(count6) + ", " +
                "Count7 = " + Helper.ToStringNull(count7) + ", " +
                "Count8 = " + Helper.ToStringNull(count8) + ", " +
                "Count9 = " + Helper.ToStringNull(count9) + ", " +
                "Count10 = " + Helper.ToStringNull(count10) + ", " +
                "Count11 = " + Helper.ToStringNull(count11) + ", " +
                "Count12 = " + Helper.ToStringNull(count12) + ", " +
                "Count13 = " + Helper.ToStringNull(count13) + ", " +
                "Count14 = " + Helper.ToStringNull(count14) + ", " +
                "Count15 = " + Helper.ToStringNull(count15) + ", " +
                "Where Id = " + Id;
            bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From MaterialGroups Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
        #endregion
    }
}
