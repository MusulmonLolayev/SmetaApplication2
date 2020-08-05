using SmetaApplication.Context;
using SmetaApplication.DbContexts;
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

namespace SmetaApplication.Windows.List
{
    /// <summary>
    /// Логика взаимодействия для WindowWorkSectionList.xaml
    /// </summary>
    public partial class WindowWorkSectionList : Window
    {
        ObservableCollection<WorkSectionView> list;

        public WindowWorkSectionList()
        {
            InitializeComponent();
            list = new ObservableCollection<WorkSectionView>();
            data.ItemsSource = list;
            using (var db = new SmetaDbAppContext())
            {
                foreach (var item in db.WorkSections)
                {
                    list.Add(new WorkSectionView(item));
                }
            }
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            ContextWorkSection ContextWorkSection = new ContextWorkSection();
            Adds.WindowAddWorkSection window = new Adds.WindowAddWorkSection(ContextWorkSection);
            if (window.ShowDialog() == true)
            {
                DBConnection.BeginTransaction();
                try
                {
                    ContextWorkSection.WorkSection.Save();
                    foreach (var item in ContextWorkSection.Comentaries)
                    {
                        item.WorkSectionId = ContextWorkSection.WorkSection.Id;
                        item.Save();
                    }
                    list.Add(new WorkSectionView(ContextWorkSection.WorkSection));
                    DBConnection.CommitTransaction();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    DBConnection.RollbackTransaction();
                }
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            using (var db = new SmetaDbAppContext())
            {
                if (data.SelectedItem == null)
                    return;
                ContextWorkSection ContextWorkSection = new ContextWorkSection();
                ContextWorkSection.WorkSection = list[data.SelectedIndex].WorkSection;
                var comentlist = db.Commentaries.Where(x => x.WorkSectionId == list[data.SelectedIndex].WorkSection.Id);
                if (comentlist.Count() != 0)
                    foreach (var item in comentlist)
                    {
                        ContextWorkSection.Comentaries.Add(item);
                    }
                ContextWorkSection.SetSelectedWorkType();
                Adds.WindowAddWorkSection window = new Adds.WindowAddWorkSection(ContextWorkSection);
                ContextWorkSection.WorkSection.WorkTypeId = ContextWorkSection.SelectedWorkType.Id;
                
                if (window.ShowDialog() == true)
                {
                    ContextWorkSection.WorkSection.Update();
                    foreach (var item in ContextWorkSection.Comentaries)
                    {
                        if (item.Id == 0)
                        {
                            item.WorkSectionId = ContextWorkSection.WorkSection.Id;
                            item.Save();
                        }
                        else
                        {
                            item.Update();
                        }
                    }
                    list[data.SelectedIndex].OnPropertyChanged("Book");
                    list[data.SelectedIndex].OnPropertyChanged("Place");
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem != null &&
                MessageBox.Show("Вы хотите удалить", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                {
                    list[data.SelectedIndex].WorkSection.Delete();
                    foreach (var item in db.Commentaries.Where(x => x.WorkSectionId == list[data.SelectedIndex].WorkSection.Id))
                    {
                        item.Delete();
                    }
                }
                list.RemoveAt(data.SelectedIndex);
            }
        }
    }
}
