using Microsoft.Data.SqlClient;
using System.Data;

namespace WinFormsApp1
{
    public partial class ProducatoriForm : Form
    {

        string connectionString = "Server=DESKTOP-O9EER6A\\SQLEXPRESS;Database=3DPrinting;Integrated Security=true;TrustServerCertificate=true;";
        public ProducatoriForm()
        {
            InitializeComponent();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conexiunea la baza de date a esuat!" + ex.Message);
            }
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

                    DataSet dataset = new DataSet();
                    SqlDataAdapter parentAdapter = new SqlDataAdapter("select * from producatori", connection);
                    SqlDataAdapter childAdapter = new SqlDataAdapter("SELECT * FROM materiale ", connection);


                    parentAdapter.Fill(dataset, "Producatori");
                    childAdapter.Fill(dataset, "Materiale");

                    BindingSource parentBS = new BindingSource();
                    BindingSource childBS = new BindingSource();

                    parentBS.DataSource = dataset.Tables["Producatori"];
                    dataGridViewProducator.DataSource = parentBS;

                    DataColumn parentPK = dataset.Tables["Producatori"].Columns["id"];
                    DataColumn childFK = dataset.Tables["Materiale"].Columns["producator"];
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void adaugaMaterial_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define your insert command
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Materiale (denumire, culoare, tip, pret_pe_gram, producator) " +
                        "VALUES (@denumire1, @mail1, @website1, @telefon1, @adresa1);", connection);



                    if (DenumireAddMaterial.Text == "" || CuloareAddMaterial.Text == "" || TipAddMaterial.Text == "" || PretAddMaterial.Text == "")
                    {
                        MessageBox.Show("Toate campurile sunt obligatorii!");
                        return;
                    }

                    DataGridViewRow selectedRow = null;

                    if (dataGridViewProducator.SelectedCells.Count != 0)
                    {
                        DataGridViewCell selectedCell = dataGridViewProducator.SelectedCells[0];
                        selectedCell.DataGridView.Rows[selectedCell.RowIndex].Selected = true;
                        selectedRow = dataGridViewProducator.SelectedRows[0];
                    }
                    else if (dataGridViewProducator.SelectedRows.Count != 0)
                    {
                        selectedRow = dataGridViewProducator.SelectedRows[0];

                    }

                    if (selectedRow == null)
                    {
                        MessageBox.Show("Niciun producator selectat!");
                    }
                    else
                    {
                        int producerId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                        string pret = PretAddMaterial.Text.Replace(',', '.');
                        var isNumeric = float.TryParse(pret, out _);
                        if (!isNumeric)
                        {
                            MessageBox.Show("Pretul trebuie sa fie un numar!");
                            return;

                        }

                        // Add parameters
                        insertCommand.Parameters.AddWithValue("@denumire1", string.IsNullOrEmpty(DenumireAddMaterial.Text) ? DBNull.Value : (object)DenumireAddMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@mail1", string.IsNullOrEmpty(CuloareAddMaterial.Text) ? DBNull.Value : (object)CuloareAddMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@website1", string.IsNullOrEmpty(TipAddMaterial.Text) ? DBNull.Value : (object)TipAddMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@telefon1", pret);
                        insertCommand.Parameters.AddWithValue("@adresa1", producerId);

                        // Execute the command
                        int insertRowCount = insertCommand.ExecuteNonQuery();
                        MessageBox.Show("Numarul de inregistrari adaugate: " + insertRowCount);

                        DenumireAddMaterial.Text = "";
                        CuloareAddMaterial.Text = "";
                        TipAddMaterial.Text = "";
                        PretAddMaterial.Text = "";

                        LoadDb();

                    }

                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL specific exceptions (e.g., syntax errors, constraint violations)
                MessageBox.Show("SQL Error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invOpEx)
            {
                // Handle invalid operation exceptions (e.g., issues with connection states)
                MessageBox.Show("Invalid Operation: " + invOpEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing insert command: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand deleteCommand = new SqlCommand("DELETE FROM Materiale WHERE id = @id1", connection);

                    DataGridViewRow selectedRow = null;

                    if (dataGridView1.SelectedCells.Count != 0)
                    {
                        DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];
                        selectedCell.DataGridView.Rows[selectedCell.RowIndex].Selected = true;
                        selectedRow = dataGridView1.SelectedRows[0];
                    }
                    else if (dataGridView1.SelectedRows.Count != 0)
                    {
                        selectedRow = dataGridView1.SelectedRows[0];

                    }

                    if (selectedRow == null)
                    {
                        MessageBox.Show("Niciun Material selectat!");
                    }
                    else
                    {

                        // Add parameters
                        deleteCommand.Parameters.AddWithValue("@id1", Convert.ToInt32(selectedRow.Cells["id"].Value));

                        // Execute the command
                        int insertRowCount = deleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Numarul de inregistrari sterse: " + insertRowCount);

                        DenumireAddMaterial.Text = "";
                        CuloareAddMaterial.Text = "";
                        TipAddMaterial.Text = "";
                        PretAddMaterial.Text = "";

                        LoadDb();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL specific exceptions (e.g., syntax errors, constraint violations)
                MessageBox.Show("SQL Error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invOpEx)
            {
                // Handle invalid operation exceptions (e.g., issues with connection states)
                MessageBox.Show("Invalid Operation: " + invOpEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing insert command: " + ex.Message);
            }
        }

        private void modificaMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Define your insert command
                    SqlCommand insertCommand = new SqlCommand("UPDATE Materiale " +
                        "SET denumire=@denumire1, culoare=@mail1, tip=@website1, pret_pe_gram=@telefon1, producator=@adresa1 where id=@id1", connection);


                    if (ProdModMaterial.Text == "" || DenumireModMaterial.Text == "" || CuloareModMaterial.Text == "" || TipModMaterial.Text == "" || PretModMaterial.Text == "")
                    {
                        MessageBox.Show("Toate campurile sunt obligatorii!");
                        return;
                    }

                    DataGridViewRow selectedRow = null;

                    if (dataGridViewProducator.SelectedCells.Count != 0)
                    {
                        DataGridViewCell selectedCell = dataGridViewProducator.SelectedCells[0];
                        selectedCell.DataGridView.Rows[selectedCell.RowIndex].Selected = true;
                        selectedRow = dataGridViewProducator.SelectedRows[0];
                    }
                    else if (dataGridViewProducator.SelectedRows.Count != 0)
                    {
                        selectedRow = dataGridViewProducator.SelectedRows[0];

                    }

                    DataGridViewRow selectedRowMaterial = null;

                    if (dataGridView1.SelectedCells.Count != 0)
                    {
                        DataGridViewCell selectedCell = dataGridView1.SelectedCells[0];
                        selectedCell.DataGridView.Rows[selectedCell.RowIndex].Selected = true;
                        selectedRowMaterial = dataGridView1.SelectedRows[0];
                    }
                    else if (dataGridView1.SelectedRows.Count != 0)
                    {
                        selectedRowMaterial = dataGridView1.SelectedRows[0];

                    }

                    if (selectedRow == null)
                    {
                        MessageBox.Show("Niciun producator selectat!");
                    }
                    else if (selectedRowMaterial == null)
                    {
                        MessageBox.Show("Niciun material selectat!");
                    }
                    else
                    {
                        int materialId = Convert.ToInt32(selectedRowMaterial.Cells["id"].Value);

                        string pret = PretModMaterial.Text.Replace(',', '.');
                        var isNumeric = float.TryParse(pret, out _);
                        if (!isNumeric)
                        {
                            MessageBox.Show("Pretul trebuie sa fie un numar!");
                            return;

                        }


                        string producatorid = ProdModMaterial.Text;

                        bool isValidId = false;
                        foreach (DataGridViewRow row in dataGridViewProducator.Rows)
                        {
                            if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == producatorid)
                            {
                                isValidId = true;
                                break;
                            }
                        }

                        if (!isValidId)
                        {
                            MessageBox.Show("ID-ul producatorului este invalid!");
                            return;

                        }

                        // Add parameters
                        insertCommand.Parameters.AddWithValue("@denumire1", string.IsNullOrEmpty(DenumireModMaterial.Text) ? DBNull.Value : (object)DenumireModMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@mail1", string.IsNullOrEmpty(CuloareModMaterial.Text) ? DBNull.Value : (object)CuloareModMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@website1", string.IsNullOrEmpty(TipModMaterial.Text) ? DBNull.Value : (object)TipModMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@telefon1", pret);
                        insertCommand.Parameters.AddWithValue("@adresa1", string.IsNullOrEmpty(ProdModMaterial.Text) ? DBNull.Value : (object)ProdModMaterial.Text);
                        insertCommand.Parameters.AddWithValue("@id1", materialId);

                        // Execute the command
                        int insertRowCount = insertCommand.ExecuteNonQuery();
                        MessageBox.Show("Numarul de inregistrari modificate: " + insertRowCount);

                        DenumireModMaterial.Text = "";
                        CuloareModMaterial.Text = "";
                        TipModMaterial.Text = "";
                        PretModMaterial.Text = "";
                        ProdModMaterial.Text = "";

                        LoadDb();

                    }

                }
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL specific exceptions (e.g., syntax errors, constraint violations)
                MessageBox.Show("SQL Error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invOpEx)
            {
                // Handle invalid operation exceptions (e.g., issues with connection states)
                MessageBox.Show("Invalid Operation: " + invOpEx.Message);
            }
            catch (Exception ex)
            {
                // Handle other general exceptions
                MessageBox.Show("Error executing insert command: " + ex.Message);
            }
        }
    }
}
