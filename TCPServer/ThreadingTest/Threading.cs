using System.Threading;

//Work With Threading 
namespace ThreadingTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new(MyThread);
            Thread tr = new Thread(ts);
            tr.Start();
        }
        private void MyThread()
        {
            while (true)
            {
                listBox1.Items.Add("item"+listBox1.Items.Count.ToString());
                Thread.Sleep(1000);
            }
        }
        public void testinput(object inObj)
        {
            TextBox txt = (TextBox)inObj;
            txt.Text = "salam";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Thread tr = new Thread(testinput);
            tr.Start(textBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }
    }
}