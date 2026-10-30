/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Chess Board GUI App - Activity 2
 * Resources: CST-250 Activity 2 Guide (Grand Canyon University, 2025)
 */

using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

namespace ChessBoardGUIApp
{
    public partial class FrmChessBoard : Form
    {
        // Class level variables
        private BoardModel _board;
        private BoardLogic _boardLogic;
        private Button[,] _buttons;

        // Color themes: each array holds { light square, dark square, legal move, piece }
        private readonly Dictionary<string, Color[]> _themes = new Dictionary<string, Color[]>
        {
            { "Classic Checkerboard", new[] { Color.BurlyWood, Color.SaddleBrown, Color.LightGreen, Color.Gold } },
            { "Cool Colors", new[] { Color.LightCyan, Color.SteelBlue, Color.MediumAquamarine, Color.MediumPurple } },
            { "Warm Colors", new[] { Color.PeachPuff, Color.Coral, Color.Gold, Color.Firebrick } },
            { "Neon Colors", new[] { Color.Black, Color.FromArgb(40, 0, 60), Color.Lime, Color.Magenta } },
            { "Pastel Colors", new[] { Color.FromArgb(255, 179, 206), Color.FromArgb(186, 164, 236), Color.FromArgb(168, 230, 163), Color.FromArgb(130, 196, 255) } },
            { "Nature Colors", new[] { Color.Khaki, Color.OliveDrab, Color.YellowGreen, Color.Sienna } },
            { "Gradient (Blue to Purple)", new[] { Color.LightSkyBlue, Color.MediumPurple, Color.Yellow, Color.OrangeRed } }
        };

        /// <summary>
        /// Constructor for the chess board form
        /// </summary>
        public FrmChessBoard()
        {
            InitializeComponent();

            // Initialize the class level variables
            _board = new BoardModel(8);
            _boardLogic = new BoardLogic();
            _buttons = new Button[_board.Size, _board.Size];

            // Fill the theme drop down and start with the classic theme
            foreach (string themeName in _themes.Keys)
            {
                cmbThemes.Items.Add(themeName);
            }
            cmbThemes.SelectedIndex = 0;

            SetUpButtons();
            UpdateButtons();
        }

        /// <summary>
        /// Fill the panel with a grid of buttons that mirrors the board
        /// </summary>
        private void SetUpButtons()
        {
            // Calculate the size of each button from the panel size
            int buttonSize = pnlChessBoard.Width / _board.Size;

            // Make the panel square so the buttons fit nicely
            pnlChessBoard.Height = pnlChessBoard.Width;

            // Loop through the grid
            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    _buttons[row, col] = new Button();
                    _buttons[row, col].Width = buttonSize;
                    _buttons[row, col].Height = buttonSize;

                    // Place the button: columns go across, rows go down
                    _buttons[row, col].Left = col * buttonSize;
                    _buttons[row, col].Top = row * buttonSize;

                    _buttons[row, col].FlatStyle = FlatStyle.Flat;
                    _buttons[row, col].Font = new Font("Segoe UI", 8F, FontStyle.Bold);

                    // Attach the click event handler
                    _buttons[row, col].Click += BtnSquareClickEH;

                    // Store the location of the button in its Tag
                    _buttons[row, col].Tag = new Point(row, col);
                    _buttons[row, col].Text = $"{row}, {col}";

                    // Add the button to the panel
                    pnlChessBoard.Controls.Add(_buttons[row, col]);
                }
            }
        }// End of SetUpButtons method

        /// <summary>
        /// Update the text and colors of every button from the board
        /// </summary>
        private void UpdateButtons()
        {
            // Map the chess notation letters to the full piece names
            Dictionary<string, string> pieceMap = new Dictionary<string, string>
            {
                { "K", "King" },
                { "Q", "Queen" },
                { "B", "Bishop" },
                { "N", "Knight" },
                { "R", "Rook" }
            };

            Color[] colors = GetThemeColors();

            for (int row = 0; row < _board.Size; row++)
            {
                for (int col = 0; col < _board.Size; col++)
                {
                    CellModel cell = _board.Grid[row, col];
                    Button button = _buttons[row, col];
                    button.FlatAppearance.BorderColor = Color.DimGray;

                    if (cell.PieceOccupyingCell != "" && pieceMap.ContainsKey(cell.PieceOccupyingCell))
                    {
                        // The square holding the piece
                        button.Text = pieceMap[cell.PieceOccupyingCell];
                        button.BackColor = colors[3];
                    }
                    else if (cell.IsLegalNextMove)
                    {
                        // A legal next move
                        button.Text = "Legal Move";
                        button.BackColor = colors[2];
                    }
                    else
                    {
                        // An empty square uses the theme pattern
                        button.Text = $"{row}, {col}";
                        button.BackColor = GetSquareColor(row, col, colors);
                    }

                    // Pick black or white text so it is easy to read on the square
                    button.ForeColor = GetTextColor(button.BackColor);
                }
            }
        }// End of UpdateButtons method

        /// <summary>
        /// Get the color array for the theme chosen in the theme drop down
        /// </summary>
        /// <returns>The theme colors</returns>
        private Color[] GetThemeColors()
        {
            string themeName = cmbThemes.SelectedItem?.ToString() ?? "";
            if (_themes.ContainsKey(themeName))
            {
                return _themes[themeName];
            }
            return _themes["Classic Checkerboard"];
        }// End of GetThemeColors method

        /// <summary>
        /// Get the color of an empty square: a checkerboard for most themes,
        /// or a top-to-bottom fade for the gradient theme
        /// </summary>
        /// <param name="row">The row of the square</param>
        /// <param name="col">The column of the square</param>
        /// <param name="colors">The theme colors</param>
        /// <returns>The color for the square</returns>
        private Color GetSquareColor(int row, int col, Color[] colors)
        {
            string themeName = cmbThemes.SelectedItem?.ToString() ?? "";
            if (themeName.StartsWith("Gradient"))
            {
                // Blend from the first color (top row) to the second color (bottom row)
                double amount = (double)row / (_board.Size - 1);
                int red = (int)(colors[0].R + (colors[1].R - colors[0].R) * amount);
                int green = (int)(colors[0].G + (colors[1].G - colors[0].G) * amount);
                int blue = (int)(colors[0].B + (colors[1].B - colors[0].B) * amount);
                return Color.FromArgb(red, green, blue);
            }

            // Checkerboard pattern
            return (row + col) % 2 == 0 ? colors[0] : colors[1];
        }// End of GetSquareColor method

        /// <summary>
        /// Choose white text for dark squares and black text for light squares
        /// </summary>
        /// <param name="backColor">The background color of the square</param>
        /// <returns>The text color</returns>
        private Color GetTextColor(Color backColor)
        {
            return backColor.GetBrightness() < 0.5f ? Color.White : Color.Black;
        }// End of GetTextColor method

        /// <summary>
        /// Show the legal moves when a square is clicked
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The event arguments</param>
        private void BtnSquareClickEH(object sender, EventArgs e)
        {
            // Get the button that was clicked and its location from the Tag
            Button clickedButton = (Button)sender;
            Point location = clickedButton.Tag is Point point ? point : new Point(-1, -1);
            int row = location.X;
            int col = location.Y;

            // Error checking: a piece must be chosen first
            if (cmbChessPieces.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a chess piece first.", "No Piece Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Error checking: the location must be on the board
            if (row < 0 || row >= _board.Size || col < 0 || col >= _board.Size)
            {
                MessageBox.Show("That square is not on the board.", "Out of Bounds",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"You clicked on cell {row}, {col}", "Cell Selected");

            // Mark the legal moves for the selected piece and update the buttons
            string piece = cmbChessPieces.SelectedItem?.ToString() ?? "";
            _board = _boardLogic.MarkLegalMoves(_board, _board.Grid[row, col], piece);
            lblStatus.Text = $"{piece} placed at {row}, {col}.";
            UpdateButtons();
        }// End of BtnSquareClickEH method

        /// <summary>
        /// Redraw the board when a new color theme is chosen
        /// </summary>
        /// <param name="sender">The theme drop down</param>
        /// <param name="e">The event arguments</param>
        private void CmbThemesSelectedIndexChangedEH(object sender, EventArgs e)
        {
            // The buttons do not exist yet the first time the theme is set
            if (_buttons != null && _buttons[0, 0] != null)
            {
                UpdateButtons();
            }
        }// End of CmbThemesSelectedIndexChangedEH method
    }
}
