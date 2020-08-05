using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmetaApplication.Models.Amount;

namespace SmetaApplication.ViewModels
{
    public class PostView : INotifyPropertyChanged
    {

        public Post Post { get; set; }

        public PostView(Post post)
        {
            Post = post;
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
