namespace Yatagarasu
{
    partial class FullDemonsListForm
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
            this.dgvDemons = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemons)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDemons
            // 
            this.dgvDemons.AllowUserToResizeRows = false;
            this.dgvDemons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDemons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDemons.Location = new System.Drawing.Point(0, 0);
            this.dgvDemons.Name = "dgvDemons";
            this.dgvDemons.Size = new System.Drawing.Size(654, 524);
            this.dgvDemons.TabIndex = 0;
            // 
            // FullDemonsListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(654, 524);
            this.Controls.Add(this.dgvDemons);
            this.Name = "FullDemonsListForm";
            this.Text = "Demons List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DemonsListForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDemons;

    }
}