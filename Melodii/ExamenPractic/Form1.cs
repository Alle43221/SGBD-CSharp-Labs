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
        string connectionString = @"Server=DESKTOP-VJN0NT8\SQLEXPRESS;Database=Melodii_SGBD;Integrated Security=true; TrustServerCertificate=true;";

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

                    daParent = new SqlDataAdapter("SELECT * FROM Artisti;", con);
                    daChild = new SqlDataAdapter("SELECT * FROM Melodii;", con);

                    daParent.Fill(ds, "Artisti");
                    daChild.Fill(ds, "Melodii");

                    DataColumn pkColumn = ds.Tables["Artisti"].Columns["cod_artist"];
                    DataColumn fkColumn = ds.Tables["Melodii"].Columns["cod_artist"];

                    DataRelation relation = new DataRelation("FK_Artisti_Melodii", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Artisti"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Artisti_Melodii";
                    dataGridViewChild.DataSource = bsChild;

                    dataGridViewChild.Columns["cod_melodie"].ReadOnly = true;
                    dataGridViewChild.Columns["cod_artist"].ReadOnly = true;
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

                    if (ds.Tables.Contains("Melodii")) ds.Tables["Melodii"].Clear();
                    if (ds.Tables.Contains("Artisti")) ds.Tables["Artisti"].Clear();

                    daParent.Fill(ds, "Artisti");
                    daChild.Fill(ds, "Melodii");
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

                string titlu = Convert.ToString(row.Cells["titlu"].Value)?.Trim();
                string codartistText = Convert.ToString(row.Cells["cod_artist"].Value);

                if (row.Cells["an_lansare"].Value == null || !int.TryParse(row.Cells["an_lansare"].Value.ToString(), out int anLansare))
                {
                    MessageBox.Show("Anul lansării nu este valid.");
                    return;
                }

                if (row.Cells["durata"].Value == null || !(row.Cells["durata"].Value is TimeSpan durata))
                {
                    MessageBox.Show("Durata nu este validă.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(titlu))
                {
                    MessageBox.Show("Titlul melodiei nu poate fi gol.");
                    return;
                }


                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Melodii (titlu, an_lansare, durata, cod_artist) " +
                        "VALUES (@titlu, @an_lansare, @durata, @cod_artist)", con);

                    cmd.Parameters.AddWithValue("@titlu", titlu);
                    cmd.Parameters.AddWithValue("@an_lansare", anLansare);
                    cmd.Parameters.AddWithValue("@durata", durata);
                    cmd.Parameters.AddWithValue("@cod_artist", codartistText);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Melodia a fost adăugată cu succes!");
                    reincarcareDate(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea melodiei: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    DataRow selectedRow = ((DataRowView)bsChild.Current).Row;
                    if (selectedRow["cod_melodie"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codPremiu = Convert.ToInt32(selectedRow["cod_melodie"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Melodii WHERE cod_melodie = @cod_melodie", con);
                        cmd.Parameters.AddWithValue("@cod_melodie", codPremiu);
                        cmd.ExecuteNonQuery();

                        reincarcareDate(sender, e);
                        MessageBox.Show("Melodie a fost ștearsa cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat nicio melodie pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea melodiei: " + ex.Message);
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

                        if (row["cod_melodie"] == DBNull.Value)
                        {
                            MessageBox.Show("ID invalid.");
                            return;
                        }

                        string titlu = Convert.ToString(row["titlu"])?.Trim();
                        string codartistText = Convert.ToString(row["cod_artist"]);

                        if (row["an_lansare"] == null || !int.TryParse(row["an_lansare"].ToString(), out int anLansare))
                        {
                            MessageBox.Show("Anul lansării nu este valid.");
                            return;
                        }

                        if (row["durata"] == null || !(row["durata"] is TimeSpan durata))
                        {
                            MessageBox.Show("Durata nu este validă.");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(titlu))
                        {
                            MessageBox.Show("Titlul melodiei nu poate fi gol.");
                            return;
                        }

                            daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Melodii SET cod_artist = @cod_artist, titlu = @titlu, durata = @durata, an_lansare = @an_lansare " +
                            "WHERE cod_melodie = @cod_melodie", con);

                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_artist", codartistText);
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_melodie", row["cod_melodie"]);
                        daChild.UpdateCommand.Parameters.AddWithValue("@titlu", titlu);
                        daChild.UpdateCommand.Parameters.AddWithValue("@durata", durata);
                        daChild.UpdateCommand.Parameters.AddWithValue("@an_lansare", anLansare);

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Melodia a fost actualizata cu succes!");
                        }

                        reincarcareDate(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat nicio melodie pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea melodiei: " + ex.Message);
            }
        }
    }
}

