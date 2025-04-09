namespace GameOfLife;

static class Program
{
    static void Main()
    {
        // TODO: use enum Live, Dead
        var glider = new byte[,]
        {
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 1, 1, 1 }
        };

        // TODO: allow user to input the entry pattern
        // TODO: validate user input so that only 1 and 0 is allowed and no more than 25x25
        // TODO: Allow user to enter array of any size

        byte[,] cells = new byte[25, 25];
        byte[,] buffer = new byte[25, 25];

        // Place initial pattern in the center of an array
        PlacePatternCentered(cells, glider);
        DisplayCells(cells);

        while (true)
        {
            var key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape) return;

            var isSuccess = NextGeneration(cells, buffer);

            // Syntax sugar for:
            // var temp = cells;
            // cells = buffer;
            // buffer = temp;
            (cells, buffer) = (buffer, cells);

            if (!isSuccess)
            {
                Console.WriteLine("""
                                  Error occurred.
                                  Press any key to exit.
                                  """);

                // TODO: Go back to main menu
                Console.ReadKey(intercept: true);
                return;
            }

            DisplayCells(cells);
        }
    }

    private static bool NextGeneration(byte[,] cells, byte[,] nextGeneration)
    {
        var rowLength = cells.GetLength(0);
        var colLength = cells.GetLength(1);

        for (var row = 0; row < rowLength; row++)
        for (var col = 0; col < colLength; col++)
        {
            var cellValue = cells[row, col];
            if (cellValue != 0 && cellValue != 1)
            {
                // TODO: Move user strings to resources
                Console.WriteLine($"Something went wrong. Unknown cell value. [{row}, {col}] = {cellValue}");
                return false;
            }

            var aliveCellsCount = 0;

            for (var neighbourRow = row - 1; neighbourRow <= row + 1; neighbourRow++)
            for (var neighbourCol = col - 1; neighbourCol <= col + 1; neighbourCol++)
            {
                if (neighbourRow < 0 || neighbourRow >= rowLength ||
                    neighbourCol < 0 || neighbourCol >= colLength ||
                    (neighbourRow == row && neighbourCol == col) ||
                    cells[neighbourRow, neighbourCol] == 0)
                    continue;

                aliveCellsCount++;
            }

            // TODO: Place all numbers to consts
            if (cellValue == 1)
            {
                nextGeneration[row, col] = aliveCellsCount < 2 || aliveCellsCount > 3 ? (byte)0 : (byte)1;
            }
            else if (aliveCellsCount == 3)
            {
                nextGeneration[row, col] = 1;
            }
            else
            {
                nextGeneration[row, col] = cellValue;
            }
        }

        return true;
    }

    private static void DisplayCells(byte[,] cells)
    {
        Console.Clear();

        // TODO: show generation number
        Console.WriteLine("""
                          Welcome to the Game of Life!
                          Use the following keys:
                            Press Any Key - Next Generation
                            Esc           - Exit the game
                          """);

        for (var row = 0; row < cells.GetLength(0); row++)
        {
            for (var col = 0; col < cells.GetLength(1); col++)
            {
                Console.Write(cells[row, col] == 1 ? "■ " : "□ ");
            }

            Console.WriteLine();
        }
    }

    private static void PlacePatternCentered(byte[,] grid, byte[,] pattern)
    {
        var gridRows = grid.GetLength(0);
        var gridCols = grid.GetLength(1);
        var patternRows = pattern.GetLength(0);
        var patternCols = pattern.GetLength(1);

        var startRow = (gridRows - patternRows) / 2;
        var startCol = (gridCols - patternCols) / 2;

        for (var r = 0; r < patternRows; r++)
        for (var c = 0; c < patternCols; c++)
        {
            grid[startRow + r, startCol + c] = pattern[r, c];
        }
    }
}