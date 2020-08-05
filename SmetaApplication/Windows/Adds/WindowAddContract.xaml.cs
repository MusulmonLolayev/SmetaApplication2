using SmetaApplication.Models.Contract;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for WindowAddContract.xaml
    /// </summary>
    public partial class WindowAddContract : Window
    {
        public WindowAddContract(Contract contract)
        {
            InitializeComponent();
            DataContext = contract;
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
