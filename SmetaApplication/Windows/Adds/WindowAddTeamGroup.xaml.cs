using SmetaApplication.Context;
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
    /// Логика взаимодействия для WindowAddTeamGroup.xaml
    /// </summary>
    public partial class WindowAddTeamGroup : Window
    {
        public ObservableCollection<TeamContext> list;
        public WindowAddTeamGroup(ObservableCollection<TeamContext> TeamContexts)
        {
            InitializeComponent();

            list = new ObservableCollection<TeamContext>();
            data.ItemsSource = list;
            using (var db = new DbContexts.SmetaDbAppContext())
            {
                int i = 0;
                foreach (var item in db.Posts)
                {
                    if (TeamContexts.Where(x => x.Post.Id == item.Id).Any())
                    {
                        list.Add(TeamContexts[i]);
                        i++;
                    }
                    else
                    {
                        list.Add(new TeamContext()
                        {
                            Post = item,
                            WorkTeam = new Models.Team.WorkTeam()
                            {
                                PostId = item.Id
                            }
                        });
                    }
                }
            }
        }

        private void OnClickCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnClickOk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
