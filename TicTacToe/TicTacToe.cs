namespace TicTacToe
{
    public class Game
    {
        private char[,] board;
        private char currentPlayer;
        private bool gameOver;
        private char winner;
        private char p1;
        private char p2;

        public Game(char player1, char player2)
        {
            Console.WriteLine("Welcome to Tic Tac Toe");
            Console.WriteLine();
            currentPlayer = player1;

            //Create and populate a new board
            board = new char[3, 3];
            gameOver = false;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = ' ';
                }
            }
            WriteBoard();
            //Set players to the user specified players
            this.p1 = player1;
            this.p2 = player2;
        }

        public void Play()
        {
            while (!gameOver)
            {
                TakeTurn();
                WriteBoard();
                CheckWin();
                ChangePlayer();
            }
        }

        private void WriteBoard()
        {
            Console.WriteLine("+---+---+---+");

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write($" {board[i, j]} |");
                }
                Console.WriteLine();
                Console.WriteLine("+---+---+---+");
            }
        }

        private void TakeTurn()
        {
            int row = 0;
            int column = 0;

            while (true)
            {
                Console.WriteLine($"{currentPlayer}'s turn");
                // Get row from user if none has been specified
                if (row == 0)
                {
                    Console.Write("Pick a row (1, 2, 3): ");
                    //Validate input
                    if (!int.TryParse(Console.ReadLine(), out row) | row < 1 | row > 3)
                    {
                        row = 0;
                        OutputError("Please enter an integer between 1 and 3.");
                        continue;
                    }
                }
                // Get column from user if none has been specified
                if (column == 0)
                {
                    Console.Write("Pick a column (1, 2, 3): ");
                    //Validate input
                    if (!int.TryParse(Console.ReadLine(), out column) | column < 1 | column > 3)
                    {
                        column = 0;
                        OutputError("Please enter an integer between 1 and 3.");
                        continue;
                    }
                }

                // Check if space has been taken already
                if (board[row - 1, column - 1] != ' ')
                {
                    OutputError("That space is already taken. Please enter another space.");
                    row = 0;
                    column = 0;
                    continue;
                }
                //set specified space to the correct user
                board[row - 1, column - 1] = currentPlayer;

                break;
            }
        }

        private void ChangePlayer()
        {
            currentPlayer = currentPlayer == p1 ? p2 : p1;
        }

        private void CheckWin()
        {
            // check across
            for (int row = 0; row < board.GetLength(0); row++)
            {
                char first = board[row, 0];
                bool matched = true;

                for (int column = 0; column < board.GetLength(1); column++)
                {
                    if (first != board[row, column] || first == ' ')
                    {
                        matched = false;
                    }
                }

                if (matched)
                {
                    winner = first;
                    gameOver = true;
                }
            }
            // check down

            if (!gameOver)
            {
                for (int column = 0; column < board.GetLength(1); column++)
                {
                    char first = board[0, column];
                    bool matched = true;

                    for (int row = 0; row < board.GetLength(0); row++)
                    {
                        if (first != board[row, column] || first == ' ')
                        {
                            matched = false;
                        }
                    }

                    if (matched)
                    {
                        winner = first;
                        gameOver = true;
                    }
                }
            }

            // check diagonal
            if (!gameOver)
            {
                if ((board[1, 1] != ' ') & ((board[0, 0] == board[1, 1] & board[2, 2] == board[1, 1]) || (board[0, 2] == board[1, 1] & board[2, 0] == board[1, 1])))
                {
                    gameOver = true;
                    winner = board[1, 1];
                }
            }
            //Check for tie
            if (!gameOver)
            {
                bool allValueFilled = true;
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int column = 0; column < board.GetLength(1); column++)
                    {
                        if (board[row, column] == ' ')
                        {
                            allValueFilled = false;
                        }
                    }
                }
                if (allValueFilled)
                {
                    gameOver = true;
                    winner = ' ';
                }
            }
            //Display winner
            if (gameOver & winner != ' ')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{winner} wins!");
                Console.WriteLine("Game over!");
                Console.ResetColor();
            }
            //Display tie message
            else if (gameOver && winner == ' ')
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"This game has ended in a tie. There is no winner.");
                Console.WriteLine("Game over!");
                Console.ResetColor();
            }
        }

        private void OutputError(string error)
        {
            Console.Beep(750, 1500);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }
    }
}
