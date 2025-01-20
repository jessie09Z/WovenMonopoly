using System.Collections.Generic;

namespace WovenMonopoly
{
    class Player
    {
        public string Name { get; }
        public int Position { get; set; } = 0;
        public int Money { get; set; } = 16;
        public HashSet<string> OwnedProperties { get; } = new HashSet<string>();

        public Player(string name)
        {
            Name = name;
        }

        public void Move(int steps, List<BoardSpace> board)
        {
            Position = (Position + steps) % board.Count;
            if (Position == 0 && steps > 0)
            {
                Money += 1; // Collect $1 when passing GO
            }
        }

        public void PayRent(Player owner, int rent)
        {
            Money -= rent;
            owner.Money += rent;
        }
    }
}