using System;
using System.Media;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Windows.Threading;
using ChatRoomWindow.BusinessLayer;

namespace ChatRoomWindow.PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields:
        Loading lw;
        BusinessLayer.ChatRoom c = new BusinessLayer.ChatRoom();
        MainWindowObservableModel _main = new MainWindowObservableModel();
        DispatcherTimer dispatcherTimer;
        private bool ValidPasswordFlag;

        //Constructor:
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _main;
            this.ValidPasswordFlag = false;
            NnTb.Focus();


        }


        //Manage the Button Pressing in the main Screen
        private void OpenScreenClick(object sender, RoutedEventArgs e)
        {

            if (sender.Equals(bRegister))
            {
                try
                {

                    Registration_window rw = new Registration_window(c);
                    this.Close();
                    rw.Show();

                }
                catch (Exception ex)
                {
                    Logger.guiLog.Error(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }

            if (sender.Equals(bLogin))
            {
                try
                {
                    if(!ValidPasswordFlag)
                        throw new Exception("Illegal Password!");
                    c.Login(_main.Tb_NickName, _main.Tb_Gid, _main.Tb_Password);
                    lw = new Loading(c);
                    Logger.guiLog.Info("info: User logged in...");
                    this.Close();
                    lw.Show();
                    dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Tick += DispatcherTimer_Tick; 
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 3);
                    dispatcherTimer.Start();

                }
                catch (Exception ex)
                {
                    Logger.guiLog.Error(ex.Message);
                    MessageBox.Show(ex.Message);
                  //  MessageBox.Show(new DateTime(2018,06,11,08,00,00).ToString());
                }
            }

            if (sender.Equals(bLogout))
            {
                this.Close();
            }

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            LoggedinWindow lwn = new LoggedinWindow(c);
            lw.Close();
            lwn.Show();
            dispatcherTimer.Stop();
        }


        //Manage the password Changes
        private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pd = sender as PasswordBox;
            String pass = pd.Password;
            if (ExceptionHandler.isValidPass(pass))
            {
                _main.Tb_Password = Hashing.GetHashString(pass + "1337");
                ValidPasswordFlag = true;
            }

            else
                ValidPasswordFlag = false;

        }
    }   
}