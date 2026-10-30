namespace ChessBoardGUIApp
{
    partial class FrmChessBoard
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            cmbChessPieces = new ComboBox();
            label3 = new Label();
            cmbThemes = new ComboBox();
            pnlChessBoard = new Panel();
            lblStatus = new Label();
            SuspendLayout();
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(20, 15);
            label1.Name = "label1";
            label1.Size = new Size(470, 23);
            label1.TabIndex = 0;
            label1.Text = "Select a chess piece and its location on the board and see the legal moves.";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(20, 55);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 1;
            label2.Text = "Pieces:";
            //
            // cmbChessPieces
            //
            cmbChessPieces.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChessPieces.FormattingEnabled = true;
            cmbChessPieces.Items.AddRange(new object[] { "King", "Queen", "Bishop", "Knight", "Rook" });
            cmbChessPieces.Location = new Point(80, 52);
            cmbChessPieces.Name = "cmbChessPieces";
            cmbChessPieces.Size = new Size(150, 28);
            cmbChessPieces.TabIndex = 2;
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Location = new Point(260, 55);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 3;
            label3.Text = "Theme:";
            //
            // cmbThemes
            //
            cmbThemes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbThemes.FormattingEnabled = true;
            cmbThemes.Location = new Point(320, 52);
            cmbThemes.Name = "cmbThemes";
            cmbThemes.Size = new Size(200, 28);
            cmbThemes.TabIndex = 4;
            cmbThemes.SelectedIndexChanged += CmbThemesSelectedIndexChangedEH;
            //
            // pnlChessBoard
            //
            pnlChessBoard.Location = new Point(20, 95);
            pnlChessBoard.Name = "pnlChessBoard";
            pnlChessBoard.Size = new Size(500, 500);
            pnlChessBoard.TabIndex = 5;
            //
            // lblStatus
            //
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 605);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(260, 20);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Choose a piece, then click a square.";
            //
            // FrmChessBoard
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(660, 640);
            Controls.Add(lblStatus);
            Controls.Add(pnlChessBoard);
            Controls.Add(cmbThemes);
            Controls.Add(label3);
            Controls.Add(cmbChessPieces);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmChessBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Chess Board";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox cmbChessPieces;
        private Label label3;
        private ComboBox cmbThemes;
        private Panel pnlChessBoard;
        private Label lblStatus;
    }
}
