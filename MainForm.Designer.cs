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
            this.tbLog = new System.Windows.Forms.TextBox();
            this.rbDisplayDemons = new System.Windows.Forms.RadioButton();
            this.rbDisplayFusions = new System.Windows.Forms.RadioButton();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pData = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // cbGame
            // 
            this.cbGame.FormattingEnabled = true;
            this.cbGame.Location = new System.Drawing.Point(12, 29);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(289, 21);
            this.cbGame.TabIndex = 0;
            this.cbGame.SelectedIndexChanged += new System.EventHandler(this.cbGame_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose game";
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(15, 133);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(286, 478);
            this.tbLog.TabIndex = 3;
            // 
            // rbDisplayDemons
            // 
            this.rbDisplayDemons.AutoSize = true;
            this.rbDisplayDemons.Location = new System.Drawing.Point(27, 66);
            this.rbDisplayDemons.Name = "rbDisplayDemons";
            this.rbDisplayDemons.Size = new System.Drawing.Size(99, 17);
            this.rbDisplayDemons.TabIndex = 10;
            this.rbDisplayDemons.TabStop = true;
            this.rbDisplayDemons.Text = "Display demons";
            this.rbDisplayDemons.UseVisualStyleBackColor = true;
            // 
            // rbDisplayFusions
            // 
            this.rbDisplayFusions.AutoSize = true;
            this.rbDisplayFusions.Location = new System.Drawing.Point(27, 89);
            this.rbDisplayFusions.Name = "rbDisplayFusions";
            this.rbDisplayFusions.Size = new System.Drawing.Size(95, 17);
            this.rbDisplayFusions.TabIndex = 11;
            this.rbDisplayFusions.TabStop = true;
            this.rbDisplayFusions.Text = "Display fusions";
            this.rbDisplayFusions.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(164, 68);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(110, 38);
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pData
            // 
            this.pData.Location = new System.Drawing.Point(320, 13);
            this.pData.Name = "pData";
            this.pData.Size = new System.Drawing.Size(768, 597);
            this.pData.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 623);
            this.Controls.Add(this.pData);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.rbDisplayFusions);
            this.Controls.Add(this.rbDisplayDemons);
            this.Controls.Add(this.tbLog);
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
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.RadioButton rbDisplayDemons;
        private System.Windows.Forms.RadioButton rbDisplayFusions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pData;
    }
}

