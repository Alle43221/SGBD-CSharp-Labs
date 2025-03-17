namespace WinFormsApp1
{
    partial class ProducatoriForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dataGridViewProducator = new DataGridView();
            dataGridView1 = new DataGridView();
            adaugaMaterial = new Button();
            PretAddMaterial = new TextBox();
            TipAddMaterial = new TextBox();
            CuloareAddMaterial = new TextBox();
            DenumireAddMaterial = new TextBox();
            modificaMaterial = new Button();
            ProdModMaterial = new TextBox();
            PretModMaterial = new TextBox();
            TipModMaterial = new TextBox();
            CuloareModMaterial = new TextBox();
            DenumireModMaterial = new TextBox();
            StergeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewProducator
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewProducator.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewProducator.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewProducator.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewProducator.Location = new Point(12, 12);
            dataGridViewProducator.Name = "dataGridViewProducator";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridViewProducator.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewProducator.RowHeadersWidth = 51;
            dataGridViewProducator.Size = new Size(592, 413);
            dataGridViewProducator.TabIndex = 13;
           
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            dataGridView1.Location = new Point(620, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(577, 413);
            dataGridView1.TabIndex = 14;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            // 
            // adaugaMaterial
            // 
            adaugaMaterial.Location = new Point(1376, 43);
            adaugaMaterial.Name = "adaugaMaterial";
            adaugaMaterial.Size = new Size(94, 29);
            adaugaMaterial.TabIndex = 20;
            adaugaMaterial.Text = "Adauga";
            adaugaMaterial.UseVisualStyleBackColor = true;
            adaugaMaterial.Click += adaugaMaterial_Click_1;
            // 
            // PretAddMaterial
            // 
            PretAddMaterial.Location = new Point(1205, 111);
            PretAddMaterial.Name = "PretAddMaterial";
            PretAddMaterial.PlaceholderText = "Pret pe gram*";
            PretAddMaterial.Size = new Size(125, 27);
            PretAddMaterial.TabIndex = 18;
            // 
            // TipAddMaterial
            // 
            TipAddMaterial.Location = new Point(1205, 78);
            TipAddMaterial.Name = "TipAddMaterial";
            TipAddMaterial.PlaceholderText = "Tip*";
            TipAddMaterial.Size = new Size(125, 27);
            TipAddMaterial.TabIndex = 17;
            // 
            // CuloareAddMaterial
            // 
            CuloareAddMaterial.Location = new Point(1205, 45);
            CuloareAddMaterial.Name = "CuloareAddMaterial";
            CuloareAddMaterial.PlaceholderText = "Culoare*";
            CuloareAddMaterial.Size = new Size(125, 27);
            CuloareAddMaterial.TabIndex = 16;
            // 
            // DenumireAddMaterial
            // 
            DenumireAddMaterial.Location = new Point(1205, 12);
            DenumireAddMaterial.Name = "DenumireAddMaterial";
            DenumireAddMaterial.PlaceholderText = "Denumire*";
            DenumireAddMaterial.Size = new Size(265, 27);
            DenumireAddMaterial.TabIndex = 15;
            // 
            // modificaMaterial
            // 
            modificaMaterial.Location = new Point(1376, 270);
            modificaMaterial.Name = "modificaMaterial";
            modificaMaterial.Size = new Size(94, 29);
            modificaMaterial.TabIndex = 26;
            modificaMaterial.Text = "Modifica";
            modificaMaterial.UseVisualStyleBackColor = true;
            modificaMaterial.Click += modificaMaterial_Click;
            // 
            // ProdModMaterial
            // 
            ProdModMaterial.Location = new Point(1205, 371);
            ProdModMaterial.Name = "ProdModMaterial";
            ProdModMaterial.PlaceholderText = "Producator*";
            ProdModMaterial.Size = new Size(125, 27);
            ProdModMaterial.TabIndex = 25;
            ProdModMaterial.TextChanged += textBox1_TextChanged;
            // 
            // PretModMaterial
            // 
            PretModMaterial.Location = new Point(1205, 338);
            PretModMaterial.Name = "PretModMaterial";
            PretModMaterial.PlaceholderText = "Pret pe gram*";
            PretModMaterial.Size = new Size(125, 27);
            PretModMaterial.TabIndex = 24;
            PretModMaterial.TextChanged += textBox2_TextChanged;
            // 
            // TipModMaterial
            // 
            TipModMaterial.Location = new Point(1205, 305);
            TipModMaterial.Name = "TipModMaterial";
            TipModMaterial.PlaceholderText = "Tip*";
            TipModMaterial.Size = new Size(125, 27);
            TipModMaterial.TabIndex = 23;
            TipModMaterial.TextChanged += textBox2_TextChanged;
            // 
            // CuloareModMaterial
            // 
            CuloareModMaterial.Location = new Point(1205, 272);
            CuloareModMaterial.Name = "CuloareModMaterial";
            CuloareModMaterial.PlaceholderText = "Culoare*";
            CuloareModMaterial.Size = new Size(125, 27);
            CuloareModMaterial.TabIndex = 22;
            CuloareModMaterial.TextChanged += textBox4_TextChanged;
            // 
            // DenumireModMaterial
            // 
            DenumireModMaterial.Location = new Point(1205, 239);
            DenumireModMaterial.Name = "DenumireModMaterial";
            DenumireModMaterial.PlaceholderText = "Denumire*";
            DenumireModMaterial.Size = new Size(265, 27);
            DenumireModMaterial.TabIndex = 21;
            DenumireModMaterial.TextChanged += textBox5_TextChanged;
            // 
            // StergeButton
            // 
            StergeButton.Location = new Point(1376, 78);
            StergeButton.Name = "StergeButton";
            StergeButton.Size = new Size(94, 29);
            StergeButton.TabIndex = 27;
            StergeButton.Text = "Sterge";
            StergeButton.UseVisualStyleBackColor = true;
            StergeButton.Click += button1_Click_1;
            // 
            // ProducatoriForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 453);
            Controls.Add(StergeButton);
            Controls.Add(modificaMaterial);
            Controls.Add(ProdModMaterial);
            Controls.Add(PretModMaterial);
            Controls.Add(TipModMaterial);
            Controls.Add(CuloareModMaterial);
            Controls.Add(DenumireModMaterial);
            Controls.Add(adaugaMaterial);
            Controls.Add(PretAddMaterial);
            Controls.Add(TipAddMaterial);
            Controls.Add(CuloareAddMaterial);
            Controls.Add(DenumireAddMaterial);
            Controls.Add(dataGridView1);
            Controls.Add(dataGridViewProducator);
            Font = new Font("Segoe UI", 9F);
            MaximumSize = new Size(1500, 500);
            MinimumSize = new Size(1500, 500);
            Name = "ProducatoriForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Producatori&Materiale";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducator).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {


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

            if (selectedRowMaterial != null)
            {
                int producerId = Convert.ToInt32(selectedRowMaterial.Cells["producator"].Value);
                string denumire = Convert.ToString(selectedRowMaterial.Cells["denumire"].Value);
                string culoare = Convert.ToString(selectedRowMaterial.Cells["culoare"].Value);
                string tip = Convert.ToString(selectedRowMaterial.Cells["tip"].Value);
                double pret = Convert.ToDouble(selectedRowMaterial.Cells["pret_pe_gram"].Value);

                DenumireModMaterial.Text = denumire;
                CuloareModMaterial.Text = culoare;
                TipModMaterial.Text = tip;
                PretModMaterial.Text = Convert.ToString(pret);
                ProdModMaterial.Text = Convert.ToString(producerId);

            }
        }

        #endregion
        private DataGridView dataGridViewProducator;
        private DataGridView dataGridView1;
        private Button adaugaMaterial;
        private TextBox PretAddMaterial;
        private TextBox TipAddMaterial;
        private TextBox CuloareAddMaterial;
        private TextBox DenumireAddMaterial;
        private Button modificaMaterial;
        private TextBox ProdModMaterial;
        private TextBox PretModMaterial;
        private TextBox TipModMaterial;
        private TextBox CuloareModMaterial;
        private TextBox DenumireModMaterial;
        private Button StergeButton;
    }
}
