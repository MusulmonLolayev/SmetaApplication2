using SmetaApplication.Models.Amount;
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
    /// Логика взаимодействия для WindowAddPost.xaml
    /// </summary>
    public partial class WindowAddPost : Window
    {
        public WindowAddPost(Post post)
        {
            InitializeComponent();
            DataContext = post;
        }

        private void Qaytarish_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Saqlash_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
