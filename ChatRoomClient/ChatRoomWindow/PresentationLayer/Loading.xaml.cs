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
using System.Threading;
using ChatRoomWindow.BusinessLayer;

namespace ChatRoomWindow.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        ChatRoom c;
        public Loading(ChatRoom c)
        {
            this.c = c;
            InitializeComponent();
            this.DataContext = new LoadingWindowObservableModel(c);
            

        }
        public void next()
        {

       
        }
    }
}
