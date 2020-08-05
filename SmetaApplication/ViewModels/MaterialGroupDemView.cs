using SmetaApplication.Methods;
using SmetaApplication.Models.Material;
using SmetaApplication.Models.GroupMaterial;
using System.Linq;

namespace SmetaApplication.ViewModels
{
    public class MaterialGroupDemView : ModelView
    {
        #region Properies
        public long Id { get; set; }

        // 1 for all objects, a 0 for for each objects
        public bool ForAllObject { get; set; }

        private double count;
        public double Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(); }
        }

        public double PriceNorma { get
            {
                return count * Material.Price;
            } }

        /// Extra properties for view
        /// 
        public Material Material { get; private set; }

        public bool IsYes { get; set; }

        #endregion

        public MaterialGroupDemView(MaterialGroup MaterialGroup, int diff)
        {
            Id = MaterialGroup.Id;
            ForAllObject = MaterialGroup.ForAllObject;
            Count = (double)Helper.Count(MaterialGroup, diff);

            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                Material = db.Materials.Where(x => x.Id == MaterialGroup.MaterialId).SingleOrDefault();
            }
            IsYes = true;
        }
    }
}
