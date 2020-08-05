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
    /// Логика взаимодействия для WindowMaterialList.xaml
    /// </summary>
    public partial class WindowMaterialList : Window
    {
        ObservableCollection<Material> list;
        public WindowMaterialList()
        {
            InitializeComponent();
            list = new ObservableCollection<Material>();

            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                foreach (var item in db.Materials)
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            Material material = new Material();
            Windows.Adds.WindowAddMaterial window = new Adds.WindowAddMaterial(material);
            if (window.ShowDialog() == true)
            {
                list.Add(material);
                material.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            Material material = list[data.SelectedIndex];
            Windows.Adds.WindowAddMaterial window = new Adds.WindowAddMaterial(material);
            if (window.ShowDialog() == true)
            {
                material.Update();
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
