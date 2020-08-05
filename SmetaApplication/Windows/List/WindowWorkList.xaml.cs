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
using SmetaApplication.Context;
using SmetaApplication.DbContexts;
using SmetaApplication.Filtrs;

namespace SmetaApplication.Windows.List
{
    /// <summary>
    /// Логика взаимодействия для WindowWorkList.xaml
    /// </summary>
    public partial class WindowWorkList : Window
    {
        private ObservableCollection<WorkListView> list;
        private List<WorkSection> WorkSections;
        private int Count = 100;

        public event EventHandler AddToCount;

        public WindowWorkList(WorkFilter filtr)
        {
            Init(filtr);
        }

        public WindowWorkList(WorkFilter filtr, bool t)
        {
            Init(filtr);
            AddToCounts.Visibility = Visibility.Visible;
        }

        private void Init(WorkFilter filtr)
        {
            InitializeComponent();
            list = new ObservableCollection<WorkListView>();
            WorkSections = new List<WorkSection>();
            data.ItemsSource = list;
            filtr.PropertyChanged += RefreshData;
            expFiltr.DataContext = filtr;
        }

        private void RefreshData(object sender, EventArgs e)
        {
            list.Clear();
            WorkFilter filtr = sender as WorkFilter;
            if (filtr.SelectedSection != null)
            {
                if (string.IsNullOrEmpty(filtr.SearchText))
                {

                    using (var db = new SmetaDbAppContext())
                    {
                        db.Works.Where(x => x.WorkSectionId == filtr.SelectedSection.Id)
                            .ToList().ForEach(x =>
                            {
                                list.Add(new WorkListView(x));
                            });
                    }
                }
                else
                {
                    //List<DistanceLevenshtein> distances = new List<DistanceLevenshtein>();

                    //using (var db = new SmetaDbAppContext())
                    //{
                    //    db.Works.Where(x => x.WorkSectionId == filtr.SelectedSection.Id)
                    //        .ToList().ForEach(x =>
                    //        {
                    //            int distance = DistanceLevenshtein.CalcLevenshteinDistance(x.Name, filtr.SearchText);
                    //            distances.Add(new DistanceLevenshtein()
                    //            {
                    //                Distance =distance,
                    //                Work = x
                    //            });
                    //        });
                    //}
                    //distances.OrderBy(x => x.Distance).ToList().ForEach(x =>
                    //{
                    //    list.Add(new WorkListView(x.Work));
                    //});

                    string lower = filtr.SearchText.ToLower();
                    using (var db = new SmetaDbAppContext())
                    {
                        db.Works.Where(x => x.WorkSectionId == filtr.SelectedSection.Id)
                            .ToList().ForEach(x =>
                            {
                                if (x.Name.ToLower().Contains(lower))
                                {
                                    list.Add(new WorkListView(x));
                                }
                            });
                    }
                }
            }
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            WorkContext WorkContext = new WorkContext();
            Adds.WindowAddWork window = new Adds.WindowAddWork(WorkContext);
            if (window.ShowDialog() == true)
            {
                using (var db = new SmetaDbAppContext())
                {
                    WorkContext.Work.PricePay = WorkContext.TeamContexts.Select(x => Math.Round((double)x.PaybyHour, 2)).Sum();
                    // Count Price of material
                    if (WorkContext.MaterailContexts.Count > 0)
                    {
                        WorkContext.Work.PriceMaterial = WorkContext.MaterailContexts.Select(x => 
                        x.Material.Price * x.MaterialGroup.Count1).Sum();
                    }
                    else
                    {
                        WorkContext.Work.PriceMaterial = 0;
                    }
                    // Count Price of Pribors
                    if (WorkContext.PriborContexts.Count > 0)
                    {
                        WorkContext.Work.PricePribor = WorkContext.PriborContexts.Select(x =>
                        Math.Round((x.Pribor.Price * x.Pribor.Percent) / 100, 2)).Sum() / 12;
                    }
                    else
                    {
                        WorkContext.Work.PricePribor = 0;
                    }

                    DBConnection.BeginTransaction();
                    try
                    {
                        // Save the work to base and get Id of the new work
                        WorkContext.Work.Save();

                        // Add team workers
                        foreach (var item in WorkContext.TeamContexts)
                        {
                            item.WorkTeam.WorkDemId = WorkContext.Work.Id;
                            item.WorkTeam.PostId = item.Post.Id;
                            item.WorkTeam.Save();
                        }
                        //Add materials
                        foreach (var item in WorkContext.MaterailContexts)
                        {
                            item.MaterialGroup.WorkId = WorkContext.Work.Id;
                            item.MaterialGroup.MaterialId = item.Material.Id;
                            item.MaterialGroup.Save();
                        }
                        // Add Pribors
                        foreach (var item in WorkContext.PriborContexts)
                        {
                            item.PriborGroup.WorkId = WorkContext.Work.Id;
                            item.PriborGroup.PriborId = item.Pribor.Id;
                            item.PriborGroup.Save();
                        }

                        DBConnection.CommitTransaction();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                        DBConnection.RollbackTransaction();
                    }
                }
                list.Add(new WorkListView(WorkContext.Work));
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedIndex < 0)
            {
                MessageBox.Show("Выберете процесс...");
                return;
            }
            WorkContext WorkContext = new WorkContext(list[data.SelectedIndex].Work);

            Adds.WindowAddWork window = new Adds.WindowAddWork(WorkContext);
            if (window.ShowDialog() == true)
            {
                using (var db = new SmetaDbAppContext())
                {

                    WorkContext.Work.PricePay = (double)WorkContext.TeamContexts.Select(x => x.PaybyHour * x.Count).Sum();

                    if (WorkContext.MaterailContexts.Count > 0)
                    {
                        WorkContext.Work.PriceMaterial = WorkContext.MaterailContexts.Select(x => x.Material.Price).Sum();
                    }
                    else
                    {
                        WorkContext.Work.PriceMaterial = 0;
                    }
                    if (WorkContext.PriborContexts.Count > 0)
                    {
                        WorkContext.Work.PricePribor = WorkContext.PriborContexts.Select(x =>
                        (x.Pribor.Price * x.Pribor.Percent) / 100).Sum() / 12;
                    }
                    else
                    {
                        WorkContext.Work.PricePribor = 0;
                    }

                    //DBConnection.BeginTransaction();
                    try
                    {
                        //MessageBox.Show(WorkContext.Work.Id.ToString());
                        // Add workr and get Id of the new work
                        WorkContext.Work.Update();

                        // Add team workers
                        foreach (var item in WorkContext.TeamContexts)
                        {
                            item.WorkTeam.WorkDemId = WorkContext.Work.Id;
                            item.WorkTeam.PostId = item.Post.Id;

                            if (item.WorkTeam.Id != 0)
                                item.WorkTeam.Update();
                            else
                                item.WorkTeam.Save();
                        }
                        //Add materials
                        foreach (var item in WorkContext.MaterailContexts)
                        {
                            item.MaterialGroup.WorkId = WorkContext.Work.Id;
                            item.MaterialGroup.MaterialId = item.Material.Id;

                            if (item.MaterialGroup.Id != 0)
                                item.MaterialGroup.Update();
                            else
                                item.MaterialGroup.Save();
                        }
                        // Add Pribors
                        foreach (var item in WorkContext.PriborContexts)
                        {
                            item.PriborGroup.WorkId = WorkContext.Work.Id;
                            item.PriborGroup.PriborId = item.Pribor.Id;

                            if (item.PriborGroup.Id != 0)
                                item.PriborGroup.Update();
                            else
                                item.PriborGroup.Save();
                        }

                        //DBConnection.CommitTransaction();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                        //DBConnection.RollbackTransaction();
                    }
                }
                //list.Add(new WorkListView(WorkContext.Work, WorkSections));
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem != null &&
                MessageBox.Show("Вы хотите удалить", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DBConnection.BeginTransaction();
                try
                {
                    // Deleta work
                    list[data.SelectedIndex].Work.Delete();
                    // Delete Group teams
                    DBConnection.SqlQuery("Delete From WorkTeams Where WorkDemId = " + list[data.SelectedIndex].Work.Id);
                    // Delete MaterialsGroups
                    DBConnection.SqlQuery("Delete From MaterialGroups Where WorkId = " + list[data.SelectedIndex].Work.Id);
                    // Delete PriborsGroups
                    DBConnection.SqlQuery("Delete From PriborGroups Where WorkId = " + list[data.SelectedIndex].Work.Id);

                    list.RemoveAt(data.SelectedIndex);
                    DBConnection.CommitTransaction();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                    DBConnection.RollbackTransaction();
                }
                
            }
        }

        private void OnAddToCounts(object sender, RoutedEventArgs e)
        {
            if (data.SelectedIndex > -1)
            {
                AddToCount?.Invoke(list[data.SelectedIndex].Work, null);
            }
        }
    }
}
