using System;
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
            var rolls1 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\rolls_1.json");
            
            var game1 = new MonopolyGame(board, rolls1);
            game1.PlayGame("rolls 1");
            game1.PrintFinalResults("rolls 1");

            var rolls2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\rolls_2.json");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var game2 = new MonopolyGame(board, rolls2);
            game2.PlayGame("rolls 2");
            game2.PrintFinalResults("rolls 2");
            Console.ReadKey();
        }
    }
}