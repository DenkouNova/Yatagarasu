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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvDemons = new System.Windows.Forms.DataGridView();
            this.tlpTierOneLeft = new System.Windows.Forms.TableLayoutPanel();
            this.pGame = new System.Windows.Forms.Panel();
            this.lbCurrentGame = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pFamilies = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpTier1Races = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvRaces = new System.Windows.Forms.DataGridView();
            this.dgvRaces_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRaces_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Race = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMain.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemons)).BeginInit();
            this.tlpTierOneLeft.SuspendLayout();
            this.pGame.SuspendLayout();
            this.pFamilies.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpTier1Races.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaces)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panel2, 1, 0);
            this.tlpMain.Controls.Add(this.tlpTierOneLeft, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1287, 679);
            this.tlpMain.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(303, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(444, 673);
            this.panel2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvDemons, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 671);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(436, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Demons";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDemons
            // 
            this.dgvDemons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDemons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDemons_Id,
            this.dgvDemons_Level,
            this.dgvDemons_Race,
            this.dgvDemons_Name});
            this.dgvDemons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDemons.Location = new System.Drawing.Point(3, 23);
            this.dgvDemons.Name = "dgvDemons";
            this.dgvDemons.Size = new System.Drawing.Size(436, 645);
            this.dgvDemons.TabIndex = 3;
            // 
            // tlpTierOneLeft
            // 
            this.tlpTierOneLeft.ColumnCount = 1;
            this.tlpTierOneLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTierOneLeft.Controls.Add(this.pGame, 0, 0);
            this.tlpTierOneLeft.Controls.Add(this.pFamilies, 0, 1);
            this.tlpTierOneLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTierOneLeft.Location = new System.Drawing.Point(3, 3);
            this.tlpTierOneLeft.Name = "tlpTierOneLeft";
            this.tlpTierOneLeft.RowCount = 2;
            this.tlpTierOneLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpTierOneLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTierOneLeft.Size = new System.Drawing.Size(294, 673);
            this.tlpTierOneLeft.TabIndex = 0;
            // 
            // pGame
            // 
            this.pGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pGame.Controls.Add(this.lbCurrentGame);
            this.pGame.Controls.Add(this.label1);
            this.pGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGame.Location = new System.Drawing.Point(3, 3);
            this.pGame.Name = "pGame";
            this.pGame.Size = new System.Drawing.Size(288, 74);
            this.pGame.TabIndex = 0;
            // 
            // lbCurrentGame
            // 
            this.lbCurrentGame.Font = new System.Drawing.Font("MS Mincho", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentGame.Location = new System.Drawing.Point(1, 17);
            this.lbCurrentGame.Name = "lbCurrentGame";
            this.lbCurrentGame.Size = new System.Drawing.Size(282, 56);
            this.lbCurrentGame.TabIndex = 1;
            this.lbCurrentGame.Text = "$lbCurrentGame";
            this.lbCurrentGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current game";
            // 
            // pFamilies
            // 
            this.pFamilies.Controls.Add(this.panel1);
            this.pFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pFamilies.Location = new System.Drawing.Point(3, 83);
            this.pFamilies.Name = "pFamilies";
            this.pFamilies.Size = new System.Drawing.Size(288, 587);
            this.pFamilies.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tlpTier1Races);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(288, 587);
            this.panel1.TabIndex = 2;
            // 
            // tlpTier1Races
            // 
            this.tlpTier1Races.ColumnCount = 1;
            this.tlpTier1Races.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTier1Races.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTier1Races.Controls.Add(this.label2, 0, 0);
            this.tlpTier1Races.Controls.Add(this.dgvRaces, 0, 1);
            this.tlpTier1Races.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTier1Races.Location = new System.Drawing.Point(0, 0);
            this.tlpTier1Races.Name = "tlpTier1Races";
            this.tlpTier1Races.RowCount = 2;
            this.tlpTier1Races.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpTier1Races.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTier1Races.Size = new System.Drawing.Size(286, 585);
            this.tlpTier1Races.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Races";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvRaces
            // 
            this.dgvRaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRaces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRaces_Id,
            this.dgvRaces_Name});
            this.dgvRaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRaces.Location = new System.Drawing.Point(3, 23);
            this.dgvRaces.Name = "dgvRaces";
            this.dgvRaces.Size = new System.Drawing.Size(280, 559);
            this.dgvRaces.TabIndex = 3;
            // 
            // dgvRaces_Id
            // 
            this.dgvRaces_Id.HeaderText = "Id";
            this.dgvRaces_Id.Name = "dgvRaces_Id";
            this.dgvRaces_Id.ReadOnly = true;
            this.dgvRaces_Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRaces_Id.Width = 40;
            // 
            // dgvRaces_Name
            // 
            this.dgvRaces_Name.HeaderText = "Name";
            this.dgvRaces_Name.Name = "dgvRaces_Name";
            this.dgvRaces_Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRaces_Name.Width = 175;
            // 
            // dgvDemons_Id
            // 
            this.dgvDemons_Id.HeaderText = "Id";
            this.dgvDemons_Id.Name = "dgvDemons_Id";
            this.dgvDemons_Id.ReadOnly = true;
            this.dgvDemons_Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemons_Id.Width = 40;
            // 
            // dgvDemons_Level
            // 
            this.dgvDemons_Level.HeaderText = "Lv";
            this.dgvDemons_Level.Name = "dgvDemons_Level";
            this.dgvDemons_Level.Width = 40;
            // 
            // dgvDemons_Race
            // 
            this.dgvDemons_Race.HeaderText = "Race";
            this.dgvDemons_Race.Name = "dgvDemons_Race";
            // 
            // dgvDemons_Name
            // 
            this.dgvDemons_Name.HeaderText = "Name";
            this.dgvDemons_Name.Name = "dgvDemons_Name";
            this.dgvDemons_Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemons_Name.Width = 180;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 679);
            this.Controls.Add(this.tlpMain);
            this.Name = "MainForm";
            this.Text = "MainForm2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tlpMain.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemons)).EndInit();
            this.tlpTierOneLeft.ResumeLayout(false);
            this.pGame.ResumeLayout(false);
            this.pGame.PerformLayout();
            this.pFamilies.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tlpTier1Races.ResumeLayout(false);
            this.tlpTier1Races.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaces)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpTierOneLeft;
        private System.Windows.Forms.Panel pGame;
        private System.Windows.Forms.Label lbCurrentGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pFamilies;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlpTier1Races;
        private System.Windows.Forms.DataGridView dgvRaces;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRaces_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRaces_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvDemons;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Race;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Name;
    }
}