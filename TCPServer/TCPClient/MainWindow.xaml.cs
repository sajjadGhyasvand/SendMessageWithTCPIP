using SuperSimpleTcp;
using System;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace TCPClient
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
        SimpleTcpClient client;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new(txtIP.Text);
            client.Events.Connected += Event_Connected;
            client.Events.Disconnected += Event_Disconnected;
            client.Events.DataReceived += Event_DataReceived;
            btnSend.IsEnabled=false;
        }


        private void Event_DataReceived(object? sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            this.textInfo.Dispatcher.Invoke(new Action(delegate
            {
                textInfo.Document.Blocks.Add(new Paragraph(new Run($"Server:{Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}")));
            }));
        }

        private void Event_Disconnected(object? sender, ConnectionEventArgs e)
        {
            this.textInfo.Dispatcher.Invoke(new Action(delegate
            {
                textInfo.Document.Blocks.Add(new Paragraph(new Run($"ServerDisconnected {Environment.NewLine}")));
            }));
        }

        private void Event_Connected(object? sender, ConnectionEventArgs e)
        {
            this.textInfo.Dispatcher.Invoke(new Action(delegate
            {
                textInfo.Document.Blocks.Add(new Paragraph(new Run($"Connected {Environment.NewLine}")));
            }));
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Connect();
                btnSend.IsEnabled=true;
                btnConnect.IsEnabled=false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMessage.Text))
                {
                    client.Send(txtMessage.Text);
                    textInfo.Document.Blocks.Add(new Paragraph(new Run($"Me: {txtMessage.Text} {Environment.NewLine}")));
                    txtMessage.Text =string.Empty;
                }
            }
        }
    }
}
