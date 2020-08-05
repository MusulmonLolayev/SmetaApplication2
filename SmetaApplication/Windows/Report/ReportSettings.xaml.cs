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
using SmetaApplication.Context;

namespace SmetaApplication.Windows.Report
{
    /// <summary>
    /// Interaction logic for ReportSettings.xaml
    /// </summary>
    public partial class ReportSettings : Window
    {
        public ReportSettings()
        {
            InitializeComponent();
        }

        public ReportSettings(ReportContext reportContext)
        {
            InitializeComponent();
            DataContext = reportContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ReportContext report = (ReportContext)DataContext;
            //MessageBox.Show(report.TypeExploreKoef.ToString());
            DialogResult = false;
        }
    }
}
