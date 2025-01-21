using System.IO;
using System.Reflection;

namespace WovenMonopoly
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var board = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\board.json");
            var rolls = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\rolls_1.json");
            var game = new MonopolyGame(board, rolls);
            game.PlayGame();
            game.PrintFinalResults();
        }
    }
}