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
//Window for Edit Messages
namespace ChatRoomWindow.PresentationLayer
{
    /// <summary>
    /// Interaction logic for MyDialog.xaml
    /// </summary>
    public partial class MyDialog : Window
    {
        //fields
        MyDialogObserableModle _main = new MyDialogObserableModle();
        //Constructor:
        public MyDialog(string title, string input)
        {
            InitializeComponent();
            this.DataContext = _main;
            TitleText = title;
            InputText = input;
            Canceled = false;
        }

        public string TitleText
        {
            get { return TitleTextBox.Text; }
            set { TitleTextBox.Text = value; }
        }

        public string InputText
        {
            get { return _main.Tb_Input; }
            set
            {
            _main.Tb_Input = value;
            }
        }

        public bool Canceled { get; set; }


        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Canceled = false;
            Close();
        }
        public static Boolean CheckValidty(String txt)
        {
            return (txt.Length <= 100 && (txt.Trim().Count() != 0));
        }
        private void BtnOk_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (CheckValidty(InputTextBox.Text))
            {
                Canceled = true;
                Close();

            }
            else
            {
                MessageBox.Show("Invalid message");
            }

        }
    }
}
