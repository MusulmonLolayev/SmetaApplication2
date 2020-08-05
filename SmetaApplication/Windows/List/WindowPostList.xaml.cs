using SmetaApplication.DbContexts;
using SmetaApplication.Models.Amount;
using SmetaApplication.ViewModels;
using SmetaApplication.Windows.Adds;
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
    /// Логика взаимодействия для WindowPostList.xaml
    /// </summary>
    public partial class WindowPostList : Window
    {
        private ObservableCollection<PostView> list;
        private double? MinPay;

        public WindowPostList()
        {
            InitializeComponent();

            list = new ObservableCollection<PostView>();
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                MinPay = db.MinimumPays.Where(x => x.Status == true).Single().Pay;
                foreach (var item in db.Posts)
                {
                    list.Add(new PostView(item));
                }
            }
            DGLavozimlist.ItemsSource = list;
        }

        private void btnnew_Click(object sender, RoutedEventArgs e)
        {
            Post post = new Post();
            WindowAddPost window = new WindowAddPost(post);
            if (window.ShowDialog() == true)
            {
                if (MinPay != null)
                    post.Pay = Math.Round((double)(post.Koef * MinPay), 2);
                list.Add(new PostView(post));
                post.Save();
            }
        }

        private void btnEditing_Click(object sender, RoutedEventArgs e)
        {
            if (DGLavozimlist.SelectedItem == null)
                return;
            WindowAddPost window = new WindowAddPost(list[DGLavozimlist.SelectedIndex].Post);
            if (window.ShowDialog() == true)
            {
                if (MinPay != null)
                    list[DGLavozimlist.SelectedIndex].Post.Pay = Math.Round((double)(list[DGLavozimlist.SelectedIndex].Post.Koef * MinPay), 2);
                list[DGLavozimlist.SelectedIndex].Post.Update();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DGLavozimlist.SelectedItem != null &&
                MessageBox.Show("Вы хотите удалить", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                list[DGLavozimlist.SelectedIndex].Post.Delete();
                list.RemoveAt(DGLavozimlist.SelectedIndex);
            }

        }

        private void btnMinZarplata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MinimumPay pay = new MinimumPay();
                WindowAddMinumumPay window = new WindowAddMinumumPay(pay);
                if (window.ShowDialog() == true)
                {
                    //MessageBox.Show(pay.Pay.ToString());
                    list.Clear();
                    using (var db = new SmetaDbAppContext())
                    {
                        db.Posts.ForEach(x =>
                        {
                            x.Pay = pay.Pay;
                            x.Update();
                            list.Add(new PostView(x));
                        });
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void InstallChange(object sender, EventArgs e)
        {
            list.Clear();
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                MinPay = db.MinimumPays.Where(x => x.Status == true).Single().Pay;
                foreach (var item in db.Posts)
                {
                    list.Add(new PostView(item));
                }
            }
        }
    }
}
