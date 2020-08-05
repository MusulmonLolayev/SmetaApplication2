using SmetaApplication.Models.Amount;
using SmetaApplication.Models.Team;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmetaApplication.Context
{
    public class TeamContext : INotifyPropertyChanged
    {

        private WorkTeam workTeam;
        public WorkTeam WorkTeam
        {
            get { return workTeam; }
            set
            {
                workTeam = value;
                using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
                {
                    post = db.Posts.Where(x => x.Id == workTeam.PostId).FirstOrDefault();
                }
                OnPropertyChanged();
            }
        }

        private Post post;
        public Post Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
                OnPropertyChanged();
            }
        }

        private int count;
        [DisplayName("Количество человека")]
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
                paybyHour = Math.Round((double)(count * post.Pay * WorkTeam.Koef / 168), 2);
                if (workTeam != null)
                {
                    workTeam.Count = count;
                }
                OnPropertyChanged("Count");
                OnPropertyChanged("PaybyHour");
            }
        }

        private double? paybyHour;

        public double? PaybyHour
        {
            get
            {
                return paybyHour;
            }
            set
            {
                paybyHour = value;
                OnPropertyChanged("PaybyHour");
            }
        }

        public double? HourInMoon
        {
            get
            {
                return 168;
            }
            private set
            {

            }
        }

        public TeamContext()
        {

        }

        public TeamContext(WorkTeam workTeam)
        {
            this.workTeam = workTeam;

            this.count = workTeam.Count;
            using (var db = new SmetaApplication.DbContexts.SmetaDbAppContext())
            {
                Post = db.Posts.Where(x => x.Id == workTeam.PostId).FirstOrDefault();
            }
            PaybyHour = Post.Pay / 168;
        }

        #region Prperty change
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        #endregion
    }
}