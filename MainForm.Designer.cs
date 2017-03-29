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
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvFusions = new System.Windows.Forms.DataGridView();
            this.tlpTierOneLeft = new System.Windows.Forms.TableLayoutPanel();
            this.pGame = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCurrentGame = new System.Windows.Forms.Button();
            this.pFamilies = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlpTier1Races = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvRaces = new System.Windows.Forms.DataGridView();
            this.dgvRaces_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRaces_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvDemons = new System.Windows.Forms.DataGridView();
            this.dgvDemons_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Fuse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvDemons_InParty = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvDemons_Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Race = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDemons_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Id1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Level1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Race1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Level2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Race2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Name2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Level3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Race3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFusions_Name3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlpMain.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFusions)).BeginInit();
            this.tlpTierOneLeft.SuspendLayout();
            this.pGame.SuspendLayout();
            this.pFamilies.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tlpTier1Races.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaces)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemons)).BeginInit();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 430F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.panel4, 2, 0);
            this.tlpMain.Controls.Add(this.tlpTierOneLeft, 0, 0);
            this.tlpMain.Controls.Add(this.panel2, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1552, 679);
            this.tlpMain.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tableLayoutPanel2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(733, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(816, 673);
            this.panel4.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dgvFusions, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(814, 671);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(808, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Fusions";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvFusions
            // 
            this.dgvFusions.AllowUserToAddRows = false;
            this.dgvFusions.AllowUserToDeleteRows = false;
            this.dgvFusions.AllowUserToResizeRows = false;
            this.dgvFusions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFusions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvFusions_Id1,
            this.dgvFusions_Level1,
            this.dgvFusions_Race1,
            this.dgvFusions_Name1,
            this.dgvFusions_Level2,
            this.dgvFusions_Race2,
            this.dgvFusions_Name2,
            this.dgvFusions_Level3,
            this.dgvFusions_Race3,
            this.dgvFusions_Name3});
            this.dgvFusions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFusions.Location = new System.Drawing.Point(3, 23);
            this.dgvFusions.Name = "dgvFusions";
            this.dgvFusions.RowHeadersVisible = false;
            this.dgvFusions.Size = new System.Drawing.Size(808, 645);
            this.dgvFusions.TabIndex = 3;
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
            this.pGame.Controls.Add(this.label1);
            this.pGame.Controls.Add(this.btnCurrentGame);
            this.pGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGame.Location = new System.Drawing.Point(3, 3);
            this.pGame.Name = "pGame";
            this.pGame.Size = new System.Drawing.Size(288, 74);
            this.pGame.TabIndex = 0;
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
            // btnCurrentGame
            // 
            this.btnCurrentGame.Font = new System.Drawing.Font("MS Mincho", 14.25F);
            this.btnCurrentGame.Location = new System.Drawing.Point(3, 19);
            this.btnCurrentGame.Name = "btnCurrentGame";
            this.btnCurrentGame.Size = new System.Drawing.Size(283, 55);
            this.btnCurrentGame.TabIndex = 4;
            this.btnCurrentGame.Text = "$lbCurrentGame";
            this.btnCurrentGame.UseVisualStyleBackColor = true;
            this.btnCurrentGame.Click += new System.EventHandler(this.btnCurrentGame_Click);
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
            this.dgvRaces.AllowUserToDeleteRows = false;
            this.dgvRaces.AllowUserToResizeRows = false;
            this.dgvRaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRaces.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRaces_Id,
            this.dgvRaces_Name});
            this.dgvRaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRaces.Location = new System.Drawing.Point(3, 23);
            this.dgvRaces.Name = "dgvRaces";
            this.dgvRaces.RowHeadersVisible = false;
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
            this.dgvRaces_Name.Width = 210;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(303, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(424, 673);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(422, 671);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(416, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Demons";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvDemons
            // 
            this.dgvDemons.AllowUserToDeleteRows = false;
            this.dgvDemons.AllowUserToResizeRows = false;
            this.dgvDemons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDemons.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDemons_Id,
            this.dgvDemons_Fuse,
            this.dgvDemons_InParty,
            this.dgvDemons_Level,
            this.dgvDemons_Race,
            this.dgvDemons_Name});
            this.dgvDemons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDemons.Location = new System.Drawing.Point(3, 23);
            this.dgvDemons.Name = "dgvDemons";
            this.dgvDemons.RowHeadersVisible = false;
            this.dgvDemons.Size = new System.Drawing.Size(416, 645);
            this.dgvDemons.TabIndex = 3;
            this.dgvDemons.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDemons_CellContentClick);
            this.dgvDemons.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvDemons_CurrentCellDirtyStateChanged);
            // 
            // dgvDemons_Id
            // 
            this.dgvDemons_Id.HeaderText = "Id";
            this.dgvDemons_Id.Name = "dgvDemons_Id";
            this.dgvDemons_Id.ReadOnly = true;
            this.dgvDemons_Id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemons_Id.Width = 35;
            // 
            // dgvDemons_Fuse
            // 
            this.dgvDemons_Fuse.HeaderText = "Fuse";
            this.dgvDemons_Fuse.Name = "dgvDemons_Fuse";
            this.dgvDemons_Fuse.Width = 30;
            // 
            // dgvDemons_InParty
            // 
            this.dgvDemons_InParty.HeaderText = "Party";
            this.dgvDemons_InParty.Name = "dgvDemons_InParty";
            this.dgvDemons_InParty.Width = 30;
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
            this.dgvDemons_Race.Width = 90;
            // 
            // dgvDemons_Name
            // 
            this.dgvDemons_Name.HeaderText = "Name";
            this.dgvDemons_Name.Name = "dgvDemons_Name";
            this.dgvDemons_Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDemons_Name.Width = 170;
            // 
            // dgvFusions_Id1
            // 
            this.dgvFusions_Id1.HeaderText = "Id1";
            this.dgvFusions_Id1.Name = "dgvFusions_Id1";
            this.dgvFusions_Id1.Visible = false;
            // 
            // dgvFusions_Level1
            // 
            this.dgvFusions_Level1.HeaderText = "Lv";
            this.dgvFusions_Level1.Name = "dgvFusions_Level1";
            this.dgvFusions_Level1.Width = 30;
            // 
            // dgvFusions_Race1
            // 
            this.dgvFusions_Race1.HeaderText = "Race";
            this.dgvFusions_Race1.Name = "dgvFusions_Race1";
            this.dgvFusions_Race1.Width = 80;
            // 
            // dgvFusions_Name1
            // 
            this.dgvFusions_Name1.HeaderText = "Name";
            this.dgvFusions_Name1.Name = "dgvFusions_Name1";
            this.dgvFusions_Name1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFusions_Name1.Width = 150;
            // 
            // dgvFusions_Level2
            // 
            this.dgvFusions_Level2.HeaderText = "Lv";
            this.dgvFusions_Level2.Name = "dgvFusions_Level2";
            this.dgvFusions_Level2.Width = 30;
            // 
            // dgvFusions_Race2
            // 
            this.dgvFusions_Race2.HeaderText = "Race";
            this.dgvFusions_Race2.Name = "dgvFusions_Race2";
            this.dgvFusions_Race2.Width = 80;
            // 
            // dgvFusions_Name2
            // 
            this.dgvFusions_Name2.HeaderText = "Name";
            this.dgvFusions_Name2.Name = "dgvFusions_Name2";
            this.dgvFusions_Name2.Width = 150;
            // 
            // dgvFusions_Level3
            // 
            this.dgvFusions_Level3.HeaderText = "Lv";
            this.dgvFusions_Level3.Name = "dgvFusions_Level3";
            this.dgvFusions_Level3.Width = 30;
            // 
            // dgvFusions_Race3
            // 
            this.dgvFusions_Race3.HeaderText = "Race";
            this.dgvFusions_Race3.Name = "dgvFusions_Race3";
            this.dgvFusions_Race3.Width = 80;
            // 
            // dgvFusions_Name3
            // 
            this.dgvFusions_Name3.HeaderText = "Name";
            this.dgvFusions_Name3.Name = "dgvFusions_Name3";
            this.dgvFusions_Name3.Width = 150;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 679);
            this.Controls.Add(this.tlpMain);
            this.Name = "MainForm";
            this.Text = "MainForm2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tlpMain.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFusions)).EndInit();
            this.tlpTierOneLeft.ResumeLayout(false);
            this.pGame.ResumeLayout(false);
            this.pGame.PerformLayout();
            this.pFamilies.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tlpTier1Races.ResumeLayout(false);
            this.tlpTier1Races.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRaces)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDemons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TableLayoutPanel tlpTierOneLeft;
        private System.Windows.Forms.Panel pGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pFamilies;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlpTier1Races;
        private System.Windows.Forms.DataGridView dgvRaces;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvDemons;
        private System.Windows.Forms.Button btnCurrentGame;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvFusions;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRaces_Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRaces_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Id;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvDemons_Fuse;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvDemons_InParty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Race;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDemons_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Id1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Level1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Race1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Level2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Race2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Name2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Level3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Race3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFusions_Name3;
    }
}