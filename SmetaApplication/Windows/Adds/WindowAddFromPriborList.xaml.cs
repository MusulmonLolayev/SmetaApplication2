using SmetaApplication.Context;
using SmetaApplication.Models.Material;
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
    /// Логика взаимодействия для WindowAddFromPriborList.xaml
    /// </summary>
    public partial class WindowAddFromPriborList : Window
    {
        public ObservableCollection<PriborContext> list;
        public WindowAddFromPriborList(ObservableCollection<PriborContext> PriborContext)
        {
            InitializeComponent();
            list = new ObservableCollection<PriborContext>();
            data.ItemsSource = list;
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                foreach (var item in db.Pribors)
                {
                    PriborContext search = PriborContext.Where(x => x.Pribor.Id == item.Id).FirstOrDefault();
                    if (search != null)
                    {
                        list.Add(search);
                    }
                    else
                    {
                        list.Add(new PriborContext(item));
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
