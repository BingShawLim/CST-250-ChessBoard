/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Chess Board Class Library Tests - Activity 2
 * Resources: CST-250 Activity 2 Guide (Grand Canyon University, 2025)
 */

using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

namespace ChessBoardClassLibrary.Tests
{
    /// <summary>
    /// xUnit tests for the BoardLogic business logic class
    /// </summary>
    public class BoardLogicTests
    {
        private readonly BoardLogic _boardLogic = new BoardLogic();

        /// <summary>
        /// Count how many cells are marked as legal next moves
        /// </summary>
        private static int CountLegalMoves(BoardModel board)
        {
            int count = 0;
            foreach (CellModel cell in board.Grid)
            {
                if (cell.IsLegalNextMove)
                {
                    count++;
                }
            }
            return count;
        }

        [Fact]
        public void BoardModel_Constructor_CreatesEveryCell()
        {
            BoardModel board = new BoardModel(8);

            Assert.Equal(8, board.Size);
            Assert.Equal(3, board.Grid[3, 5].Row);
            Assert.Equal(5, board.Grid[3, 5].Column);
            Assert.Equal("", board.Grid[3, 5].PieceOccupyingCell);
            Assert.False(board.Grid[3, 5].IsLegalNextMove);
        }

        [Theory]
        [InlineData("Knight", 0, 0, 2)]
        [InlineData("Knight", 4, 4, 8)]
        [InlineData("King", 0, 0, 3)]
        [InlineData("King", 4, 4, 8)]
        [InlineData("Rook", 4, 2, 14)]
        [InlineData("Bishop", 0, 0, 7)]
        [InlineData("Bishop", 3, 5, 11)]
        [InlineData("Queen", 1, 1, 23)]
        public void MarkLegalMoves_MarksCorrectNumberOfMoves(string piece, int row, int col, int expected)
        {
            BoardModel board = new BoardModel(8);

            board = _boardLogic.MarkLegalMoves(board, board.Grid[row, col], piece);

            Assert.Equal(expected, CountLegalMoves(board));
        }

        [Theory]
        [InlineData("knight", "N")]
        [InlineData("KING", "K")]
        [InlineData("Queen", "Q")]
        [InlineData("rook", "R")]
        [InlineData("Bishop", "B")]
        public void MarkLegalMoves_SetsPieceLetter_AnyCasing(string piece, string letter)
        {
            BoardModel board = new BoardModel(8);

            board = _boardLogic.MarkLegalMoves(board, board.Grid[2, 2], piece);

            Assert.Equal(letter, board.Grid[2, 2].PieceOccupyingCell);
            Assert.False(board.Grid[2, 2].IsLegalNextMove);
        }

        [Fact]
        public void MarkLegalMoves_UnknownPiece_MarksNothing()
        {
            BoardModel board = new BoardModel(8);

            board = _boardLogic.MarkLegalMoves(board, board.Grid[2, 2], "Dragon");

            Assert.Equal(0, CountLegalMoves(board));
        }

        [Fact]
        public void ResetBoard_ClearsPreviousMoves()
        {
            BoardModel board = new BoardModel(8);
            board = _boardLogic.MarkLegalMoves(board, board.Grid[4, 4], "Queen");

            board = _boardLogic.ResetBoard(board);

            Assert.Equal(0, CountLegalMoves(board));
            Assert.Equal("", board.Grid[4, 4].PieceOccupyingCell);
        }
    }
}
