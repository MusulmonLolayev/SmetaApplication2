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
using SmetaApplication.DbContexts;

namespace SmetaApplication.Windows.Adds
{
    /// <summary>
    /// Логика взаимодействия для WindowNewSection.xaml
    /// </summary>
    public partial class WindowNewSection : Window
    {
        WorkSection section;
        public WindowNewSection(WorkSection section)
        {
            InitializeComponent();
            this.section = section;
            DataContext = section;
            TypeWork.DisplayMemberPath = "Name";
            SetValues();
            //if (section.Id != null)
            //{
                using (var db = new SmetaDbAppContext())
                {
                    int i = 0;
                    foreach (var ob in db.WorkTypes)
                    {
                        if (ob.Id == section.WorkTypeId)
                            break;
                        i++;
                    }
                    TypeWork.SelectedIndex = i;
                }
                PlaceWork.SelectedItem = Constants.Constants.GetPlaceWork()[(int)section.WorkTypeId];
            //}
        }

        private void SetValues()
        {
            using (var db = new SmetaDbAppContext())
            {
                TypeWork.ItemsSource = db.WorkTypes.ToList();
                PlaceWork.ItemsSource = Constants.Constants.GetPlaceWork();
            }
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnOk(object sender, RoutedEventArgs e)
        {
            try
            {
                section.WorkTypeId = (TypeWork.SelectedItem as WorkType).Id;
                section.Place = PlaceWork.SelectedIndex;
                DialogResult = true;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }
    }
}
