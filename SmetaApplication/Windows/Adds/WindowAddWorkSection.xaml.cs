using SmetaApplication.Context;
using SmetaApplication.Models.Commentary;
using SmetaApplication.Models.WorkModels;
using SmetaApplication.ViewModels;
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
    /// Логика взаимодействия для WindowAddWorkSection.xaml
    /// </summary>
    public partial class WindowAddWorkSection : Window
    {
        private ContextWorkSection ContextWorkSection;

        public WindowAddWorkSection(ContextWorkSection ContextWorkSection)
        {
            InitializeComponent();
            this.DataContext = ContextWorkSection;
            this.ContextWorkSection = ContextWorkSection;
            //this.CbbWorkType.SelectedIndex = 0;
        }

        private void OnClickAddCommentary(object sender, RoutedEventArgs e)
        {
            ContextWorkSection.Comentaries.Add(new Commentary());
        }

        private void OnClickDeleteCommentary(object sender, RoutedEventArgs e)
        {
            if (ContextWorkSection.SelectedCommentary != null)
            {
                ContextWorkSection.Comentaries.Remove(ContextWorkSection.SelectedCommentary);
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            if (rbpol.IsChecked == true)
                ContextWorkSection.WorkSection.Place = 0;
            if (rbkam.IsChecked == true)
                ContextWorkSection.WorkSection.Place = 1;
            if (rblab.IsChecked == true)
                ContextWorkSection.WorkSection.Place = 2;
            DialogResult = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ContextWorkSection.WorkSection.WorkTypeId = ((WorkType)CbbWorkType.SelectedValue).Id;
        }
    }
}
