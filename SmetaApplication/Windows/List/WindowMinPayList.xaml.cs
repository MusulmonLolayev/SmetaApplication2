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
using SmetaApplication.ViewModels;
using System.Collections.ObjectModel;
using SmetaApplication.DbContexts;
using System.Threading;
using System.ComponentModel;
using SmetaApplication.Models.Amount;
using SmetaApplication.Methods;
using SmetaApplication.Models.Team;

namespace SmetaApplication.Windows.List
{
    /// <summary>
    /// Логика взаимодействия для WindowMinPayList.xaml
    /// </summary>
    public partial class WindowMinPayList : Window
    {
        ObservableCollection<MinimumPay> list;
        BackgroundWorker worker;
        public event EventHandler InstallChange;

        public WindowMinPayList()
        {
            InitializeComponent();
            list = new ObservableCollection<MinimumPay>();
            data.ItemsSource = list;
            worker = new BackgroundWorker();
            worker.DoWork += DoWok;
            worker.ProgressChanged += ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += RunWorkerCompleted;

            FillData();
        }

        private void FillData()
        {
            using (var db = new SmetaDbAppContext())
            {
                db.MinimumPays.ToList().ForEach(x =>
                {
                    list.Add(x);
                });
            }
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (data.SelectedIndex > -1)
                {
                    foreach (var item in list)
                    {
                        if (item.Status == true)
                        {
                            item.Status = false;
                        }
                    }
                    list[data.SelectedIndex].Status = true;
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void OnClickNew(object sender, RoutedEventArgs e)
        {
            MinimumPay pay = new MinimumPay();
            Adds.WindowAddMinumumPay window = new Adds.WindowAddMinumumPay(pay);
            if (window.ShowDialog() == true)
            {
                pay.Status = false;
                list.Add(pay);
                pay.Save();
            }
        }

        private void OnEditing(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem == null)
                return;
            MinimumPay pay = list[data.SelectedIndex];
            Adds.WindowAddMinumumPay window = new Adds.WindowAddMinumumPay(pay);
            if (window.ShowDialog() == true)
            {
                pay.Update();
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            if (data.SelectedItem != null &&
                MessageBox.Show("Вы хотите удалить", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Warning) ==
                MessageBoxResult.Yes)
            {
                list[data.SelectedIndex].Delete();
                list.RemoveAt(data.SelectedIndex);
            }
        }

        private void Install(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;
            IsEnabled = false;
            using (var db = new SmetaDbAppContext())
            {
                ProgressBar.Maximum = db.Works.Count;
                //MessageBox.Show(ProgressBar.Maximum.ToString());
            }
            worker.RunWorkerAsync();
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Collapsed;
            IsEnabled = true;
            InstallChange?.Invoke(sender, e);
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ProgressBar.Value = Math.Round((double)e.ProgressPercentage / ProgressBar.Maximum * 100, 2);
            ProgressBar.Value = e.ProgressPercentage;
        }

        private void DoWok(object sender, DoWorkEventArgs e)
        {
            using (var db = new SmetaDbAppContext())
            {
                MinimumPay MinPay = list.Where(x => x.Status == true).FirstOrDefault();
                // Ishchilarning narxini o'zgartirish
                foreach (var item in db.Posts)
                {
                    item.Pay = Math.Round(MinPay.Pay * item.Koef, 2);
                    item.Update();
                }
                // Ishlarning narxlarini o'zgartirish
                List<Post> posts = db.Posts;
                List<WorkTeam> teams = db.WorkTeams;
                int i = 1;
                db.Works.ForEach(work =>
                {
                    double s = 0;
                    teams.Where(group => group.WorkDemId == work.Id).ToList().ForEach(x =>
                    {
                        Post post = posts.Where(q => q.Id == x.PostId).FirstOrDefault();
                        s += Math.Round(post.Pay / 168 * x.Koef * x.Count, 2);
                    });
                    work.PricePay = s;
                    //work.Update();
                    string query = "Update Works Set PricePay=" + Helper.ToString(s) + " Where Id=" + work.Id;
                    DBConnection.SqlQuery(query);
                    (sender as BackgroundWorker).ReportProgress(i, null);
                    i++;
                });
                // Minimum zarplatani o'zgartirish
                foreach (var item in list)
                {
                    item.Update();
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (worker.IsBusy)
            {
                MessageBox.Show("Ещё обновляется, пожалуйста подождите!");
                e.Cancel = true;
            }
        }
    }
}
