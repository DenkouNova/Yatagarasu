namespace Yatagarasu
{
    partial class MainForm
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
            this.cbGame = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFusions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbGame
            // 
            this.cbGame.FormattingEnabled = true;
            this.cbGame.Location = new System.Drawing.Point(21, 33);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(253, 21);
            this.cbGame.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose game";
            // 
            // btnFusions
            // 
            this.btnFusions.Location = new System.Drawing.Point(85, 83);
            this.btnFusions.Name = "btnFusions";
            this.btnFusions.Size = new System.Drawing.Size(119, 41);
            this.btnFusions.TabIndex = 2;
            this.btnFusions.Text = "Fusions";
            this.btnFusions.UseVisualStyleBackColor = true;
            this.btnFusions.Click += new System.EventHandler(this.btnFusions_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 211);
            this.Controls.Add(this.btnFusions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbGame);
            this.Name = "MainForm";
            this.Text = "Yatagarasu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFusions;
    }
}

