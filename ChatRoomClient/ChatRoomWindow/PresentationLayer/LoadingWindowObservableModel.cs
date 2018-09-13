using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChatRoomWindow.BusinessLayer;

namespace ChatRoomWindow.PresentationLayer
{
    class LoadingWindowObservableModel : INotifyPropertyChanged
    {
        ChatRoom chatRoom;
        public event PropertyChangedEventHandler PropertyChanged;
        public LoadingWindowObservableModel(ChatRoom chatRoom)
        {
            this.chatRoom = chatRoom;
        }
        #region Binding 

        public String Joke
        {
            get
            {
                return chatRoom.getJoke();
            }

        }

        #endregion Binding
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}