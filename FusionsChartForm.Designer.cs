namespace Yatagarasu
{
    partial class FusionsChartForm
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
            this.dgvFusions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFusions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFusions
            // 
            this.dgvFusions.AllowUserToAddRows = false;
            this.dgvFusions.AllowUserToDeleteRows = false;
            this.dgvFusions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFusions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFusions.Location = new System.Drawing.Point(0, 0);
            this.dgvFusions.Name = "dgvFusions";
            this.dgvFusions.RowHeadersVisible = false;
            this.dgvFusions.Size = new System.Drawing.Size(1339, 667);
            this.dgvFusions.TabIndex = 0;
            // 
            // FusionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 667);
            this.Controls.Add(this.dgvFusions);
            this.Name = "FusionsForm";
            this.Text = "Fusions Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FusionsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFusions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFusions;
    }
}