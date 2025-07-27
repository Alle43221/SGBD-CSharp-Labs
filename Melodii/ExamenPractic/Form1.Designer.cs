namespace ExamenPractic
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dataGridViewParent = new DataGridView();
            dataGridViewChild = new DataGridView();
            btnAddChild = new Button();
            btnDeleteChild = new Button();
            btnUpdateChild = new Button();
            Briose = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewParent).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewChild).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewParent
            // 
            dataGridViewParent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewParent.Location = new Point(35, 70);
            dataGridViewParent.Name = "dataGridViewParent";
            dataGridViewParent.RowHeadersWidth = 51;
            dataGridViewParent.Size = new Size(328, 350);
            dataGridViewParent.TabIndex = 0;
            // 
            // dataGridViewChild
            // 
            dataGridViewChild.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewChild.Location = new Point(401, 70);
            dataGridViewChild.Name = "dataGridViewChild";
            dataGridViewChild.RowHeadersWidth = 51;
            dataGridViewChild.Size = new Size(360, 285);
            dataGridViewChild.TabIndex = 1;
            // 
            // btnAddChild
            // 
            btnAddChild.BackColor = Color.LavenderBlush;
            btnAddChild.Font = new Font("Modern No. 20", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddChild.Location = new Point(401, 379);
            btnAddChild.Name = "btnAddChild";
            btnAddChild.Size = new Size(105, 41);
            btnAddChild.TabIndex = 5;
            btnAddChild.Text = "Add";
            btnAddChild.UseVisualStyleBackColor = false;
            // 
            // btnDeleteChild
            // 
            btnDeleteChild.BackColor = Color.LavenderBlush;
            btnDeleteChild.Font = new Font("Modern No. 20", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeleteChild.Location = new Point(634, 379);
            btnDeleteChild.Name = "btnDeleteChild";
            btnDeleteChild.Size = new Size(127, 41);
            btnDeleteChild.TabIndex = 6;
            btnDeleteChild.Text = "Delete";
            btnDeleteChild.UseVisualStyleBackColor = false;
            // 
            // btnUpdateChild
            // 
            btnUpdateChild.BackColor = Color.LavenderBlush;
            btnUpdateChild.Font = new Font("Modern No. 20", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdateChild.Location = new Point(512, 379);
            btnUpdateChild.Name = "btnUpdateChild";
            btnUpdateChild.Size = new Size(116, 41);
            btnUpdateChild.TabIndex = 7;
            btnUpdateChild.Text = "Update";
            btnUpdateChild.UseVisualStyleBackColor = false;
            // 
            // Briose
            // 
            Briose.AutoSize = true;
            Briose.BackColor = Color.LavenderBlush;
            Briose.Font = new Font("Modern No. 20", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Briose.Location = new Point(401, 27);
            Briose.Name = "Briose";
            Briose.Size = new Size(90, 25);
            Briose.TabIndex = 9;
            Briose.Text = "Melodii";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.LavenderBlush;
            label1.Font = new Font("Modern No. 20", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(35, 27);
            label1.Name = "label1";
            label1.Size = new Size(80, 25);
            label1.TabIndex = 10;
            label1.Text = "Artisti";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Thistle;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(Briose);
            Controls.Add(btnUpdateChild);
            Controls.Add(btnDeleteChild);
            Controls.Add(btnAddChild);
            Controls.Add(dataGridViewChild);
            Controls.Add(dataGridViewParent);
            Name = "Form1";
            Text = "Examen Practic SGBD";
            ((System.ComponentModel.ISupportInitialize)dataGridViewParent).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewChild).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewParent;
        private DataGridView dataGridViewChild;
        private Button btnAddChild;
        private Button btnDeleteChild;
        private Button btnUpdateChild;
        private Label Briose;
        private Label label1;
    }
}
