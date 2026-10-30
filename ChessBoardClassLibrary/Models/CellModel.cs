/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Chess Board Class Library - Activity 2
 * Resources: CST-250 Activity 2 Guide (Grand Canyon University, 2025)
 */

namespace ChessBoardClassLibrary.Models
{
    /// <summary>
    /// One square on the chessboard. Holds data only.
    /// </summary>
    public class CellModel
    {
        // Class level properties
        // Row and Column have private setters (encapsulation): only this class can set them
        public int Row { get; private set; }
        public int Column { get; private set; }
        public string PieceOccupyingCell { get; set; }
        public bool IsLegalNextMove { get; set; }

        /// <summary>
        /// Parameterized constructor for a cell
        /// </summary>
        /// <param name="row">The row of the cell</param>
        /// <param name="column">The column of the cell</param>
        public CellModel(int row, int column)
        {
            Row = row;
            Column = column;
            // Default values: the cell starts empty and is not a legal move
            PieceOccupyingCell = "";
            IsLegalNextMove = false;
        }
    }
}
