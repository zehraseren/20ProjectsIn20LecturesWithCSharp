using System;
using System.Windows.Forms;

namespace L01.AdoNetCustomer
{
    public partial class FrmMap : Form
    {
        public FrmMap()
        {
            InitializeComponent();
        }

        private void btnOpenCityForm_Click(object sender, EventArgs e)
        {
            FrmCity city = new FrmCity();
            city.Show();
        }

        private void btnOpenCustomerForm_Click(object sender, EventArgs e)
        {
            FrmCustomer customer = new FrmCustomer();
            customer.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
