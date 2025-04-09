namespace GameOfLife;

static class Program
{
    private const byte MinLiveNeighborsToSurvive = 2;
    private const byte MaxLiveNeighborsToSurvive = 3;

    private const string AliveCellSymbol = "■ ";
    private const string DeadCellSymbol = "□ ";

    private const int CellsArraySize = 25;

    static void Main()
    {
        try
        {
            var glider = new[,]
            {
                { CellState.Dead, CellState.Live, CellState.Dead },
                { CellState.Dead, CellState.Dead, CellState.Live },
                { CellState.Live, CellState.Live, CellState.Live }
            };

            // TODO: allow user to input the entry pattern
            // TODO: validate user input so that only 1 and 0 is allowed and no more than 25x25
            // TODO: Allow user to enter array of any size

            var generationNumber = 0;
            var cells = new CellState[CellsArraySize, CellsArraySize];
            var buffer = new CellState[CellsArraySize, CellsArraySize];

            // Place initial pattern in the center of an array
            PlacePatternCentered(cells, glider);
            DisplayCells(cells, generationNumber);

            while (true)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Escape) return;

                NextGeneration(cells, buffer);

                // Syntax sugar for:
                // var temp = cells;
                // cells = buffer;
                // buffer = temp;
                (cells, buffer) = (buffer, cells);
                generationNumber++;
                DisplayCells(cells, generationNumber);
            }
        }
        catch (Exception)
        {
            // TODO: Log exception
            Console.WriteLine("An error occurred, please contact customer support. Press any key to exit.");
    
            Console.ReadKey(intercept: true);
        }
    }

    private static void NextGeneration(CellState[,] cells, CellState[,] nextGeneration)
    {
        var rowLength = cells.GetLength(0);
        var colLength = cells.GetLength(1);

        for (var row = 0; row < rowLength; row++)
        for (var col = 0; col < colLength; col++)
        {
            var cellValue = cells[row, col];
            var aliveNeighboursCount = CountAliveNeighbours(cells, row, col);

            if (cellValue == CellState.Live)
            {
                nextGeneration[row, col] =
                    aliveNeighboursCount < MinLiveNeighborsToSurvive || aliveNeighboursCount > MaxLiveNeighborsToSurvive
                        ? CellState.Dead
                        : CellState.Live;
            }
            else if (aliveNeighboursCount == MaxLiveNeighborsToSurvive)
            {
                nextGeneration[row, col] = CellState.Live;
            }
            else
            {
                nextGeneration[row, col] = cellValue;
            }
        }
    }

    private static int CountAliveNeighbours(CellState[,] cells, int row, int col)
    {
        var rowLength = cells.GetLength(0);
        var colLength = cells.GetLength(1);

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

        return aliveCellsCount;
    }

    private static void DisplayCells(CellState[,] cells, int generationNumber)
    {
        Console.Clear();

        // TODO: put user messages to resources
        Console.WriteLine($"""
                           Welcome to the Game of Life!
                           Use the following keys:
                             Press Any Key - Next Generation
                             Esc           - Exit the game
                             
                             Generation {generationNumber}.
                           """);

        for (var row = 0; row < cells.GetLength(0); row++)
        {
            for (var col = 0; col < cells.GetLength(1); col++)
            {
                Console.Write(cells[row, col] == CellState.Live ? AliveCellSymbol : DeadCellSymbol);
            }

            Console.WriteLine();
        }
    }

    private static void PlacePatternCentered(CellState[,] grid, CellState[,] pattern)
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