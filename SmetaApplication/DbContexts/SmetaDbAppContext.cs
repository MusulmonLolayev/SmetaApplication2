using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using SmetaApplication.Models.Amount;
using SmetaApplication.Models.Commentary;
using SmetaApplication.Models.GroupMaterial;
using SmetaApplication.Models.Material;
using SmetaApplication.Models.Team;
using SmetaApplication.Models.WorkModels;
using System.Data;
using SmetaApplication.Models.Contract;

namespace SmetaApplication.DbContexts
{
    class SmetaDbAppContext : IDisposable
    {
        public List<MinimumPay> MinimumPays {
            get
            {
                List<MinimumPay> list = new List<MinimumPay>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From MinimumPays").Rows)
                {
                    list.Add(new MinimumPay(data));
                }
                return list;
            }
            private set {; } }

        public List<Post> Posts
        {
            get
            {
                List<Post> list = new List<Post>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Posts").Rows)
                {
                    list.Add(new Post(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Commentary> Commentaries
        {
            get
            {
                List<Commentary> list = new List<Commentary>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Commentaries").Rows)
                {
                    list.Add(new Commentary(data));
                }
                return list;
            }
            private set {; }
        }

        public List<MaterialGroup> MaterialGroups
        {
            get
            {
                List<MaterialGroup> list = new List<MaterialGroup>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From MaterialGroups").Rows)
                {
                    list.Add(new MaterialGroup(data));
                }
                return list;
            }
            private set {; }
        }

        public List<PriborGroup> PriborGroups
        {
            get
            {
                List<PriborGroup> list = new List<PriborGroup>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From PriborGroups").Rows)
                {
                    list.Add(new PriborGroup(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Material> Materials
        {
            get
            {
                List<Material> list = new List<Material>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Materials").Rows)
                {
                    list.Add(new Material(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Pribor> Pribors
        {
            get
            {
                List<Pribor> list = new List<Pribor>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Pribors").Rows)
                {
                    list.Add(new Pribor(data));
                }
                return list;
            }
            private set {; }
        }

        public List<WorkTeam> WorkTeams
        {
            get
            {
                List<WorkTeam> list = new List<WorkTeam>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From WorkTeams").Rows)
                {
                    list.Add(new WorkTeam(data));
                }
                return list;
            }
            private set {; }
        }

        public List<WorkSection> WorkSections
        {
            get
            {
                List<WorkSection> list = new List<WorkSection>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From WorkSections").Rows)
                {
                    list.Add(new WorkSection(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Work> Works
        {
            get
            {
                List<Work> list = new List<Work>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Works").Rows)
                {
                    list.Add(new Work(data));
                }
                return list;
            }
            private set {; }
        }

        public List<WorkType> WorkTypes
        {
            get
            {
                List<WorkType> list = new List<WorkType>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From WorkTypes").Rows)
                {
                    list.Add(new WorkType(data));
                }
                return list;
            }
            private set {; }
        }

        public List<Contract> Contracts
        {
            get
            {
                List<Contract> list = new List<Contract>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From Contracts").Rows)
                {
                    list.Add(new Contract(data));
                }
                return list;
            }
            private set {; }
        }

        public List<WorkData> WorkDatas
        {
            get
            {
                List<WorkData> list = new List<WorkData>();
                foreach (DataRow data in DBConnection.GetTableByQuery("Select *From WorkData").Rows)
                {
                    list.Add(new WorkData(data));
                }
                return list;
            }
            private set {; }
        }

        public SmetaDbAppContext()
        {
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SmetaDbAppContext() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
