using CSharpEgitimKampi601.Entities;
using CSharpEgitimKampi601.Services;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CustomerOperations customerOperations = new CustomerOperations(new MongoDbConnection());
        private void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            var customer = new Customer()
            {
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = Convert.ToDecimal(txtCustomerBalance.Text),
                CustomerShoppingCount = Convert.ToInt32(txtCustomerShoppingCount.Text)
            };
            customerOperations.AddCustomer(customer);
            MessageBox.Show("Müşteri ekleme işlemi gerçekleştirildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCustomerList_Click(object sender, EventArgs e)
        {
            List<Customer> customer = customerOperations.GetAllCustomer();
            dataGridView1.DataSource = customer;
        }

        private void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            customerOperations.DeleteCustomer(customerId);
            MessageBox.Show("Müşteri silme işlemi gerçekleştirildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCustomerUpdate_Click(object sender, EventArgs e)
        {
            string customerId = txtCustomerId.Text;
            var updatedCustomer = new Customer()
            {
                CustomerId = customerId,
                CustomerName = txtCustomerName.Text,
                CustomerSurname = txtCustomerSurname.Text,
                CustomerCity = txtCustomerCity.Text,
                CustomerBalance = Convert.ToDecimal(txtCustomerBalance.Text),
                CustomerShoppingCount = Convert.ToInt32(txtCustomerShoppingCount.Text)
             };
            customerOperations.UpdateCustomer(updatedCustomer);
            MessageBox.Show("Müşteri güncelleme işlemi gerçekleştirildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text;
            Customer customer = customerOperations.GetCustomerByID(id);
            dataGridView1.DataSource = new List <Customer> { customer };
        }
    }
}
