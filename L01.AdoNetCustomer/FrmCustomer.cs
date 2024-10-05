using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace L01.AdoNetCustomer
{
    public partial class FrmCustomer : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Server=Zehra;initial catalog=L01.AdoNetCustomer;integrated security=true");
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select CustomerId, CustomerName, CustomerSurname, CustomerBalance, CustomerStatus, CityName From Customer Inner Join City On City.CityId = Customer.CustomerCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Execute CustomerListWithCity", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From City", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbCity.ValueMember = "CityId";
            cmbCity.DisplayMember = "CityName";
            cmbCity.DataSource = dt;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Insert into Customer (CustomerName, CustomerSurname, CustomerCity, CustomerBalance, CustomerStatus) values (@customerName, @customerSurname, @customerCity, @customerBalance, @customerStatus)", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", cmbCity.Text);
            command.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            if (rdbActive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri başarıyla eklendi.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Delete From Customer Where CustomerId = @customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri başarılı bir şekilde silindi.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Update Customer Set CustomerName=@customerName, CustomerSurname=@customerSurname, CustomerCity=@customerCity, CustomerBalance=@customerBalance, CustomerStatus=@customerStatus Where CustomerId=@customerId", sqlConnection);
            command.Parameters.AddWithValue("@customerId", txtCustomerId.Text);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            command.Parameters.AddWithValue("@customerSurname", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@customerCity", cmbCity.SelectedValue);
            command.Parameters.AddWithValue("@customerBalance", txtCustomerBalance.Text);
            if (rdbActive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", true);
            }
            if (rdbPassive.Checked)
            {
                command.Parameters.AddWithValue("@customerStatus", false);
            }
            command.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("Müşteri başarılı bir şekilde güncellendi.", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("Select CustomerId, CustomerName, CustomerSurname, CustomerBalance, CustomerStatus, CityName From Customer Inner Join City On City.CityId = Customer.CustomerCity Where CustomerName=@customerName", sqlConnection);
            command.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
        }
    }
}
