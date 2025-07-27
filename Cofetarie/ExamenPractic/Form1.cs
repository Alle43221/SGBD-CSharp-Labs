using System.Data;
using Microsoft.Data.SqlClient;

namespace ExamenPractic
{
    public partial class Form1 : Form
    {
        SqlDataAdapter daParent, daChild;
        DataSet ds = new DataSet();
        BindingSource bsParent = new BindingSource();
        BindingSource bsChild = new BindingSource();
        string connectionString = @"Server=DESKTOP-VJN0NT8\SQLEXPRESS;Database=Cofetarie_SGBD;Integrated Security=true; TrustServerCertificate=true;";

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            btnAddChild.Click += btnAddChild_Click;
            btnDeleteChild.Click += btnDeleteChild_Click;
            btnUpdateChild.Click += btnUpdateChild_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    daParent = new SqlDataAdapter("SELECT * FROM Cofetarii;", con);
                    daChild = new SqlDataAdapter("SELECT * FROM Briose;", con);

                    daParent.Fill(ds, "Cofetarii");
                    daChild.Fill(ds, "briose");

                    DataColumn pkColumn = ds.Tables["Cofetarii"].Columns["cod_cofetarie"];
                    DataColumn fkColumn = ds.Tables["Briose"].Columns["cod_cofetarie"];

                    DataRelation relation = new DataRelation("FK_Cofetarii_Briose", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Cofetarii"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Cofetarii_Briose";
                    dataGridViewChild.DataSource = bsChild;

                    dataGridViewChild.Columns["cod_briosa"].ReadOnly = true;
                    dataGridViewChild.Columns["cod_cofetarie"].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la incarcarea datelor: " + ex.Message);
            }
        }

        private void reincarcareDate(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    daParent.SelectCommand.Connection = con;
                    daChild.SelectCommand.Connection = con;

                    if (ds.Tables.Contains("Briose")) ds.Tables["Briose"].Clear();
                    if (ds.Tables.Contains("Cofetarii")) ds.Tables["Cofetarii"].Clear();

                    daParent.Fill(ds, "Cofetarii");
                    daChild.Fill(ds, "Briose");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la reincarcarea datelor: " + ex.Message);
            }
        }

        private void btnAddChild_Click(object sender, EventArgs e)
        {
            try
            {
                // Presupunem că ultimul rând completat de utilizator este înainte de NewRow
                DataGridViewRow row = dataGridViewChild.Rows[dataGridViewChild.NewRowIndex - 1];

                string numeBriosa = Convert.ToString(row.Cells["nume_briosa"].Value)?.Trim();
                string descriere = Convert.ToString(row.Cells["descriere"].Value)?.Trim();
                string pretText = Convert.ToString(row.Cells["pret"].Value);
                string codCofetarieText = Convert.ToString(row.Cells["cod_cofetarie"].Value);

                if (string.IsNullOrWhiteSpace(numeBriosa))
                {
                    MessageBox.Show("Numele brioșei nu poate fi gol.");
                    return;
                }

                if (!decimal.TryParse(pretText, out decimal pret) || pret < 0)
                {
                    MessageBox.Show("Prețul trebuie să fie un număr pozitiv.");
                    return;
                }

                if (!int.TryParse(codCofetarieText, out int codCofetarie))
                {
                    MessageBox.Show("Codul cofetăriei nu este valid.");
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Briose (nume_briosa, descriere, pret, cod_cofetarie) " +
                        "VALUES (@nume_briosa, @descriere, @pret, @cod_cofetarie)", con);

                    cmd.Parameters.AddWithValue("@nume_briosa", numeBriosa);
                    cmd.Parameters.AddWithValue("@descriere", string.IsNullOrWhiteSpace(descriere) ? (object)DBNull.Value : descriere);
                    cmd.Parameters.AddWithValue("@pret", pret);
                    cmd.Parameters.AddWithValue("@cod_cofetarie", codCofetarie);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Brioșa a fost adăugată cu succes!");
                    reincarcareDate(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea brioșei: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    DataRow selectedRow = ((DataRowView)bsChild.Current).Row;
                    if (selectedRow["cod_briosa"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codPremiu = Convert.ToInt32(selectedRow["cod_briosa"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Briose WHERE cod_briosa = @cod_briosa", con);
                        cmd.Parameters.AddWithValue("@cod_briosa", codPremiu);
                        cmd.ExecuteNonQuery();

                        reincarcareDate(sender, e);
                        MessageBox.Show("Briosa a fost ștearsa cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat nicio briosa pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea briosei: " + ex.Message);
            }
        }

        private void btnUpdateChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        DataRow selectedRow = ((DataRowView)bsChild.Current).Row;

                        if (selectedRow["cod_briosa"] == DBNull.Value)
                        {
                            MessageBox.Show("ID invalid.");
                            return;
                        }

                        daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Briose SET cod_cofetarie = @cod_cofetarie, nume_briosa = @nume_briosa, descriere = @descriere, pret = @pret " +
                            "WHERE cod_briosa = @cod_briosa", con);

                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_cofetarie", Convert.ToInt32(selectedRow["cod_cofetarie"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_briosa", Convert.ToInt32(selectedRow["cod_briosa"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@nume_briosa", selectedRow["nume_briosa"].ToString());
                        daChild.UpdateCommand.Parameters.AddWithValue("@descriere", selectedRow["descriere"]);
                        daChild.UpdateCommand.Parameters.AddWithValue("@pret", Convert.ToDouble(selectedRow["pret"]));

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Briosa a fost actualizata cu succes!");
                        }

                        reincarcareDate(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat nicio briosa pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea briosei: " + ex.Message);
            }
        }
    }
}

