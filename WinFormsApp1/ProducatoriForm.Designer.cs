
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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            dataGridViewProducator = new DataGridView();
            dataGridView1 = new DataGridView();
            adaugaMaterial = new Button();
            StergeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducator).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewProducator
            // 
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Control;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridViewProducator.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewProducator.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Window;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dataGridViewProducator.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewProducator.Location = new Point(12, 12);
            dataGridViewProducator.Name = "dataGridViewProducator";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Control;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dataGridViewProducator.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewProducator.RowHeadersWidth = 51;
            dataGridViewProducator.Size = new Size(592, 413);
            dataGridViewProducator.TabIndex = 13;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = SystemColors.Control;
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = SystemColors.Window;
            dataGridViewCellStyle11.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle11.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle11;
            dataGridView1.Location = new Point(620, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = SystemColors.Control;
            dataGridViewCellStyle12.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(577, 413);
            dataGridView1.TabIndex = 14;
            // 
            // adaugaMaterial
            // 
            adaugaMaterial.Location = new Point(1219, 339);
            adaugaMaterial.Name = "adaugaMaterial";
            adaugaMaterial.Size = new Size(135, 29);
            adaugaMaterial.TabIndex = 20;
            adaugaMaterial.Text = "Adauga/Modifica";
            adaugaMaterial.UseVisualStyleBackColor = true;
            adaugaMaterial.Click += Add_Click_1;
            // 
            // StergeButton
            // 
            StergeButton.Location = new Point(1219, 396);
            StergeButton.Name = "StergeButton";
            StergeButton.Size = new Size(94, 29);
            StergeButton.TabIndex = 27;
            StergeButton.Text = "Sterge";
            StergeButton.UseVisualStyleBackColor = true;
            StergeButton.Click += Delete_Click_1;
            // 
            // ProducatoriForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 453);
            Controls.Add(StergeButton);
            Controls.Add(adaugaMaterial);
            Controls.Add(dataGridView1);
            Controls.Add(dataGridViewProducator);
            Font = new Font("Segoe UI", 9F);
            MaximumSize = new Size(1400, 500);
            MinimumSize = new Size(1400, 500);
            Name = "ProducatoriForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Producatori&Materiale";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewProducator).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }


        #endregion
        private DataGridView dataGridViewProducator;
        private DataGridView dataGridView1;
        private Button adaugaMaterial;
        private Button StergeButton;
    }
}
