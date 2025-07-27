using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace ExamenPractic
{
    public partial class Form1 : Form
    {
        SqlDataAdapter daParent, daChild;
        DataSet ds = new DataSet();
        BindingSource bsParent = new BindingSource();
        BindingSource bsChild = new BindingSource();
        string connectionString = @"Server=DESKTOP-VJN0NT8\SQLEXPRESS;Database=Premii_SGBD;Integrated Security=true; TrustServerCertificate=true;";

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

                    daParent = new SqlDataAdapter("SELECT * FROM Tipuri_premii;", con);
                    daChild = new SqlDataAdapter("SELECT * FROM Premii;", con);

                    daParent.Fill(ds, "Tipuri_premii");
                    daChild.Fill(ds, "Premii");

                    DataColumn pkColumn = ds.Tables["Tipuri_premii"].Columns["cod"];
                    DataColumn fkColumn = ds.Tables["Premii"].Columns["cod_tip"];

                    DataRelation relation = new DataRelation("FK_Tipuri_premii_Premii", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Tipuri_premii"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Tipuri_premii_Premii";
                    dataGridViewChild.DataSource = bsChild;

                    dataGridViewChild.Columns["cod"].ReadOnly = true;
                    dataGridViewChild.Columns["cod_tip"].ReadOnly = true;
                    dataGridViewParent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

                    if (ds.Tables.Contains("Premii")) ds.Tables["Premii"].Clear();
                    if (ds.Tables.Contains("Tipuri_premii")) ds.Tables["Tipuri_premii"].Clear();

                    daParent.Fill(ds, "Tipuri_premii");
                    daChild.Fill(ds, "Premii");
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

                string nume = Convert.ToString(row.Cells["nume"].Value)?.Trim();
                string sponsor = Convert.ToString(row.Cells["sponsor"].Value)?.Trim();
                int anText = Convert.ToInt32(row.Cells["an"].Value);
                int codTip = Convert.ToInt32(row.Cells["cod_tip"].Value);
                string nume_castigator = Convert.ToString(row.Cells["nume_castigator"].Value);
                int varsta = Convert.ToInt32(row.Cells["varsta"].Value);

                DataGridViewRow row1 = dataGridViewParent.SelectedRows[0];
                int numar_maxim = Convert.ToInt32(row1.Cells["numar_castigatori"].Value);

                if (string.IsNullOrWhiteSpace(nume))
                {
                    MessageBox.Show("Numele premiului nu poate fi gol.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(sponsor))
                {
                    MessageBox.Show("Numele sponsorului nu poate fi gol.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(nume_castigator))
                {
                    MessageBox.Show("Numele castigatorului nu poate fi gol.");
                    return;
                }

                if (varsta < 0)
                {
                    MessageBox.Show("Varsta nu poate fi negativa.");
                    return;
                }


                if (anText < 0)
                {
                    MessageBox.Show("Anul nu poate fi negativ.");
                    return;
                }

                // 2. Verificăm câți câștigători există deja în anul respectiv pentru acest tip

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand countCmd = new SqlCommand(@"
                    SELECT COUNT(*) 
                    FROM Premii 
                    WHERE cod_tip = @cod_tip AND an = @an", con);
                    countCmd.Parameters.AddWithValue("@cod_tip", codTip);
                    countCmd.Parameters.AddWithValue("@an", anText);

                    int existenti = (int)countCmd.ExecuteScalar();
                    if (existenti >= numar_maxim)
                    {
                        MessageBox.Show($"Nu se mai pot adăuga premii pentru acest tip în anul {anText}. Limita este {numar_maxim}.");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Premii (nume, sponsor, an, nume_castigator, varsta, cod_tip) " +
                        "VALUES (@nume, @sponsor, @an, @nume_castigator, @varsta, @cod_tip)", con);

                    cmd.Parameters.AddWithValue("@nume", nume);
                    cmd.Parameters.AddWithValue("@sponsor", sponsor);
                    cmd.Parameters.AddWithValue("@an", anText);
                    cmd.Parameters.AddWithValue("@cod_tip", codTip);
                    cmd.Parameters.AddWithValue("@varsta", varsta);
                    cmd.Parameters.AddWithValue("@nume_castigator", nume_castigator);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Premiul a fost adăugat cu succes!");
                    reincarcareDate(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea premiului: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    DataRow selectedRow = ((DataRowView)bsChild.Current).Row;
                    if (selectedRow["cod"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codPremiu = Convert.ToInt32(selectedRow["cod"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Premii WHERE cod = @cod", con);
                        cmd.Parameters.AddWithValue("@cod", codPremiu);
                        cmd.ExecuteNonQuery();

                        reincarcareDate(sender, e);
                        MessageBox.Show("Premiul a fost șters cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun premiu pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea premiului: " + ex.Message);
            }
        }

        private void btnUpdateChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {

                    DataRow row = ((DataRowView)bsChild.Current).Row;

                    DataGridViewRow row1 = dataGridViewParent.SelectedRows[0];
                    int numar_maxim = Convert.ToInt32(row1.Cells["numar_castigatori"].Value);

                    if (row["cod"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    string nume = Convert.ToString(row["nume"])?.Trim();
                    string sponsor = Convert.ToString(row["sponsor"])?.Trim();
                    int anText = Convert.ToInt32(row["an"]);
                    int codTip = Convert.ToInt32(row["cod_tip"]);
                    string nume_castigator = Convert.ToString(row["nume_castigator"]);
                    int varsta = Convert.ToInt32(row["varsta"]);


                    if (string.IsNullOrWhiteSpace(nume))
                    {
                        MessageBox.Show("Numele premiului nu poate fi gol.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(sponsor))
                    {
                        MessageBox.Show("Numele sponsorului nu poate fi gol.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(nume_castigator))
                    {
                        MessageBox.Show("Numele castigatorului nu poate fi gol.");
                        return;
                    }

                    if (varsta < 0)
                    {
                        MessageBox.Show("Varsta nu poate fi negativa.");
                        return;
                    }


                    if (anText < 0)
                    {
                        MessageBox.Show("Anul nu poate fi negativ.");
                        return;
                    }
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {

                        con.Open();
                        SqlCommand countCmd = new SqlCommand(@"
                            SELECT COUNT(*) 
                            FROM Premii 
                            WHERE cod_tip = @cod_tip AND an = @an", con);
                        countCmd.Parameters.AddWithValue("@cod_tip", codTip);
                        countCmd.Parameters.AddWithValue("@an", anText);

                        int existenti = (int)countCmd.ExecuteScalar();
                        if (existenti >= numar_maxim)
                        {
                            MessageBox.Show($"Nu se mai pot adăuga premii pentru acest tip în anul {anText}. Limita este {numar_maxim}.");
                            return;
                        }


                        daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Premii SET cod_tip = @cod_tip, nume = @nume, sponsor = @sponsor, an = @an, nume_castigator =  @nume_castigator, varsta=@varsta " +
                            "WHERE cod = @cod", con);

                        daChild.UpdateCommand.Parameters.AddWithValue("@cod_tip", Convert.ToInt32(row["cod_tip"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod", Convert.ToInt32(row["cod"]));
                        daChild.UpdateCommand.Parameters.AddWithValue("@nume", row["nume"].ToString());
                        daChild.UpdateCommand.Parameters.AddWithValue("@sponsor", row["sponsor"]);
                        daChild.UpdateCommand.Parameters.AddWithValue("@nume_castigator", row["nume_castigator"]);
                        daChild.UpdateCommand.Parameters.AddWithValue("@varsta", row["varsta"]);
                        daChild.UpdateCommand.Parameters.AddWithValue("@an", Convert.ToDouble(row["an"]));

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Premiul a fost actualizat cu succes!");
                        }

                        reincarcareDate(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun premiu pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea premiului: " + ex.Message);
            }
        }

        private void Briose_Click(object sender, EventArgs e)
        {

        }
    }
}

