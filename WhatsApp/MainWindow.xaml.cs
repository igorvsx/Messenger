using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WhatsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewChatBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text))
            {
                MessageBox.Show("Введите имя пользователя!");
            }
            else
            {
                string name = UsernameBox.Text;
                AdminPanel window = new AdminPanel(name);
                window.AddItemToListBox(UsernameBox.Text);
                window.Show();
                Close();  
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrWhiteSpace(IPBox.Text))
            {
                MessageBox.Show("Введите имя пользователя и IP-адрес!");
            }
            else
            {
                if (Regex.IsMatch(IPBox.Text, @"\b(?:[0-9]{1,3}\.){3}[0-9]{1,3}\b"))
                {
                    string ip = IPBox.Text;
                    string name = UsernameBox.Text;
                    ClientWindow window = new ClientWindow(ip, name);
                    window.AddItemToListBox(UsernameBox.Text);
                    window.Closed += Window_Closed;
                    window.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("IP-адрес неверен.");
                }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Show(); 
        }
    }
}
