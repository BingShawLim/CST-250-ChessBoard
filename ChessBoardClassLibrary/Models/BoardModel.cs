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
    /// A square chessboard made of a 2D array of CellModel objects.
    /// </summary>
    public class BoardModel
    {
        // Class level properties
        // Private setters (encapsulation): only the board can change its size or grid
        public int Size { get; private set; }
        public CellModel[,] Grid { get; private set; }

        /// <summary>
        /// Parameterized constructor for the board
        /// </summary>
        /// <param name="size">The number of rows and columns on the board</param>
        public BoardModel(int size)
        {
            Size = size;
            Grid = new CellModel[size, size];
            InitializeBoard();
        }

        /// <summary>
        /// Fill every space in the grid with a new CellModel.
        /// Private so the details of building the grid stay hidden (encapsulation).
        /// </summary>
        private void InitializeBoard()
        {
            // Loop through every row
            for (int row = 0; row < Size; row++)
            {
                // Loop through every column
                for (int col = 0; col < Size; col++)
                {
                    Grid[row, col] = new CellModel(row, col);
                }
            }
        }// End of InitializeBoard method
    }
}
