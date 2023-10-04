using System.Net;
using System.Net.Sockets;


//Work With DNS and IP
namespace ThreadingTest
{
    public partial class Socket1 : Form
    {
        public Socket1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Dns.GetHostName();

        }
        //solve 1 for Get ip addresses
        private void button2_Click(object sender, EventArgs e)
        {
            IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
            foreach (IPAddress ip in ipHost.AddressList)
            {
                listBox1.Items.Add(ip.ToString());
            }
        }
        //solve 2 for Get ip addresses
        private void button3_Click(object sender, EventArgs e)
        {
            IPAddress [] ipArray = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ipArray)
            {
                if(ip.AddressFamily == AddressFamily.InterNetwork)
                listBox2.Items.Add(ip.ToString());
            }
        }
    }
}
