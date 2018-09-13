using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ChatRoomWindow.BusinessLayer;

namespace ChatRoomWindow.PresentationLayer
{
    class LoggedInWindowObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public LoggedInWindowObservableModel()
        {
            Messages.CollectionChanged += Messages_CollectionChanged;

        }
        #region Binding 


       public ObservableCollection<DataMessage> Messages { get; } = new ObservableCollection<DataMessage>();
           

            
 

        private void Messages_CollectionChanged(Object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Messages");
        }

        private bool isChecked_Nickname;
        public bool IsChecked_Nickname
        {
            get { return isChecked_Nickname; }
            set { isChecked_Nickname = value; OnPropertyChanged("IsChecked_Nickname"); }
        }

        private bool isChecked_Gid;
        public bool IsChecked_Gid
        {
            get { return isChecked_Gid; }
            set { isChecked_Gid = value; OnPropertyChanged("IsChecked_Gid"); }
        }

        private String tb_Nickname;
        public String Tb_Nickname
        {
            get { return tb_Nickname; }
            set { tb_Nickname = value; OnPropertyChanged("Tb_Nickname"); }
        }

        private String tb_Gid;
        public String Tb_Gid
        {
            get { return tb_Gid; }
            set { tb_Gid = value; OnPropertyChanged("Tb_Gid"); }
        }

        private String label_Head = "Current Sort: TimeStamp";
        public String Label_Head
        {
            get { return label_Head; }
            set { label_Head = value; OnPropertyChanged("Label_Head"); }
        }

        private bool isChecked_Reverse;
        public bool IsChecked_Reverse
        {
            get { return isChecked_Reverse; }
            set { isChecked_Reverse = value; OnPropertyChanged("IsChecked_Reverse"); }
        }

        private bool isChecked_TimeStamp = true;
        public bool IsChecked_TimeStamp
        {
            get { return isChecked_TimeStamp; }
            set { isChecked_TimeStamp = value; OnPropertyChanged("IsChecked_TimeStamp"); }
        }

        private bool isChecked_NicknameSort;
        public bool IsChecked_NicknameSort
        {
            get { return isChecked_NicknameSort; }
            set { isChecked_NicknameSort = value; OnPropertyChanged("IsChecked_NicknameSort"); }
        }

        private bool isChecked_TripleSort;
        public bool IsChecked_TripleSort
        {
            get { return isChecked_TripleSort; }
            set { isChecked_TripleSort = value; OnPropertyChanged("IsChecked_TripleSort"); }
        }



        private String tb_ToSendMessage = "Welcome.." ;
        public String Tb_ToSendMessage
        {
            get { return tb_ToSendMessage; }
            set { tb_ToSendMessage = value; OnPropertyChanged("Tb_ToSendMessage"); }
        }


        private List<DataMessage> msgList = new List<DataMessage>();
        public List<DataMessage> MsgList
        {
            get { return msgList; }
            set { msgList = value; OnPropertyChanged("MsgList"); }
        }

        private String SongName;
        public String songName
        {
            get { return SongName; }
            set { SongName = value; OnPropertyChanged("songName"); }
        }

        private String SongTime;
        public String songTime
        {
            get { return SongTime; }
            set { SongTime = value; OnPropertyChanged("songTime"); }
        }

        private bool isEnabeld_Nickname=true;
        public bool IsEnabeld_Nickname
        {
            get { return isEnabeld_Nickname; }
            set { isEnabeld_Nickname = value; OnPropertyChanged("IsEnabeld_Nickname"); }
        }

        private bool isEnabeld_Gid=true;
        public bool IsEnabeld_Gid
        {
            get { return isEnabeld_Gid; }
            set { isEnabeld_Gid = value; OnPropertyChanged("IsEnabeld_Gid"); }
        }
        #endregion
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}