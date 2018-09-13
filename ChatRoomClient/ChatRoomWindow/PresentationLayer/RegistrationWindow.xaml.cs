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
using ChatRoomWindow.BusinessLayer;

namespace ChatRoomWindow.PresentationLayer
{
    /// <summary>
    /// Interaction logic for Registration_window.xaml
    /// </summary>
    public partial class Registration_window : Window
    {
        //Fields:
        ChatRoom c;
        RegisterWindowObserableModle _main = new RegisterWindowObserableModle();
        private bool ValidPasswordFlag;
        //Constructor:
        public Registration_window(BusinessLayer.ChatRoom c)
        {
            this.c = c;
            this.DataContext = _main;
            this.ValidPasswordFlag = false;
            InitializeComponent();

        }

        //Manage Button Pressing in the register Screen
        private void OpenScreenClick(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(bRegister))
            {
                try
                {
                    if (ValidPasswordFlag)
                        c.Register(_main.Tb_NickName, _main.Tb_Gid, _main.Tb_Password);
                    else
                        throw new Exception("Illegal Password!");
                    MessageBox.Show("Register Success!");
                    Logger.guiLog.Info("info: Register Success...");
                    MainWindow mn = new MainWindow();
                    this.Close();
                    mn.Show();

                }
                catch (Exception ex)
                {
                    Logger.guiLog.Error(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }

        }

        // opens MainWindow , if the user click on the the main button.
        private void ReturnToMain(object sender, RoutedEventArgs e)
        {
            MainWindow mn = new MainWindow();
            this.Close();
            mn.Show();
        }

        //Manage the Password changes
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

