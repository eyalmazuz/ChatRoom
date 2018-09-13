using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoomWindow.PresentationLayer
{
    class MyDialogObserableModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MyDialogObserableModle() { }
        #region Binding 
        private String tb_input = "";

        public String Tb_Input
        {
            get
            {
                return tb_input;
            }
            set
            {
                tb_input = value;
                OnPropertyChanged("Tb_Input");
            }
        }
        #endregion
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

