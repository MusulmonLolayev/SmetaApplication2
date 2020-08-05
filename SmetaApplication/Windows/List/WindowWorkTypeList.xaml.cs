using SmetaApplication.DbContexts;
using SmetaApplication.Models.Material;
using SmetaApplication.Models.WorkModels;
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
    /// Interaction logic for WindowWorkTypeList.xaml
    /// </summary>
    public partial class WindowWorkTypeList : Window
    {
        ObservableCollection<WorkType> list;
        public WindowWorkTypeList()
        {
            InitializeComponent();
            list = new ObservableCollection<WorkType>();

            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                foreach (var item in db.WorkTypes)
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            WorkType workType = new WorkType();
            Windows.Adds.WindowAddWorkType window = new Adds.WindowAddWorkType(workType);
            if (window.ShowDialog() == true)
            {
                list.Add(workType);
                workType.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            WorkType workType = list[data.SelectedIndex];
            Windows.Adds.WindowAddWorkType window = new Adds.WindowAddWorkType(workType);
            if (window.ShowDialog() == true)
            {
                workType.Update();
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
