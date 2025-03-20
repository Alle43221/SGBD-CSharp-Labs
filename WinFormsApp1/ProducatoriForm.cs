using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;

namespace WinFormsApp1
{
    public partial class ProducatoriForm : Form
    {
        Config Config = new Config();
        SqlDataAdapter parentAdapter;
        SqlDataAdapter childAdapter;
        DataSet dataset = new DataSet();

        BindingSource parentBS = new BindingSource();
        BindingSource childBS = new BindingSource();

        string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
        string selectParent = ConfigurationManager.AppSettings.Get("selectParent");
        string selectChild = ConfigurationManager.AppSettings.Get("selectChild");
        string nameParent = ConfigurationManager.AppSettings.Get("nameParent");
        string nameChild = ConfigurationManager.AppSettings.Get("nameChild");
        string parentID = ConfigurationManager.AppSettings.Get("parentID");
        string parentReference = ConfigurationManager.AppSettings.Get("parentReference");
        string childID = ConfigurationManager.AppSettings.Get("childID");
        string deleteChild = ConfigurationManager.AppSettings.Get("deleteChild");



        public ProducatoriForm()
        {
            InitializeComponent();
        }

        private void LoadDb()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //MessageBox.Show("Status: " + connection.State);
                    connection.Open();
                    //MessageBox.Show("Status: " + connection.State);

                    parentAdapter = new SqlDataAdapter(Config.selectParent, connection);
                    childAdapter = new SqlDataAdapter(Config.selectChild, connection);

                    parentAdapter.Fill(dataset, Config.nameParent);
                    childAdapter.Fill(dataset, Config.nameChild);
                    DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                    PrimaryKeyColumns[0] = dataset.Tables[Config.nameParent].Columns[Config.parentID];
                    dataset.Tables[Config.nameParent].Columns[0].AutoIncrement = true;

                    PrimaryKeyColumns[0] = dataset.Tables[Config.nameChild].Columns[Config.childID];
                    dataset.Tables[Config.nameChild].Columns[0].AutoIncrement = true;


                    parentBS.DataSource = dataset.Tables[Config.nameParent];
                    dataGridViewProducator.DataSource = parentBS;

                    DataColumn parentPK = dataset.Tables[Config.nameParent].Columns[Config.parentID];
                    DataColumn childFK = dataset.Tables[Config.nameChild].Columns[Config.parentReference];
                    DataRelation relation = new DataRelation("fk_parent_child", parentPK, childFK);
                    dataset.Relations.Add(relation);

                    childBS.DataSource = parentBS;
                    childBS.DataMember = "fk_parent_child";
                    dataGridView1.DataSource = childBS;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat!" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadDb();
        }


        private void Add_Click_1(object sender, EventArgs e)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.connectionString))
                {
                    connection.Open();
                    // Initialize the SqlDataAdapter with a SELECT statement (this is required for SqlCommandBuilder to generate commands)
                    SqlDataAdapter childAdapter = new SqlDataAdapter(Config.selectChild, connection);

                    // Initialize SqlCommandBuilder with the childAdapter
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(childAdapter);


                    this.Validate();
                    childBS.EndEdit();
                    childAdapter.Update(dataset, Config.nameChild);
                }

            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing insert command: " + ex.Message);
            }
        }

        private void Modifica_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Config.connectionString))
                {
                    connection.Open();
                    // Initialize the SqlDataAdapter with a SELECT statement (this is required for SqlCommandBuilder to generate commands)
                    SqlDataAdapter childAdapter = new SqlDataAdapter(Config.selectChild, connection);

                    // Initialize SqlCommandBuilder with the childAdapter
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(childAdapter);


                    this.Validate();
                    childBS.EndEdit();
                    childAdapter.Update(dataset, Config.nameChild);
                }

            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing update command: " + ex.Message);
            }
        }

        private void Delete_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                // Ensure the user wants to delete the record
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    // Get the selected row from DataGridView
                    DataRowView selectedRow = (DataRowView)childBS.Current; // Get the current row being edited
                    if (selectedRow != null)
                    {
                        
                        // Delete the selected row from the DataSet
                        selectedRow.Row.Delete();  // Mark the row for deletion

                        using (SqlConnection connection = new SqlConnection(Config.connectionString))
                        {
                            connection.Open();
                            SqlDataAdapter childAdapter = new SqlDataAdapter(Config.selectChild, connection);
                            SqlCommand deleteCommand = new SqlCommand(Config.deleteChild, connection);
                            deleteCommand.Parameters.Add("@" + Config.childID, SqlDbType.Int, 4, Config.childID);
                            childAdapter.DeleteCommand = deleteCommand;

                            // Initialize SqlCommandBuilder with the childAdapter
                            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(childAdapter);

                            childAdapter.Update(dataset, Config.nameChild);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the delete
                MessageBox.Show("Error executing delete command: " + ex.Message);
            }
        }

    }
}
