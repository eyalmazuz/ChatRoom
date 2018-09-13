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
using System.Windows.Threading;
using System.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using ChatRoomWindow.BusinessLayer;
using ChatRoomWindow.BusinessLayer.LinqActions;
using System.Collections.ObjectModel;

namespace ChatRoomWindow.PresentationLayer
{
    /// <summary>
    /// Interaction logic for LoggedinWindow.xaml
    /// </summary>
    public partial class LoggedinWindow : Window
    {
        //Fields:
        MediaPlayer mediaPlayer = new MediaPlayer();
        DispatcherTimer dispatcherTimer;
        int insertMessage = 0;
        BusinessLayer.ChatRoom c;
        LoggedInWindowObservableModel _main = new LoggedInWindowObservableModel();
        List<ILinqAction> actionList = new List<ILinqAction>();
        String currentSort;
       //Constructor:
        public LoggedinWindow(BusinessLayer.ChatRoom c)
        {
            InitializeComponent();
            this.DataContext = _main;
            this.c = c;
            display();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(showMessages);
            
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);

            intialSortFilterProps();

            dispatcherTimer.Start();
            this.Loaded += LoggedinWindow_Loaded;
            

        }

        private void LoggedinWindow_Loaded(object sender, RoutedEventArgs e)
        {
            display();
            scrollToEnd();
        }

        //Uptades the LinqAction List according to the Chosen Filter/Sort
        private void intialSortFilterProps()
        {
            ILinqAction la = null;
            actionList.Clear();
            c.clearFilters();
            if (_main.IsChecked_NicknameSort)
            {
                la = new NicknameSort(_main.IsChecked_Reverse);
                actionList.Add(la);
            }
            else if (_main.IsChecked_TimeStamp)
            {
                la = new TimeSort(_main.IsChecked_Reverse);
                actionList.Add(la);
            }
            else if (_main.IsChecked_TripleSort)
            {
                la = new TripleSort(_main.IsChecked_Reverse);
                actionList.Add(la);
            }
            if (_main.IsChecked_Nickname)
            {
                c.AddNicknameFilter(_main.Tb_Nickname);
            }
            if (_main.IsChecked_Gid)
            {
                c.AddGroupFilter(int.Parse(_main.Tb_Gid));
            }
            currentSort = la?.name;

        }

        //Updates The MessageList
        private void showMessages(object sender, EventArgs e)
        {
            try
            {
                c.RetriveTenlastMessages();
                display();
            }
            catch (Exception ex)
            {
                Logger.guiLog.Error(ex.Message);
                MessageBox.Show(ex.Message);

            }
        }

        private void MessageBox1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (insertMessage == 0)
            { _main.Tb_ToSendMessage = ""; insertMessage = 1; }

        }

        //Happens when Pressing Go! in the Filter/Sort toolbar
        private void ApplyClick(object sender, RoutedEventArgs e)
        {

            try
            {
                Logger.guiLog.Info("info: user apply sorter...");
                intialSortFilterProps();
                showMessages(sender,e);
            }
            catch (Exception ex)
            {
                Logger.guiLog.Error(ex.Message);
                MessageBox.Show(ex.Message);
                _main.Tb_Nickname = "";
                _main.Tb_Gid = "";
            }

        }

        //Happens when Pressing send Button
        private void sendMessage()
        {
            if (insertMessage == 1 && _main.Tb_ToSendMessage.Count() != 0)
            {
                try
                {
                    if (!c.SendMessage(_main.Tb_ToSendMessage))
                    {
                        _main.Tb_ToSendMessage = "";
                        MessageBox.Show("Error! Massage Invalid!");

                    }
                    else
                    {


                        SoundPlayer sound = new SoundPlayer(ChatRoomWindow.Properties.Resources.Pikachu);
                        sound.Play();
                        Logger.guiLog.Info("info: message sent...");

                        _main.Tb_ToSendMessage = "";
                        display();
                    }
                }
                catch (Exception ex)
                {
                    Logger.guiLog.Error(ex.Message);
                    MessageBox.Show(ex.Message);
                    _main.Tb_ToSendMessage = "";
                    Logger.guiLog.Error(ex.Message);

                }
                
            }

            scrollToEnd();
        }

        //Updates The MessageList
        private void display()
        {

            List<DataMessage> MsgList = c.FilterSortList(actionList);
            _main.Messages.Clear();
            addAll(MsgList);
            _main.Label_Head = "Current Sort: ";
            _main.Label_Head += currentSort;


        }

        //Move all Messages to ObservableCollection Object
        private void addAll(List<DataMessage> msgList)
        {
           foreach(DataMessage m in msgList)
            {
                _main.Messages.Add(m);
            }
        }

        //Reset The Filters TextFields
        private void resetToDefault()
        {
            _main.IsChecked_Gid = false;
            _main.IsEnabeld_Gid= true;
            _main.Tb_Gid = "";

            _main.IsChecked_Nickname = false;
            _main.IsEnabeld_Nickname = true;
            _main.Tb_Nickname = "";

        }

        //Manage the press Enter To send Message
        private void pressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                sendMessage();
            }
        }

        //Edit Message
        private void EditMessage(object senders, RoutedEventArgs ef)
        {
            try
            {
                Button b = (Button)senders;
                DataMessage m = (DataMessage)b.DataContext;
                var dialog = new MyDialog("Enter Edited Message", m.messageBody);
                dialog.Show();
                dialog.Closing += (sender, e) =>
                {
                    var d = sender as MyDialog;
                    if (d.Canceled)
                        c.editMessage(m, d.InputText);
                };
                showMessages(null, null);
                Logger.guiLog.Info("Edit Massage!");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                Logger.guiLog.Error(e.Message);
            }
        }

        //Manage The CheckBox in the Filters toolbar
        private void cbChecked(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Nicknamecb))
            {
                try
                {
                    ExceptionHandler.NicknameExceptionHandeling(_main.Tb_Nickname);
                    Logger.guiLog.Info("info: user checked nickname checkbox...");
                    _main.IsEnabeld_Nickname = false;
                }
                catch (Exception ex)
                {
                    Logger.guiLog.Error(ex.Message);
                    _main.IsChecked_Nickname = false;
                    MessageBox.Show(ex.Message);
                }


            }
            if (sender.Equals(G_idCb))
            {
                try
                {
                    Logger.guiLog.Info("info: user checked g_id checkbox...");
                    ExceptionHandler.G_idExceptionHandeling(_main.Tb_Gid);
                    _main.IsEnabeld_Gid = false;
                }
                catch (Exception ex)
                {
                    Logger.guiLog.Error(ex.Message);
                    _main.IsChecked_Gid = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Cb_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(Nicknamecb))
            {
                _main.IsEnabeld_Nickname = true;
                _main.Tb_Nickname = "";
            }
            if (sender.Equals(G_idCb))
            {
                _main.IsEnabeld_Gid = true;
                _main.Tb_Gid = "";
            }
        }


        private void scrollToEnd()
        {
            if (messagesList.Items.Count == 0)
                return;
            messagesList.ScrollIntoView(messagesList.Items[messagesList.Items.Count-1]);
        }


        private void SendButton_Click(object sender, MouseButtonEventArgs e)
        {
            sendMessage();


        }

        //LogOut
        private void Logout(object sender, MouseButtonEventArgs e)
        {
            c.Logout();
            Logger.guiLog.Info("Info: User Logged out...");
            MainWindow mn = new MainWindow();
            dispatcherTimer.Stop();
            this.Close();
            mn.Show();
        }

        //Function Related to Music Player
        private void Open_Music(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                mediaPlayer.Play();
                String filename = openFileDialog.FileName;
                filename = filename.Substring(filename.LastIndexOf('\\') + 1);
                _main.songName = filename;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();
            }

        }

        private void Music_Play(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void Music_Pause(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void Music_Stop(object sender, MouseButtonEventArgs e)
        {
            mediaPlayer.Stop();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
                _main.songTime = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            else
                _main.songTime = "No file selected...";
        }

        private void Button_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Button b = (Button)sender;
            b.Visibility = Visibility.Visible;
        }
    }
}


