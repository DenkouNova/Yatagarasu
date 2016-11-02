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
            this.btnReload = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyFusions)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPartyFusions
            // 
            this.dgvPartyFusions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartyFusions.Location = new System.Drawing.Point(3, 53);
            this.dgvPartyFusions.Name = "dgvPartyFusions";
            this.dgvPartyFusions.Size = new System.Drawing.Size(1008, 585);
            this.dgvPartyFusions.TabIndex = 0;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(3, 3);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(103, 44);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnReload);
            this.flowLayoutPanel1.Controls.Add(this.dgvPartyFusions);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1011, 638);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // PartyFusionsVerticalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 638);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "PartyFusionsVerticalForm";
            this.Text = "PartyFusions";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyFusions)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPartyFusions;
        public System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}