using SmetaApplication.Models.WorkModels;
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
    /// Interaction logic for WindowAddWorkType.xaml
    /// </summary>
    public partial class WindowAddWorkType : Window
    {
        public WindowAddWorkType(WorkType workType)
        {
            InitializeComponent();
            DataContext = workType;
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
