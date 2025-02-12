﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WovenMonopoly
{
    class MonopolyGame
    {
        public List<BoardSpace> Board { get; }
        public Queue<int> Rolls { get; }
        private Queue<Player> Players { get; }
        public Dictionary<string, Player> PropertyOwners { get; } = new Dictionary<string, Player>();
        private const string Property = "property";

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

        public void PlayGame(string rolls)
        {
            while (Rolls.Count > 0)
            {
                var currentPlayer = Players.Dequeue();
                var roll = Rolls.Dequeue();
                currentPlayer.Move(roll, Board);

                var landedSpace = Board[currentPlayer.Position];

                if (landedSpace.Type == Property)
                {
                    HandleProperty(currentPlayer, landedSpace);
                }

                if (currentPlayer.Money <= 0)
                {
                    Console.WriteLine($"In {rolls}: {currentPlayer.Name} is bankrupt!");
                    DecideWinner(rolls);
                    return; // End game when a player goes bankrupt
                }

                Players.Enqueue(currentPlayer);
            }

            DecideWinner(rolls);
        }

        public void HandleProperty(Player player, BoardSpace space)
        {
            if (!PropertyOwners.TryGetValue(space.Name, out var owner))
            {
                // Buy property
                player.Money -= space.Price;
                player.OwnedProperties.Add(space.Name);
                PropertyOwners[space.Name] = player;
            }
            else
            {
                if (owner == player) return;
                var rent = space.Price;
                var ownedCount = Board.Count(prop => prop.Colour == space.Colour && owner.OwnedProperties.Contains(prop.Name));

                if (ownedCount == Board.FindAll(p => p.Colour == space.Colour).Count)
                {
                    rent *= 2; // Double rent if the owner has all of the same colour properties
                }

                player.PayRent(owner, rent);
            }
        }

        private void DecideWinner(string rolls)
        {
            Player winner = null;
            var maxMoney = 0;

            foreach (var player in Players.Where(player => player.Money > maxMoney))
            {
                maxMoney = player.Money;
                winner = player;
            }

            if (winner != null) Console.WriteLine($"In {rolls}: The winner is {winner.Name} with ${winner.Money} left!");
        }

        public void PrintFinalResults(string rolls)
        {
            foreach (var player in Players)
            {
                Console.WriteLine($"In {rolls}: {player.Name} has ${player.Money} and is on position {player.Position}");
            }
        }
    }
}