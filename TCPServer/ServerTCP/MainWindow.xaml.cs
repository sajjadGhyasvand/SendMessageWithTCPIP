using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerTCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            server = new SimpleTcpServer((long.Parse(textIP.Text)).ToString(), Convert.ToInt32(txtPort.Text));
            Thread.Sleep(2000);
            server.Start();
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            server.Events.DataReceived += Events_DataRecieved;
            textInfo.Document.Blocks.Add(new Paragraph(new Run($"Starting...{Environment.NewLine}")));
            BTNStart.IsEnabled = false;
            BTNSend.IsEnabled = true;
        }

        private void BTNSend_Click(object sender, RoutedEventArgs e)
        {
            if (server.IsListening)
            {
                if (!string.IsNullOrEmpty(txtMessage.Text) && lstClientIP.SelectedItem != null)
                {
                    server.Send(lstClientIP.SelectedItem.ToString(), txtMessage.Text);
                    textInfo.Document.Blocks.Add(new Paragraph(new Run($"Server:{txtMessage.Text}{Environment.NewLine}")));
                    txtMessage.Text =string.Empty;
                }

            }
        }




        private void Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {
            this.textInfo.Dispatcher.Invoke(new Action(delegate
            {
                textInfo.Document.Blocks.Add(new Paragraph(new Run($"{e.IpPort} disconnected.{Environment.NewLine}")));
                lstClientIP.Items.Remove(e.IpPort);
            }));
        }

        private void Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {
            this.textInfo.Dispatcher.Invoke(new Action(delegate
            {
                textInfo.Document.Blocks.Add(new Paragraph(new Run($"{e.IpPort} connected.{Environment.NewLine}")));
                lstClientIP.Items.Add(e.IpPort);
            }));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BTNSend.IsEnabled = false;
        }

        private void Events_DataRecieved(object? sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            this.textInfo.Dispatcher.Invoke(new Action(delegate
            {
                textInfo.Document.Blocks.Add(new Paragraph(new Run($"{e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}")));
            }));
        }

        private void BTNStop_Click(object sender, RoutedEventArgs e)
        {
            if (server.IsListening)
                server.Stop();
        }
    }
}
