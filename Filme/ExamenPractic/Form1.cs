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
        string connectionString = @"Server=DESKTOP-VJN0NT8\SQLEXPRESS;Database=Filme_SGBD;Integrated Security=true; TrustServerCertificate=true;";

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

                    daParent = new SqlDataAdapter("SELECT * FROM Gen;", con);
                    daChild = new SqlDataAdapter("SELECT cod, titlu, regizor, AnLansare, rating, durata, cod_gen FROM Filme, Filme_Genuri " +
                        "where cod_film = cod" +
                        " ;", con);

                    daParent.Fill(ds, "Gen");
                    daChild.Fill(ds, "Filme");

                    DataColumn pkColumn = ds.Tables["Gen"].Columns["codGen"];
                    DataColumn fkColumn = ds.Tables["Filme"].Columns["cod_gen"];

                    DataRelation relation = new DataRelation("FK_Gen_Filme", pkColumn, fkColumn);
                    ds.Relations.Add(relation);

                    bsParent.DataSource = ds.Tables["Gen"];
                    dataGridViewParent.DataSource = bsParent;

                    bsChild.DataSource = bsParent;
                    bsChild.DataMember = "FK_Gen_Filme";
                    dataGridViewChild.DataSource = bsChild;

                    dataGridViewChild.Columns["cod"].ReadOnly = true;
                    dataGridViewChild.Columns["cod_gen"].ReadOnly = true;
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

                    if (ds.Tables.Contains("Filme")) ds.Tables["Filme"].Clear();
                    if (ds.Tables.Contains("Gen")) ds.Tables["Gen"].Clear();

                    daParent.Fill(ds, "Gen");
                    daChild.Fill(ds, "Filme");
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
                string regizor = Convert.ToString(row.Cells["regizor"].Value)?.Trim();
                int anText = Convert.ToInt32(row.Cells["anLansare"].Value);
                int cod_gen = Convert.ToInt32(row.Cells["cod_gen"].Value);
                double rating = Convert.ToDouble(row.Cells["rating"].Value);

                if(!(row.Cells["durata"].Value is TimeSpan durata))
                {
                    MessageBox.Show("Durata trebuie să fie un interval de timp valid (ex: 01:30:00 pentru 1 oră și 30 minute).");
                    return;
                }

                if (string.IsNullOrWhiteSpace(titlu))
                {
                    MessageBox.Show("Titlul filmului nu poate fi gol.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(regizor))
                {
                    MessageBox.Show("Numele regizorului nu poate fi gol.");
                    return;
                }

                if (rating < 0)
                {
                    MessageBox.Show("Ratingul nu poate fi negativ.");
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

                    SqlCommand cmd = new SqlCommand(
                        "insert into Filme(titlu, regizor, AnLansare, rating, durata) " +
                        "VALUES (@titlu, @regizor, @an, @rating, @durata)", con);

                    cmd.Parameters.AddWithValue("@titlu", titlu);
                    cmd.Parameters.AddWithValue("@regizor", regizor);
                    cmd.Parameters.AddWithValue("@an", anText);
                    cmd.Parameters.AddWithValue("@durata", durata);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.ExecuteNonQuery();


                    SqlCommand countCmd = new SqlCommand(@"
                    SELECT cod
                    FROM Filme
                    order by cod desc;", con);

                    int existenti = (int)countCmd.ExecuteScalar();


                    SqlCommand cmd1 = new SqlCommand(
                        "insert into Filme_Genuri(cod_gen, cod_film) " +
                        "VALUES (@cod_gen, @cod_film)", con);

                    cmd1.Parameters.AddWithValue("@cod_gen", cod_gen);
                    cmd1.Parameters.AddWithValue("@cod_film", existenti);

                    cmd1.ExecuteNonQuery();

                    MessageBox.Show("Filmul a fost adăugat cu succes!");
                    reincarcareDate(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea filmului: " + ex.Message);
            }
        }

        private void btnDeleteChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {
                    DataRow selectedRow = ((DataRowView)bsChild.Current).Row;
                    DataRow parentRow = ((DataRowView)bsParent.Current).Row;
                    if (selectedRow["cod"] == DBNull.Value)
                    {
                        MessageBox.Show("ID invalid.");
                        return;
                    }

                    int codCopil = Convert.ToInt32(selectedRow["cod"]);
                    int codParinte = Convert.ToInt32(parentRow["codGen"]);

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand("DELETE FROM Filme_Genuri WHERE cod_film = @cod and cod_gen=@cod_gen", con);
                        cmd.Parameters.AddWithValue("@cod", codCopil);
                        cmd.Parameters.AddWithValue("@cod_gen", codParinte);
                        cmd.ExecuteNonQuery();

                        reincarcareDate(sender, e);
                        MessageBox.Show("Filmul a fost șters cu succes!");
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun film pentru ștergere.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la ștergerea filmului: " + ex.Message);
            }
        }

        private void btnUpdateChild_Click(object sender, EventArgs e)
        {
            try
            {
                if (bsChild.Current != null)
                {

                    DataRow row = ((DataRowView)bsChild.Current).Row;

                    string titlu = Convert.ToString(row["titlu"])?.Trim();
                    string regizor = Convert.ToString(row["regizor"])?.Trim();
                    int anText = Convert.ToInt32(row["anLansare"]);
                    int cod_gen = Convert.ToInt32(row["cod_gen"]);
                    double rating = Convert.ToDouble(row["rating"]);
                    int cod = Convert.ToInt32(row["cod"]);

                    if (!(row["durata"] is TimeSpan durata))
                    {
                        MessageBox.Show("Durata trebuie să fie un interval de timp valid (ex: 01:30:00 pentru 1 oră și 30 minute).");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(titlu))
                    {
                        MessageBox.Show("Titlul filmului nu poate fi gol.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(regizor))
                    {
                        MessageBox.Show("Numele regizorului nu poate fi gol.");
                        return;
                    }

                    if (rating < 0)
                    {
                        MessageBox.Show("Ratingul nu poate fi negativ.");
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

                        daChild.UpdateCommand = new SqlCommand(
                            "UPDATE Filme SET titlu = @titlu, regizor = @regizor, anLansare = @an, rating=@rating, durata=@durata " +
                            "WHERE cod = @cod", con)
                        {
                            //titlu, regizor, AnLansare, rating, durata
                        };
                        daChild.UpdateCommand.Parameters.AddWithValue("@titlu", titlu);
                        daChild.UpdateCommand.Parameters.AddWithValue("@regizor", regizor);
                        daChild.UpdateCommand.Parameters.AddWithValue("@an", anText);
                        daChild.UpdateCommand.Parameters.AddWithValue("@durata", durata);
                        daChild.UpdateCommand.Parameters.AddWithValue("@rating", rating);
                        daChild.UpdateCommand.Parameters.AddWithValue("@cod", cod);

                        int rowsAffected = daChild.UpdateCommand.ExecuteNonQuery();
                        if (rowsAffected >= 1)
                        {
                            MessageBox.Show("Titlul a fost actualizat cu succes!");
                        }

                        reincarcareDate(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Nu ați selectat niciun titlu pentru actualizare.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea titlului: " + ex.Message);
            }
        }

        private void Briose_Click(object sender, EventArgs e)
        {

        }
    }
}

