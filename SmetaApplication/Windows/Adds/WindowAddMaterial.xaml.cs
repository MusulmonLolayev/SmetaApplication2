using SmetaApplication.Models.Material;
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
    /// Логика взаимодействия для WindowAddMaterial.xaml
    /// </summary>
    public partial class WindowAddMaterial : Window
    {
        public WindowAddMaterial(Material material)
        {
            InitializeComponent();
            DataContext = material;
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
