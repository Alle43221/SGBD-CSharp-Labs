using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class MaterialeForm : Form
    {
        public MaterialeForm()
        {
            InitializeComponent();


            string connectionString = "Server=DESKTOP-O9EER6A\\SQLEXPRESS;Database=3DPrinting;Integrated Security=true;TrustedServerCertificate=true;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    MessageBox.Show("Status: " + connection.State);
                    connection.Open();
                    MessageBox.Show("Status: " + connection.State);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat!" + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void MaterialeForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
