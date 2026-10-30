/*
 * Bing Shaw Lim
 * CST-250
 * 10/28/2026
 * Chess Board Console App - Activity 2
 * Resources: CST-250 Activity 2 Guide (Grand Canyon University, 2025)
 */

using ChessBoardClassLibrary.Models;
using ChessBoardClassLibrary.Services.BusinessLogicLayer;

//----------------------------------------------------------
// Start of the Main Method
//----------------------------------------------------------

// Declare and initialize variables
string piece = "";
Tuple<int, int> rowAndCol;
bool placeAnother = true;

Console.WriteLine("Hello, welcome to the Chess Board App!");

// Create a standard 8 x 8 board and print it empty
BoardModel board = new BoardModel(8);
Utility.PrintBoard(board);

// Create the business logic object used to mark the legal moves
BoardLogic boardLogic = new BoardLogic();

while (placeAnother)
{
    // Ask the user which piece to place (only valid names are accepted)
    piece = Utility.GetChessPiece();

    // Ask the user for the location of the piece (only numbers on the board are accepted)
    rowAndCol = Utility.GetRowAndCol(board.Size);

    // Mark the legal moves for the piece
    board = boardLogic.MarkLegalMoves(board, board.Grid[rowAndCol.Item1, rowAndCol.Item2], piece);

    // Print the board with the legal moves
    Utility.PrintBoard(board);

    // Ask the user if they want to place another piece
    placeAnother = Utility.GetYesOrNo("Would you like to place another piece? (y/n): ");
}

Console.WriteLine("Thank you for using the Chess Board App. Goodbye!");

//----------------------------------------------------------
// End of the Main Method
//----------------------------------------------------------

//----------------------------------------------------------
// Utility class for console helper methods
//----------------------------------------------------------
public static class Utility
{
    // The pieces the user is allowed to choose
    private static readonly string[] ValidPieces = { "King", "Queen", "Bishop", "Knight", "Rook" };

    /// <summary>
    /// Print the board with an outline around every square,
    /// column headers across the top, and row numbers down the side
    /// </summary>
    /// <param name="board">The board to print</param>
    internal static void PrintBoard(BoardModel board)
    {
        Console.WriteLine();

        // Column headers
        Console.Write("    ");
        for (int col = 0; col < board.Size; col++)
        {
            Console.Write($"  {col} ");
        }
        Console.WriteLine();
        PrintDivider(board.Size);

        // Loop through every row and column
        for (int row = 0; row < board.Size; row++)
        {
            // Row number
            Console.Write($" {row}  |");
            for (int col = 0; col < board.Size; col++)
            {
                CellModel cell = board.Grid[row, col];
                if (cell.PieceOccupyingCell != "")
                {
                    // The piece letter
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($" {cell.PieceOccupyingCell} ");
                }
                else if (cell.IsLegalNextMove)
                {
                    // A legal next move
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" + ");
                }
                else
                {
                    // An empty square
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(" . ");
                }
                Console.ResetColor();
                Console.Write("|");
            }
            Console.WriteLine();
            PrintDivider(board.Size);
        }
        Console.WriteLine("Legend: letter = chess piece (N = Knight), + = legal next move");
        Console.WriteLine();
    }// End of PrintBoard method

    /// <summary>
    /// Print one horizontal divider line for the board outline
    /// </summary>
    /// <param name="size">The size of the board</param>
    internal static void PrintDivider(int size)
    {
        Console.Write("    +");
        for (int col = 0; col < size; col++)
        {
            Console.Write("---+");
        }
        Console.WriteLine();
    }// End of PrintDivider method

    /// <summary>
    /// Ask the user for the row and column of the piece
    /// </summary>
    /// <param name="size">The size of the board</param>
    /// <returns>A tuple holding the row (Item1) and column (Item2)</returns>
    internal static Tuple<int, int> GetRowAndCol(int size)
    {
        int row = GetNumberInRange($"Enter the row number of the piece (0 - {size - 1}): ", 0, size - 1);
        int col = GetNumberInRange($"Enter the column number of the piece (0 - {size - 1}): ", 0, size - 1);
        return Tuple.Create(row, col);
    }// End of GetRowAndCol method

    /// <summary>
    /// Keep asking until the user types a whole number inside the range
    /// (fixes the out-of-bounds and input errors)
    /// </summary>
    /// <param name="prompt">The message shown to the user</param>
    /// <param name="min">The smallest number allowed</param>
    /// <param name="max">The largest number allowed</param>
    /// <returns>A valid number</returns>
    internal static int GetNumberInRange(string prompt, int min, int max)
    {
        int number;
        Console.Write(prompt);

        // TryParse stops letters from crashing the program
        while (!int.TryParse(Console.ReadLine(), out number) || number < min || number > max)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid input. Please enter a whole number from {min} to {max}.");
            Console.ResetColor();
            Console.Write(prompt);
        }
        return number;
    }// End of GetNumberInRange method

    /// <summary>
    /// Keep asking until the user types a valid chess piece name
    /// </summary>
    /// <returns>The piece name with standard capitalization</returns>
    internal static string GetChessPiece()
    {
        while (true)
        {
            Console.Write("Enter the type of piece (King, Queen, Bishop, Knight, Rook): ");
            string input = (Console.ReadLine() ?? "").Trim();

            // Compare without caring about capitalization
            foreach (string validPiece in ValidPieces)
            {
                if (string.Equals(input, validPiece, StringComparison.OrdinalIgnoreCase))
                {
                    return validPiece;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\"{input}\" is not a chess piece. Please try again.");
            Console.ResetColor();
        }
    }// End of GetChessPiece method

    /// <summary>
    /// Keep asking until the user answers y or n
    /// </summary>
    /// <param name="prompt">The message shown to the user</param>
    /// <returns>True for yes, false for no</returns>
    internal static bool GetYesOrNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim().ToLower();
            if (input == "y" || input == "yes")
            {
                return true;
            }
            if (input == "n" || input == "no")
            {
                return false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Please type y or n.");
            Console.ResetColor();
        }
    }// End of GetYesOrNo method
}
