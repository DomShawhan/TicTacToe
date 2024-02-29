using System.Data.Common;
using TicTacToe;

namespace Proj0803TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game('X', 'O');

            game.Play();
        }
    }
}
