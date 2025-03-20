
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
            StergeButton = new Button();
            button1 = new Button();
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
            // 
            // adaugaMaterial
            // 
            adaugaMaterial.Location = new Point(1219, 282);
            adaugaMaterial.Name = "adaugaMaterial";
            adaugaMaterial.Size = new Size(135, 29);
            adaugaMaterial.TabIndex = 20;
            adaugaMaterial.Text = "Adauga";
            adaugaMaterial.UseVisualStyleBackColor = true;
            adaugaMaterial.Click += Add_Click_1;
            // 
            // StergeButton
            // 
            StergeButton.Location = new Point(1219, 396);
            StergeButton.Name = "StergeButton";
            StergeButton.Size = new Size(135, 29);
            StergeButton.TabIndex = 27;
            StergeButton.Text = "Sterge";
            StergeButton.UseVisualStyleBackColor = true;
            StergeButton.Click += Delete_Click_1;
            // 
            // button1
            // 
            button1.Location = new Point(1219, 341);
            button1.Name = "button1";
            button1.Size = new Size(135, 29);
            button1.TabIndex = 28;
            button1.Text = "Modifica";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Modifica_Click;
            // 
            // ProducatoriForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 453);
            Controls.Add(button1);
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
        private Button button1;
    }
}
