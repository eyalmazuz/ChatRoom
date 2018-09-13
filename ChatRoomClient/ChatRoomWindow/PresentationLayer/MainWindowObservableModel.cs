using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.PresentationLayer
{
    class MainWindowObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindowObservableModel()
        {

        }
        #region Binding 
        private String tb_nickname = "";
    
        public String Tb_NickName
        {
            get
            {
                return tb_nickname;
            }
            set
            {
                tb_nickname = value;
                OnPropertyChanged("Tb_NickName");
            }
        }

        private String tb_gid = "";

        public String Tb_Gid
        {
            get
            {
                return tb_gid;
            }
            set
            {
                tb_gid = value;
                OnPropertyChanged("Tb_Gid");
            }
        }

        private String tb_password = "";

        public String Tb_Password
        {
            get
            {
                return tb_password;
            }
            set
            {
                tb_password = value;
                OnPropertyChanged("Tb_Password");
            }
        }

        #endregion
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    
    }
}

