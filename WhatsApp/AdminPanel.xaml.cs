using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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

namespace WhatsApp
{
    public partial class AdminPanel : Window
    {
        private Dictionary<EndPoint, string> connectedCli = new Dictionary<EndPoint, string>();
        private List<string> connectedClients = new List<string>();
        public List<string> clientss = new List<string>();
        private Socket server;
        private Socket socket;
        private List<Socket> clients = new List<Socket>();
        string username;
        string clientListString;
        public AdminPanel(string name)
        {
            InitializeComponent();
            username = name;
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(1000);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect("127.0.0.1", 8888);
            connectedClients.Add(username);
            LogLbx.Items.Add($"[{DateTime.Now}] Клиент [{username}] был подключен.");
            //ReceiverMessage();
            ListenToClients();
        }
        
        public void AddItemToListBox(string item)
        {
            ClientLbx.Items.Add(item);
        }
        private async Task ListenToClients()
        {
            while (true)
            {
                var client = await socket.AcceptAsync();
                clients.Add(client);
                //ClientLbx.Items.Add(client);
                ReceiveMessage(client); //Надо запускать с отладкой, сообщение не успевает дойти.
                //Task.Delay(100);
                //LogLbx.Items.Add($"[{DateTime.Now}] Кто-то под ником <{username}> был подключен");
                //ReceiveMessage(client);
            }
        }
        string user;
        private async Task ReceiveMessage(Socket client)
        {
            while (true)
            {
                try
                {
                    var buffer = new byte[1024];
                    var bytesReceived = await client.ReceiveAsync(buffer, SocketFlags.None);
                    var message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                    int startIndex = message.IndexOf('[');
                    int endIndex = message.IndexOf(']');
                    if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
                    {
                        user = message.Substring(startIndex + 1, endIndex - startIndex - 1);
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

                        if (!ClientLbx.Items.Contains(user) && !connectedClients.Contains(user))
                        {
                            EndPoint newClientIP = client.RemoteEndPoint;
                            connectedCli[newClientIP] = user;
                            ClientLbx.Items.Add(user);
                            connectedClients.Add(user);
                            LogLbx.Items.Add($"[{DateTime.Now}] Клиент [{user}] был подключен.");
                        }
                    }
                    clientListString = string.Join(":::", connectedClients);
                    foreach (var item in clients)
                    {
                        SendMessage(item, clientListString);
                        SendMessage(item, message);
                    }
                }
                catch (SocketException)
                {
                    clients.Remove(client);
                    EndPoint disconnectedClientEndPoint = client.RemoteEndPoint;
                    user = connectedCli[disconnectedClientEndPoint];
                    string disconnectedClientNick = connectedCli[disconnectedClientEndPoint];
                    //MessagesLbx.Items.Add($"Клиент {client.RemoteEndPoint} [{disconnectedClientNick}] был отключен.");
                    ClientLbx.Items.Remove(disconnectedClientNick);
                    connectedClients.Remove(disconnectedClientNick);
                    LogLbx.Items.Add($"[{DateTime.Now}] Клиент [{disconnectedClientNick}] был отключен.");
                    client.Close();
                    clientListString = string.Join(":::", connectedClients);
                    foreach (var item in clients)
                    {
                        SendMessage(item, clientListString);
                    }
                    break;
                }
                catch (Exception ex)
                {
                    MessagesLbx.Items.Add($"Ошибка при обработке сообщения от {client.RemoteEndPoint}: {ex.Message}");
                    break;
                }
            }
        }
        private async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(bytes, SocketFlags.None);
        }
        private async Task SenderMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(bytes, SocketFlags.None);
        }
        private void SendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            SenderMessage($"[{username}] " + MessageTxt.Text);
        }
    }
}
