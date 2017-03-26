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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demonsListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partyDemonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menusToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1555, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menusToolStripMenuItem
            // 
            this.menusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseGameToolStripMenuItem,
            this.demonsListToolStripMenuItem,
            this.partyDemonsToolStripMenuItem});
            this.menusToolStripMenuItem.Name = "menusToolStripMenuItem";
            this.menusToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.menusToolStripMenuItem.Text = "Menus";
            // 
            // chooseGameToolStripMenuItem
            // 
            this.chooseGameToolStripMenuItem.Name = "chooseGameToolStripMenuItem";
            this.chooseGameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.chooseGameToolStripMenuItem.Text = "Choose Game";
            this.chooseGameToolStripMenuItem.Click += new System.EventHandler(this.chooseGameToolStripMenuItem_Click);
            // 
            // demonsListToolStripMenuItem
            // 
            this.demonsListToolStripMenuItem.Name = "demonsListToolStripMenuItem";
            this.demonsListToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.demonsListToolStripMenuItem.Text = "Demons List";
            this.demonsListToolStripMenuItem.Click += new System.EventHandler(this.demonsListToolStripMenuItem_Click);
            // 
            // partyDemonsToolStripMenuItem
            // 
            this.partyDemonsToolStripMenuItem.Name = "partyDemonsToolStripMenuItem";
            this.partyDemonsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.partyDemonsToolStripMenuItem.Text = "Party Demons";
            this.partyDemonsToolStripMenuItem.Click += new System.EventHandler(this.partyDemonsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1555, 832);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Yatagarasu";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem demonsListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem partyDemonsToolStripMenuItem;

    }
}

