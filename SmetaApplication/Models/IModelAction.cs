using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Models
{
    public interface IDataBaseAction
    {
        /// <summary>
        /// IsUpdated for 
        /// </summary>
        bool IsUpdated { get; set; }
        bool IsDeleted { get; set; }
        void Save();
        bool Update();
        bool Delete();
    }
}
