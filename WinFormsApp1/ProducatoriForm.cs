using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Data;

namespace WinFormsApp1
{
    public partial class ProducatoriForm : Form
    {
        SqlDataAdapter parentAdapter;
        SqlDataAdapter childAdapter;
        DataSet dataset = new DataSet();

        BindingSource parentBS = new BindingSource();
        BindingSource childBS = new BindingSource();
        private readonly IConfiguration _configuration;

        string connectionString;
        string selectParentQuery;
        string selectChildQuery;
        string deleteChildQuery;
        string nameParent;
        string nameChild;
        string childID;
        string parentID;
        string parentReference;


        public ProducatoriForm(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeComponent();

        }

        private void LoadDb()
        {
            connectionString = _configuration.GetConnectionString("DefaultConnection");
            selectParentQuery = _configuration["Queries:SelectParent"];
            selectChildQuery = _configuration["Queries:SelectChild"];
            deleteChildQuery = _configuration["Queries:DeleteChild"];
            nameParent = _configuration["TableNames:NameParent"];
            nameChild = _configuration["TableNames:NameChild"];
            childID = _configuration["ColumnNames:ChildID"];
            parentID = _configuration["ColumnNames:ParentID"];
            parentReference = _configuration["ColumnNames:ParentReference"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //MessageBox.Show("Status: " + connection.State);
                    connection.Open();
                    //MessageBox.Show("Status: " + connection.State);

                    parentAdapter = new SqlDataAdapter(selectParentQuery, connection);
                    childAdapter = new SqlDataAdapter(selectChildQuery, connection);

                    parentAdapter.Fill(dataset, nameParent);
                    childAdapter.Fill(dataset, nameChild);
                    DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                    PrimaryKeyColumns[0] = dataset.Tables[nameParent].Columns[parentID];
                    dataset.Tables[nameParent].Columns[0].AutoIncrement = true;

                    PrimaryKeyColumns[0] = dataset.Tables[nameChild].Columns[childID];
                    dataset.Tables[nameChild].Columns[0].AutoIncrement = true;


                    parentBS.DataSource = dataset.Tables[nameParent];
                    dataGridViewProducator.DataSource = parentBS;

                    DataColumn parentPK = dataset.Tables[nameParent].Columns[parentID];
                    DataColumn childFK = dataset.Tables[nameChild].Columns[parentReference];
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Initialize the SqlDataAdapter with a SELECT statement (this is required for SqlCommandBuilder to generate commands)
                    SqlDataAdapter childAdapter = new SqlDataAdapter(selectChildQuery, connection);

                    // Initialize SqlCommandBuilder with the childAdapter
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(childAdapter);


                    this.Validate();
                    childBS.EndEdit();
                    childAdapter.Update(dataset, nameChild);
                }

            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing insert command: " + ex.Message);
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

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlDataAdapter childAdapter = new SqlDataAdapter(selectChildQuery, connection);
                            SqlCommand deleteCommand = new SqlCommand(deleteChildQuery, connection);
                            deleteCommand.Parameters.Add("@" + childID, SqlDbType.Int, 4, childID);
                            childAdapter.DeleteCommand = deleteCommand;

                            // Initialize SqlCommandBuilder with the childAdapter
                            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(childAdapter);

                            childAdapter.Update(dataset, nameChild);

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

        private void Modifica_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Initialize the SqlDataAdapter with a SELECT statement (this is required for SqlCommandBuilder to generate commands)
                    SqlDataAdapter childAdapter = new SqlDataAdapter(selectChildQuery, connection);

                    // Initialize SqlCommandBuilder with the childAdapter
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(childAdapter);


                    this.Validate();
                    childBS.EndEdit();
                    childAdapter.Update(dataset, nameChild);
                }

            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing update command: " + ex.Message);
            }
        }
    }
}
