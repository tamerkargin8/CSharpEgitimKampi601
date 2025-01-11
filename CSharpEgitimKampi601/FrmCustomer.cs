using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpEgitimKampi601
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost; port=5432;Database=CustomerDb;userId=postgres;Password=1234";

        void GetAllCustomers()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * from Customers";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable datatable = new DataTable();
            adapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            connection.Close();
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            GetAllCustomers();
        }

        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text;
            string customerSurname = txtCustomerSurname.Text;
            string customerCity = txtCustomerCity.Text;
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into Customers (CustomerName, CustomerSurname, CustomerCity) values (@p1, @p2, @p3)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", customerName);
            command.Parameters.AddWithValue("@p2", customerSurname);
            command.Parameters.AddWithValue("@p3", customerCity);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Müşteri ekleme işlemi gerçekleştirildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetAllCustomers();
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Delete from Customers where CustomerId = @p1";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Müşteri silme işlemi gerçekleştirildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetAllCustomers();
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtCustomerId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();  
            string query = "Update Customers set CustomerName = @p1, CustomerSurname = @p2, CustomerCity = @p3 where CustomerId = @p4";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", txtCustomerName.Text);
            command.Parameters.AddWithValue("@p2", txtCustomerSurname.Text);
            command.Parameters.AddWithValue("@p3", txtCustomerCity.Text);
            command.Parameters.AddWithValue("@p4", id);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Müşteri güncelleme işlemi gerçekleştirildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            GetAllCustomers();
        }
    }
}
