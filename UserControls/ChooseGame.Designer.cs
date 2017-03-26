namespace Yatagarasu.UserControls
{
    partial class ChooseGame
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
            this.btnNewGame = new System.Windows.Forms.Button();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pRight = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbGameName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pLeft = new System.Windows.Forms.Panel();
            this.btnChooseGame = new System.Windows.Forms.Button();
            this.cbGame = new System.Windows.Forms.ComboBox();
            this.lblChooseGame = new System.Windows.Forms.Label();
            this.pDown = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.pRight.SuspendLayout();
            this.pLeft.SuspendLayout();
            this.pDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(79, 126);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(98, 39);
            this.btnNewGame.TabIndex = 4;
            this.btnNewGame.Text = "Save";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.pRight, 1, 0);
            this.tlpMain.Controls.Add(this.pLeft, 0, 0);
            this.tlpMain.Controls.Add(this.pDown, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpMain.Size = new System.Drawing.Size(522, 253);
            this.tlpMain.TabIndex = 2;
            // 
            // pRight
            // 
            this.pRight.Controls.Add(this.btnNewGame);
            this.pRight.Controls.Add(this.label2);
            this.pRight.Controls.Add(this.tbGameName);
            this.pRight.Controls.Add(this.label1);
            this.pRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pRight.Location = new System.Drawing.Point(264, 3);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(255, 197);
            this.pRight.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Game title";
            // 
            // tbGameName
            // 
            this.tbGameName.Location = new System.Drawing.Point(20, 82);
            this.tbGameName.Name = "tbGameName";
            this.tbGameName.Size = new System.Drawing.Size(216, 20);
            this.tbGameName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(75, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "New game";
            // 
            // pLeft
            // 
            this.pLeft.Controls.Add(this.btnChooseGame);
            this.pLeft.Controls.Add(this.cbGame);
            this.pLeft.Controls.Add(this.lblChooseGame);
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pLeft.Location = new System.Drawing.Point(3, 3);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(255, 197);
            this.pLeft.TabIndex = 0;
            // 
            // btnChooseGame
            // 
            this.btnChooseGame.Location = new System.Drawing.Point(85, 126);
            this.btnChooseGame.Name = "btnChooseGame";
            this.btnChooseGame.Size = new System.Drawing.Size(98, 39);
            this.btnChooseGame.TabIndex = 5;
            this.btnChooseGame.Text = "Continue";
            this.btnChooseGame.UseVisualStyleBackColor = true;
            this.btnChooseGame.Click += new System.EventHandler(this.btnChooseGame_Click);
            // 
            // cbGame
            // 
            this.cbGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGame.FormattingEnabled = true;
            this.cbGame.Location = new System.Drawing.Point(19, 63);
            this.cbGame.Name = "cbGame";
            this.cbGame.Size = new System.Drawing.Size(218, 21);
            this.cbGame.TabIndex = 1;
            this.cbGame.SelectedIndexChanged += new System.EventHandler(this.cbGame_SelectedIndexChanged);
            // 
            // lblChooseGame
            // 
            this.lblChooseGame.AutoSize = true;
            this.lblChooseGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChooseGame.Location = new System.Drawing.Point(67, 10);
            this.lblChooseGame.Name = "lblChooseGame";
            this.lblChooseGame.Size = new System.Drawing.Size(119, 20);
            this.lblChooseGame.TabIndex = 0;
            this.lblChooseGame.Text = "Choose game";
            // 
            // pDown
            // 
            this.tlpMain.SetColumnSpan(this.pDown, 2);
            this.pDown.Controls.Add(this.btnExit);
            this.pDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDown.Location = new System.Drawing.Point(3, 206);
            this.pDown.Name = "pDown";
            this.pDown.Size = new System.Drawing.Size(516, 44);
            this.pDown.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(206, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 39);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ChooseGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 253);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.Name = "ChooseGame";
            this.Text = "ChooseGameForm";
            this.tlpMain.ResumeLayout(false);
            this.pRight.ResumeLayout(false);
            this.pRight.PerformLayout();
            this.pLeft.ResumeLayout(false);
            this.pLeft.PerformLayout();
            this.pDown.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pRight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbGameName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Button btnChooseGame;
        private System.Windows.Forms.ComboBox cbGame;
        private System.Windows.Forms.Label lblChooseGame;
        private System.Windows.Forms.Panel pDown;
        private System.Windows.Forms.Button btnExit;
    }
}