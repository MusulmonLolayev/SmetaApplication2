using SmetaApplication.DbContexts;
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

namespace SmetaApplication.Windows.List
{
    /// <summary>
    /// Логика взаимодействия для WindowPriborList.xaml
    /// </summary>
    public partial class WindowPriborList : Window
    {
        ObservableCollection<Pribor> list;
        public WindowPriborList()
        {
            InitializeComponent();
            list = new ObservableCollection<Pribor>();

            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                foreach (var item in db.Pribors)
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            Pribor pribor = new Pribor();
            Windows.Adds.WindowAddPribor window = new Adds.WindowAddPribor(pribor);
            if (window.ShowDialog() == true)
            {
                list.Add(pribor);
                pribor.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            Pribor pribor = list[data.SelectedIndex];
            Windows.Adds.WindowAddPribor window = new Adds.WindowAddPribor(pribor);
            if (window.ShowDialog() == true)
            {
                pribor.Update();
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem != null &&
                MessageBox.Show("Вы хотите удалить", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                list[data.SelectedIndex].Delete();
                list.RemoveAt(data.SelectedIndex);
            }
        }
    }
}
