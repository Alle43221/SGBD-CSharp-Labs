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
        string connectionString = @"Server=DESKTOP-VJN0NT8\SQLEXPRESS;Database=Biscuiti_SGBD;Integrated Security=true; TrustServerCertificate=true;";

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

                    daParent = new SqlDataAdapter("SELECT * FROM Producatori;", con);
                    daChild = new SqlDataAdapter("SELECT * FROM Biscuiti;", con);

                    daParent.Fill(ds, "Producatori");
                    daChild.Fill(ds, "Biscuiti");

                    DataColumn pkColumn = ds.Tables["Producatori"].Columns["cod_p"];
                    DataColumn fkColumn = ds.Tables["Biscuiti"].Columns["cod_p"];

                    DataRelation relation = new DataRelation("FK_Producatori_Biscuiti", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Producatori"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Producatori_Biscuiti";
                    dataGridViewChild.DataSource = bsChild;

                    dataGridViewChild.Columns["cod_b"].ReadOnly = true;
                    dataGridViewChild.Columns["cod_p"].ReadOnly = true;
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

                    if (ds.Tables.Contains("Biscuiti")) ds.Tables["Biscuiti"].Clear();
                    if (ds.Tables.Contains("Producatori")) ds.Tables["Producatori"].Clear();

                    daParent.Fill(ds, "Producatori");
                    daChild.Fill(ds, "Biscuiti");
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

                string numeb = Convert.ToString(row.Cells["nume_b"].Value)?.Trim();
                string calText = Convert.ToString(row.Cells["nr_calorii"].Value);
                string pretText = Convert.ToString(row.Cells["pret"].Value);
                string codpText = Convert.ToString(row.Cells["cod_p"].Value);

                if (string.IsNullOrWhiteSpace(numeb))
                {
                    MessageBox.Show("Numele biscuitului nu poate fi gol.");
                    return;
                }

                if (!decimal.TryParse(pretText, out decimal pret) || pret < 0)
                {
                    MessageBox.Show("Prețul trebuie să fie un număr pozitiv.");
                    return;
                }

                if (!int.TryParse(calText, out int cals) || cals <= 0)
                {
                    MessageBox.Show("Numarul de calorii trebuie să fie un număr pozitiv.");
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Biscuiti (nume_b, nr_calorii, pret, cod_p) " +
                        "VALUES (@nume_b, @nr_calorii, @pret, @cod_p)", con);

                    cmd.Parameters.AddWithValue("@nume_b", numeb);
                    cmd.Parameters.AddWithValue("@nr_calorii", cals);
                    cmd.Parameters.AddWithValue("@pret", pret);
                    cmd.Parameters.AddWithValue("@cod_p", codpText);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Biscuitele a fost adăugat cu succes!");
                    reincarcareDate(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea biscuitelui: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    DataRow selectedRow = ((DataRowView)bsChild.Current).Row;
                    if (selectedRow["cod_b"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codPremiu = Convert.ToInt32(selectedRow["cod_b"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Biscuiti WHERE cod_b = @cod_b", con);
                        cmd.Parameters.AddWithValue("@cod_b", codPremiu);
                        cmd.ExecuteNonQuery();

                        reincarcareDate(sender, e);
                        MessageBox.Show("Biscuitele a fost șters cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun biscuite pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea biscuitelui: " + ex.Message);
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
                        DataRow row = ((DataRowView)bsChild.Current).Row;

                        if (row["cod_b"] == DBNull.Value)
                        {
                            MessageBox.Show("ID invalid.");
                            return;
                        }

                        string numeb = Convert.ToString(row["nume_b"])?.Trim();
                        string calText = Convert.ToString(row["nr_calorii"]);
                        string pretText = Convert.ToString(row["pret"]);
                        string codpText = Convert.ToString(row["cod_p"]);

                        if (string.IsNullOrWhiteSpace(numeb))
                        {
                            MessageBox.Show("Numele biscuitului nu poate fi gol.");
                            return;
                        }

                        if (!decimal.TryParse(pretText, out decimal pret) || pret < 0)
                        {
                            MessageBox.Show("Prețul trebuie să fie un număr pozitiv.");
                            return;
                        }

                        if (!int.TryParse(calText, out int cals) || cals <= 0)
                        {
                            MessageBox.Show("Numarul de calorii trebuie să fie un număr pozitiv.");
                            return;
                        }

                        daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Biscuiti SET cod_p = @cod_p, nume_b = @nume_b, nr_calorii = @nr_calorii, pret = @pret " +
                            "WHERE cod_b = @cod_b", con);

                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_p", Convert.ToInt32(row["cod_p"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_b", Convert.ToInt32(row["cod_b"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@nume_b", row["nume_b"].ToString());
                        daChild.UpdateCommand.Parameters.AddWithValue("@nr_calorii", Convert.ToInt32(row["nr_calorii"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@pret", Convert.ToDouble(row["pret"]));

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Biscuitele a fost actualizat cu succes!");
                        }

                        reincarcareDate(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun biscuite pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea biscuitelui: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click_1(object sender, EventArgs e)
        {

        }
    }
}

