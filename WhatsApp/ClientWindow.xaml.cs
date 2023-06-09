using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WhatsApp
{
    public partial class ClientWindow : Window
    {
        private Socket server;
        string username;
        private CancellationTokenSource cancellationTokenSource;
        public ClientWindow(string ip, string name)
        {
            InitializeComponent();
            username = name;
            Closing += ClientWindow_Closing;
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            cancellationTokenSource = new CancellationTokenSource();
            try
            {
                server.Connect(ip, 8888);
                ReceiveMessage(cancellationTokenSource.Token);
            }
            catch
            {
                MessageBox.Show("Подключение не удалось!");
                cancellationTokenSource.Cancel();
                Task.Delay(2000).ContinueWith(t =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        Close();
                    });
                });
            }
            SendMessage($"[{username}] ку");
        }
        public void AddItemToListBox(string item)
        {
            ClientLbx.Items.Add(item);
        }
        private void ClientWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.Close();
        }
        private void ProcessClientList(string clientListString)
        {
            string[] clientNicks = clientListString.Split(":::");

            ClientLbx.Items.Clear();

            foreach (string clientNick in clientNicks)
            {
                ClientLbx.Items.Add(clientNick);
            }
        }

        private async Task ReceiveMessage(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                byte[] bytes = new byte[1024];
                try
                {
                    await server.ReceiveAsync(bytes, SocketFlags.None, cancellationToken);
                }
                catch (SocketException)
                {
                    cancellationTokenSource.Cancel();
                    MessageBox.Show("Потеряно соединение с сервером.");
                    Task.Delay(2000).ContinueWith(t =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            var mainWindow = new MainWindow();
                            mainWindow.Show();
                            Close();
                        });
                    });
                    return;
                }
                //byte[] bytes = new byte[1024];
                //await server.ReceiveAsync(bytes, SocketFlags.None);
                string message = Encoding.UTF8.GetString(bytes);
                if (message.Contains(":::"))
                {
                    ProcessClientList(message);
                }
                int startIndex = message.IndexOf('[');
                int endIndex = message.IndexOf(']');
                if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                {
                    string user = message.Substring(startIndex + 1, endIndex - startIndex - 1);
                    string text = message.Substring(endIndex + 1).Trim();

                    //if (String.IsNullOrWhiteSpace(text))
                    //{
                    //    MessagesLbx.Items.Add($"[{DateTime.Now}] [{user}]: {text}");
                    //    if (!ClientLbx.Items.Contains(user))
                    //    {
                    //        ClientLbx.Items.Add(user);
                    //    }
                    //    int lastIndex = MessagesLbx.Items.Count - 1;
                    //    MessagesLbx.Items.RemoveAt(lastIndex);
                    //}
                    MessagesLbx.Items.Add($"[{DateTime.Now}] [{user}]: {text}");

                    //if (!ClientLbx.Items.Contains(user))
                    //{
                    //    ClientLbx.Items.Add(user);
                    //}
                }

            }
        }
        private void SendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTxt.Text.Contains("/disconnect"))
            {
                server.Close();
                Close();
            }
            SendMessage($"[{username}] " + MessageTxt.Text);
        }
        private async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(bytes, SocketFlags.None);
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            server.Close();
            Close();
        }
    }
}
