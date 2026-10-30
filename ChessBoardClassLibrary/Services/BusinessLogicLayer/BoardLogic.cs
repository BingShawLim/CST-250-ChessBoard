/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Chess Board Class Library - Activity 2
 * Resources: CST-250 Activity 2 Guide (Grand Canyon University, 2025)
 */

using ChessBoardClassLibrary.Models;

namespace ChessBoardClassLibrary.Services.BusinessLogicLayer
{
    /// <summary>
    /// Business logic for the chessboard. The board is created and held by the
    /// presentation layer, so every method takes the board and returns it.
    /// </summary>
    public class BoardLogic
    {
        /// <summary>
        /// Clear every cell so the board is ready for the next move
        /// </summary>
        /// <param name="board">The board to reset</param>
        /// <returns>The reset board</returns>
        public BoardModel ResetBoard(BoardModel board)
        {
            // A foreach loop visits every CellModel in the 2D array
            foreach (CellModel cell in board.Grid)
            {
                cell.IsLegalNextMove = false;
                cell.PieceOccupyingCell = "";
            }
            return board;
        }// End of ResetBoard method

        /// <summary>
        /// Check if a row and column location is on the board
        /// </summary>
        /// <param name="board">The board to check against</param>
        /// <param name="row">The row to check</param>
        /// <param name="col">The column to check</param>
        /// <returns>True if the location is on the board</returns>
        private bool IsOnBoard(BoardModel board, int row, int col)
        {
            // Check the row and the column individually, then combine the results
            bool isRowOnBoard = row >= 0 && row < board.Size;
            bool isColOnBoard = col >= 0 && col < board.Size;
            return isRowOnBoard && isColOnBoard;
        }// End of IsOnBoard method

        /// <summary>
        /// Mark every legal next move for a chess piece placed on a cell
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the piece is placed on</param>
        /// <param name="chessPiece">The name of the chess piece</param>
        /// <returns>The board with the legal moves marked</returns>
        public BoardModel MarkLegalMoves(BoardModel board, CellModel currentCell, string chessPiece)
        {
            // Clear any moves from the last turn
            board = ResetBoard(board);

            // Error checking: return the empty board if the input cannot be used
            if (currentCell == null || string.IsNullOrWhiteSpace(chessPiece)
                || !IsOnBoard(board, currentCell.Row, currentCell.Column))
            {
                return board;
            }

            // ToLower means the casing of the piece name does not matter
            switch (chessPiece.Trim().ToLower())
            {
                case "knight":
                    board = MarkValidKnightMoves(board, currentCell);
                    break;
                case "king":
                    board = MarkValidKingMoves(board, currentCell);
                    break;
                case "rook":
                    board = MarkValidRookMoves(board, currentCell);
                    break;
                case "bishop":
                    board = MarkValidBishopMoves(board, currentCell);
                    break;
                case "queen":
                    board = MarkValidQueenMoves(board, currentCell);
                    break;
                default:
                    // Unknown piece: nothing is marked
                    break;
            }
            return board;
        }// End of MarkLegalMoves method

        /// <summary>
        /// Mark the eight L-shaped moves of a knight
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the knight is on</param>
        /// <returns>The board with the knight moves marked</returns>
        private BoardModel MarkValidKnightMoves(BoardModel board, CellModel currentCell)
        {
            // Each matching pair of numbers is one L-shaped move
            int[] rowMoves = { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] colMoves = { -1, 1, -2, 2, -2, 2, -1, 1 };

            board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "N";

            // Check all eight moves
            for (int i = 0; i < rowMoves.Length; i++)
            {
                int newRow = currentCell.Row + rowMoves[i];
                int newCol = currentCell.Column + colMoves[i];
                if (IsOnBoard(board, newRow, newCol))
                {
                    board.Grid[newRow, newCol].IsLegalNextMove = true;
                }
            }
            return board;
        }// End of MarkValidKnightMoves method

        /// <summary>
        /// Mark the eight squares around a king
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the king is on</param>
        /// <returns>The board with the king moves marked</returns>
        private BoardModel MarkValidKingMoves(BoardModel board, CellModel currentCell)
        {
            int[] rowMoves = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] colMoves = { -1, 0, 1, -1, 1, -1, 0, 1 };

            board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "K";

            for (int i = 0; i < rowMoves.Length; i++)
            {
                int newRow = currentCell.Row + rowMoves[i];
                int newCol = currentCell.Column + colMoves[i];
                if (IsOnBoard(board, newRow, newCol))
                {
                    board.Grid[newRow, newCol].IsLegalNextMove = true;
                }
            }
            return board;
        }// End of MarkValidKingMoves method

        /// <summary>
        /// Mark every square up, down, left, and right of a rook
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the rook is on</param>
        /// <returns>The board with the rook moves marked</returns>
        private BoardModel MarkValidRookMoves(BoardModel board, CellModel currentCell)
        {
            // Up, down, left, right
            int[] rowDirections = { -1, 1, 0, 0 };
            int[] colDirections = { 0, 0, -1, 1 };

            board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "R";
            return MarkSlidingMoves(board, currentCell, rowDirections, colDirections);
        }// End of MarkValidRookMoves method

        /// <summary>
        /// Mark every square along the four diagonals of a bishop
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the bishop is on</param>
        /// <returns>The board with the bishop moves marked</returns>
        private BoardModel MarkValidBishopMoves(BoardModel board, CellModel currentCell)
        {
            // Up-left, up-right, down-left, down-right
            int[] rowDirections = { -1, -1, 1, 1 };
            int[] colDirections = { -1, 1, -1, 1 };

            board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "B";
            return MarkSlidingMoves(board, currentCell, rowDirections, colDirections);
        }// End of MarkValidBishopMoves method

        /// <summary>
        /// Mark the queen's moves, which are the rook and bishop moves combined
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the queen is on</param>
        /// <returns>The board with the queen moves marked</returns>
        private BoardModel MarkValidQueenMoves(BoardModel board, CellModel currentCell)
        {
            // Reuse the rook and bishop logic
            board = MarkValidRookMoves(board, currentCell);
            board = MarkValidBishopMoves(board, currentCell);

            // Those methods set their own letters, so set the queen's letter last
            board.Grid[currentCell.Row, currentCell.Column].PieceOccupyingCell = "Q";
            return board;
        }// End of MarkValidQueenMoves method

        /// <summary>
        /// Slide one square at a time in each direction until the edge of the board
        /// </summary>
        /// <param name="board">The board to mark</param>
        /// <param name="currentCell">The cell the piece is on</param>
        /// <param name="rowDirections">The row step for each direction</param>
        /// <param name="colDirections">The column step for each direction</param>
        /// <returns>The board with the moves marked</returns>
        private BoardModel MarkSlidingMoves(BoardModel board, CellModel currentCell,
            int[] rowDirections, int[] colDirections)
        {
            for (int i = 0; i < rowDirections.Length; i++)
            {
                int newRow = currentCell.Row + rowDirections[i];
                int newCol = currentCell.Column + colDirections[i];

                // Keep moving in this direction while still on the board
                while (IsOnBoard(board, newRow, newCol))
                {
                    board.Grid[newRow, newCol].IsLegalNextMove = true;
                    newRow += rowDirections[i];
                    newCol += colDirections[i];
                }
            }
            return board;
        }// End of MarkSlidingMoves method
    }
}
