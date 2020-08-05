using SmetaApplication.DbContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Models.Contract
{
    public class Contract : AbstractModel
    {
        #region Properties
        public long Id { get; set; }

        private string number = " ";
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        private DateTime date;
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
                OnPropertyChanged("Name");
            }
        }

        private string client = " ";
        public string Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                OnPropertyChanged("Client");
            }
        }

        private string executer = "“O‘ZGASHKLITI” DUK";

        public Contract()
        {
            date = DateTime.Today;
        }

        public Contract(DataRow dataRow)
        {
            Id = long.Parse(dataRow.ItemArray[0].ToString());
            number = dataRow.ItemArray[1].ToString();
            date = DateTime.Parse(dataRow.ItemArray[2].ToString());
            name = dataRow.ItemArray[3].ToString();
            client = dataRow.ItemArray[4].ToString();
            executer = dataRow.ItemArray[5].ToString();
        }

        public string Executer
        {
            get
            {
                return executer;
            }
            set
            {
                executer = value;
                OnPropertyChanged("Executer");
            }
        }

        #endregion

        public override void Save()
        {
            string query = "Insert Into Contracts " +
                "(Number, Date, Name, Client, Executer) Values (" +
                "'" + number.Replace('\'', '‘').Replace('"', '“') + "', " +
                "'" + date + "', " +
                "'" + name.Replace('\'', '‘').Replace('"', '“') + "', " +
                "'" + client.Replace('\'', '‘').Replace('"', '“') + "', " + 
                "'" + executer.Replace('\'', '‘').Replace('"', '“') + "')";
            Id = DBConnection.Save(query);
            IsUpdated = false;
        }

        public override bool Update()
        {
            if (IsUpdated == false)
                return true;

            string query = "Update Contracts Set " +
                "Number = '" + number + "', " +
                "Date = '" + date + "', " +
                "Name = '" + name + "'" +
                "Client = '" + client + "'" +
                "Executer = '" + executer + "'" +
                " Where Id = " + Id; ;

                bool result = DBConnection.Update(query) > 0;
            IsUpdated = false;
            return result;
        }

        public override bool Delete()
        {
            if (IsDeleted)
                return false;
            string query = "Delete From Contracts Where Id = " + Id;
            bool result = DBConnection.Delete(query) > 0;
            IsDeleted = true;
            return result;
        }
    }
}
