using System.IO;
using System.Reflection;

namespace WovenMonopoly
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string board = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\board.json");
            string rolls = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\rolls_2.json");
            var game = new MonopolyGame(board, rolls);
            game.PlayGame();
            game.PrintFinalStandings();
        }
    }
}