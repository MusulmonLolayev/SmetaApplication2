using SmetaApplication.Models.Contract;
using SmetaApplication.Windows.Adds;
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
    /// Interaction logic for WindowContractList.xaml
    /// </summary>
    public partial class WindowContractList : Window
    {
        ObservableCollection<Contract> list;

        public Contract Selected;

        public WindowContractList()
        {
            InitializeComponent();

            list = new ObservableCollection<Contract>();

            using (var db = new DbContexts.SmetaDbAppContext())
            {
                foreach (var item in db.Contracts.OrderByDescending(x => x.Id))
                {
                    list.Add(item);
                }
            }
            data.ItemsSource = list;
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            Selected = list[data.SelectedIndex];
            DialogResult = true;
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            Contract contract = list[data.SelectedIndex];
            WindowAddContract window = new WindowAddContract(contract);
            if (window.ShowDialog() == true)
            {
                contract.Update();
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

        private void SelectedChanged(object sender, MouseButtonEventArgs e)
        {
            Selected = list[data.SelectedIndex];
            DialogResult = true;
        }
    }
}
