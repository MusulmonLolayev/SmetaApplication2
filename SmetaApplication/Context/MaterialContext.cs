using SmetaApplication.Models.GroupMaterial;
using SmetaApplication.Models.Material;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmetaApplication.Context
{
    public class MaterialContext : INotifyPropertyChanged
    {
        public Material Material { get; set; }

        public MaterialGroup MaterialGroup { get; set; }

        public MaterialContext(Material Material)
        {
            this.Material = Material;
            MaterialGroup = new MaterialGroup();
            MaterialGroup.MaterialId = Material.Id;
        }

        public MaterialContext(MaterialGroup MaterialGroup)
        {
            this.MaterialGroup = MaterialGroup;
            //using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            //{
            //    Material = db.Materials.Where(x => x.Id == MaterialGroup.MaterialId).FirstOrDefault();
            //}   
        }

        #region Prperty change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #endregion
    }
}
