namespace WinFormsApp1
{
    partial class MaterialeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DenumireAddMaterial = new TextBox();
            CuloareAddMaterial = new TextBox();
            TipAddMaterial = new TextBox();
            PretAddMaterial = new TextBox();
            ProducatorAddMaterial = new TextBox();
            adaugaMaterial = new Button();
            ProducatorEditMaterial = new TextBox();
            PretEditMaterial = new TextBox();
            TipEditMaterial = new TextBox();
            CuloareEditMaterial = new TextBox();
            DenumireEditMaterial = new TextBox();
            modificaMaterial = new Button();
            dataGridViewMateriale = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMateriale).BeginInit();
            SuspendLayout();
            // 
            // DenumireAddMaterial
            // 
            DenumireAddMaterial.Location = new Point(509, 12);
            DenumireAddMaterial.Name = "DenumireAddMaterial";
            DenumireAddMaterial.PlaceholderText = "Denumire*";
            DenumireAddMaterial.Size = new Size(265, 27);
            DenumireAddMaterial.TabIndex = 0;
            DenumireAddMaterial.TextChanged += textBox1_TextChanged;
            // 
            // CuloareAddMaterial
            // 
            CuloareAddMaterial.Location = new Point(509, 45);
            CuloareAddMaterial.Name = "CuloareAddMaterial";
            CuloareAddMaterial.PlaceholderText = "Culoare*";
            CuloareAddMaterial.Size = new Size(125, 27);
            CuloareAddMaterial.TabIndex = 1;
            CuloareAddMaterial.TextChanged += textBox2_TextChanged;
            // 
            // TipAddMaterial
            // 
            TipAddMaterial.Location = new Point(509, 78);
            TipAddMaterial.Name = "TipAddMaterial";
            TipAddMaterial.PlaceholderText = "Tip*";
            TipAddMaterial.Size = new Size(125, 27);
            TipAddMaterial.TabIndex = 2;
            // 
            // PretAddMaterial
            // 
            PretAddMaterial.Location = new Point(509, 111);
            PretAddMaterial.Name = "PretAddMaterial";
            PretAddMaterial.PlaceholderText = "Pret pe gram*";
            PretAddMaterial.Size = new Size(125, 27);
            PretAddMaterial.TabIndex = 3;
            // 
            // ProducatorAddMaterial
            // 
            ProducatorAddMaterial.Location = new Point(509, 144);
            ProducatorAddMaterial.Name = "ProducatorAddMaterial";
            ProducatorAddMaterial.PlaceholderText = "Producator*";
            ProducatorAddMaterial.Size = new Size(125, 27);
            ProducatorAddMaterial.TabIndex = 4;
            ProducatorAddMaterial.TextChanged += textBox5_TextChanged;
            // 
            // adaugaMaterial
            // 
            adaugaMaterial.Location = new Point(680, 43);
            adaugaMaterial.Name = "adaugaMaterial";
            adaugaMaterial.Size = new Size(94, 29);
            adaugaMaterial.TabIndex = 5;
            adaugaMaterial.Text = "Adauga";
            adaugaMaterial.UseVisualStyleBackColor = true;
            adaugaMaterial.Click += button1_Click;
            // 
            // ProducatorEditMaterial
            // 
            ProducatorEditMaterial.Location = new Point(509, 358);
            ProducatorEditMaterial.Name = "ProducatorEditMaterial";
            ProducatorEditMaterial.PlaceholderText = "Producator*";
            ProducatorEditMaterial.Size = new Size(125, 27);
            ProducatorEditMaterial.TabIndex = 10;
            // 
            // PretEditMaterial
            // 
            PretEditMaterial.Location = new Point(509, 325);
            PretEditMaterial.Name = "PretEditMaterial";
            PretEditMaterial.PlaceholderText = "Pret pe gram*";
            PretEditMaterial.Size = new Size(125, 27);
            PretEditMaterial.TabIndex = 9;
            // 
            // TipEditMaterial
            // 
            TipEditMaterial.Location = new Point(509, 292);
            TipEditMaterial.Name = "TipEditMaterial";
            TipEditMaterial.PlaceholderText = "Tip*";
            TipEditMaterial.Size = new Size(125, 27);
            TipEditMaterial.TabIndex = 8;
            TipEditMaterial.TextChanged += textBox8_TextChanged;
            // 
            // CuloareEditMaterial
            // 
            CuloareEditMaterial.Location = new Point(509, 259);
            CuloareEditMaterial.Name = "CuloareEditMaterial";
            CuloareEditMaterial.PlaceholderText = "Culoare*";
            CuloareEditMaterial.Size = new Size(125, 27);
            CuloareEditMaterial.TabIndex = 7;
            // 
            // DenumireEditMaterial
            // 
            DenumireEditMaterial.Location = new Point(509, 226);
            DenumireEditMaterial.Name = "DenumireEditMaterial";
            DenumireEditMaterial.PlaceholderText = "Denumire*";
            DenumireEditMaterial.Size = new Size(265, 27);
            DenumireEditMaterial.TabIndex = 6;
            // 
            // modificaMaterial
            // 
            modificaMaterial.Location = new Point(680, 259);
            modificaMaterial.Name = "modificaMaterial";
            modificaMaterial.Size = new Size(94, 29);
            modificaMaterial.TabIndex = 11;
            modificaMaterial.Text = "Modifica";
            modificaMaterial.UseVisualStyleBackColor = true;
            modificaMaterial.Click += button2_Click;
            // 
            // dataGridViewMateriale
            // 
            dataGridViewMateriale.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMateriale.Location = new Point(12, 12);
            dataGridViewMateriale.Name = "dataGridViewMateriale";
            dataGridViewMateriale.RowHeadersWidth = 51;
            dataGridViewMateriale.Size = new Size(491, 429);
            dataGridViewMateriale.TabIndex = 12;
            // 
            // MaterialeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 453);
            Controls.Add(dataGridViewMateriale);
            Controls.Add(modificaMaterial);
            Controls.Add(ProducatorEditMaterial);
            Controls.Add(PretEditMaterial);
            Controls.Add(TipEditMaterial);
            Controls.Add(CuloareEditMaterial);
            Controls.Add(DenumireEditMaterial);
            Controls.Add(adaugaMaterial);
            Controls.Add(ProducatorAddMaterial);
            Controls.Add(PretAddMaterial);
            Controls.Add(TipAddMaterial);
            Controls.Add(CuloareAddMaterial);
            Controls.Add(DenumireAddMaterial);
            MaximumSize = new Size(800, 500);
            MinimumSize = new Size(800, 500);
            Name = "MaterialeForm";
            Text = "MaterialeForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewMateriale).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox DenumireAddMaterial;
        private TextBox CuloareAddMaterial;
        private TextBox TipAddMaterial;
        private TextBox PretAddMaterial;
        private TextBox ProducatorAddMaterial;
        private Button adaugaMaterial;
        private TextBox ProducatorEditMaterial;
        private TextBox PretEditMaterial;
        private TextBox TipEditMaterial;
        private TextBox CuloareEditMaterial;
        private TextBox DenumireEditMaterial;
        private Button modificaMaterial;
        private DataGridView dataGridViewMateriale;
    }
}