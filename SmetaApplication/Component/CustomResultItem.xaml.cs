using SmetaApplication.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmetaApplication.Component
{
    /// <summary>
    /// Interaction logic for CustomResultItem.xaml
    /// </summary>
    public partial class CustomResultItem : UserControl
    {
        public CustomResultItem(WorkDemView WorkDemView)
        {
            InitializeComponent();
            DataContext = WorkDemView;
        }

        private void btn_Cancel(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Ok(object sender, RoutedEventArgs e)
        {

        }
    }
}
