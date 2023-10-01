using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderProj
{
    public partial class OrderAddAndEdit : Form
    {
        public OrderAddAndEdit()
        {
            InitializeComponent();
        }

        private void Refuse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            OrderDetailsAddAndEdit orderDetails = new OrderDetailsAddAndEdit();
            orderDetails.ShowDialog();
        }
    }
}
