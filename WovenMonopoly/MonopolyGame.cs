using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WovenMonopoly
{
    class MonopolyGame
    {
        private List<BoardSpace> Board { get; }
        private Queue<int> Rolls { get; }
        private Queue<Player> Players { get; }
        private Dictionary<string, Player> PropertyOwners { get; } = new Dictionary<string, Player>();

        public MonopolyGame(string boardFile, string rollsFile)
        {
            Board = LoadBoard(boardFile);
            Rolls = new Queue<int>(LoadRolls(rollsFile));
            Players = new Queue<Player>(new List<Player>
            {
                new Player("Peter"),
                new Player("Billy"),
                new Player("Charlotte"),
                new Player("Sweedal")
            });
        }

        private List<BoardSpace> LoadBoard(string fileName)
        {
            return JsonConvert.DeserializeObject<List<BoardSpace>>(File.ReadAllText(fileName));
        }

        private List<int> LoadRolls(string fileName)
        {
            return JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(fileName));
        }

        public void PlayGame()
        {
            while (Rolls.Count > 0)
            {
                var currentPlayer = Players.Dequeue();
                int roll = Rolls.Dequeue();
                currentPlayer.Move(roll, Board);

                var landedSpace = Board[currentPlayer.Position];

                if (landedSpace.Type == "property")
                {
                    HandleProperty(currentPlayer, landedSpace);
                }

                if (currentPlayer.Money <= 0)
                {
                    Console.WriteLine($"{currentPlayer.Name} is bankrupt!");
                    return; // End game when a player goes bankrupt
                }

                Players.Enqueue(currentPlayer);
            }

            DeclareWinner();
        }

        private void HandleProperty(Player player, BoardSpace space)
        {
            if (!PropertyOwners.ContainsKey(space.Name))
            {
                // Buy property
                player.Money -= space.Price;
                player.OwnedProperties.Add(space.Name);
                PropertyOwners[space.Name] = player;
            }
            else
            {
                Player owner = PropertyOwners[space.Name];
                if (owner != player)
                {
                    int rent = space.Price;
                    int ownedCount = 0;
                    foreach (var prop in Board)
                    {
                        if (prop.Colour == space.Colour && owner.OwnedProperties.Contains(prop.Name))
                        {
                            ownedCount++;
                        }
                    }

                    if (ownedCount == Board.FindAll(p => p.Colour == space.Colour).Count)
                    {
                        rent *= 2; // Double rent if the owner has all of the same colour properties
                    }

                    player.PayRent(owner, rent);
                }
            }
        }

        private void DeclareWinner()
        {
            Player winner = null;
            int maxMoney = 0;

            foreach (var player in Players)
            {
                if (player.Money > maxMoney)
                {
                    maxMoney = player.Money;
                    winner = player;
                }
            }

            Console.WriteLine($"The winner is {winner.Name} with ${winner.Money} left!");
        }

        public void PrintFinalStandings()
        {
            foreach (var player in Players)
            {
                Console.WriteLine($"{player.Name} has ${player.Money} and is on position {player.Position}");
            }
        }
    }
}