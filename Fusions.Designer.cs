namespace Yatagarasu
{
    partial class FusionsForm
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
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.dgvFusions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFusions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(289, 550);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(110, 43);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(478, 550);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(110, 43);
            this.btnSaveChanges.TabIndex = 1;
            this.btnSaveChanges.Text = "Save changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // dgvFusions
            // 
            this.dgvFusions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFusions.Location = new System.Drawing.Point(28, 18);
            this.dgvFusions.Name = "dgvFusions";
            this.dgvFusions.Size = new System.Drawing.Size(825, 511);
            this.dgvFusions.TabIndex = 2;
            // 
            // FusionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 605);
            this.Controls.Add(this.dgvFusions);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnReload);
            this.Name = "FusionsForm";
            this.Text = "Fusions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FusionsForm_FormClosing);
            this.Load += new System.EventHandler(this.FusionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFusions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.DataGridView dgvFusions;
    }
}