using System.Net;
using System.Net.Sockets;

namespace ThreadingTest
{
    public partial class Socket2 : Form
    {
        public Socket2()
        {
            InitializeComponent();
        }

        private void Socket2_Load(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse("192.168.3.180");
            IPEndPoint ipcode = new IPEndPoint(IPAddress.Parse("192.1168.3.182"),5050);

            Socket soc = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        }
    }
}
