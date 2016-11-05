namespace Yatagarasu
{
    partial class PartyFusionsVerticalForm
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
            this.dgvPartyFusions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyFusions)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPartyFusions
            // 
            this.dgvPartyFusions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartyFusions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPartyFusions.Location = new System.Drawing.Point(0, 0);
            this.dgvPartyFusions.Name = "dgvPartyFusions";
            this.dgvPartyFusions.Size = new System.Drawing.Size(1011, 638);
            this.dgvPartyFusions.TabIndex = 0;
            // 
            // PartyFusionsVerticalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 638);
            this.Controls.Add(this.dgvPartyFusions);
            this.Name = "PartyFusionsVerticalForm";
            this.Text = "PartyFusions";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyFusions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPartyFusions;
    }
}