using SmetaApplication.Context;
using SmetaApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmetaApplication.Windows.Adds
{
    /// <summary>
    /// Логика взаимодействия для WindowAddFromMaterialList.xaml
    /// </summary>
    public partial class WindowAddFromMaterialList : Window
    {
        public ObservableCollection<MaterialContext> list;

        public WindowAddFromMaterialList(ObservableCollection<MaterialContext> MaterialContext)
        {
            InitializeComponent();
            list = new ObservableCollection<Context.MaterialContext>();
            data.ItemsSource = list;
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                foreach (var item in db.Materials)
                {
                    MaterialContext search;
                    if ((search = MaterialContext.Where(x => x.Material.Id == item.Id).FirstOrDefault()) != null)
                    {
                        list.Add(search);
                    }
                    else
                    {
                        list.Add(new MaterialContext(item));
                    }
                }
            }
        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
