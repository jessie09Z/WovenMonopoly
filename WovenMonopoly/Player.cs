using System.Collections.Generic;

namespace WovenMonopoly
{
    class Player
    {
        public string Name { get; }
        public int Position { get; set; } = 0;
        public bool IsFirstMove { get; set; } = true;
        public int Money { get; set; } = 16;
        public HashSet<string> OwnedProperties { get; } = new HashSet<string>();

        public Player(string name)
        {
            Name = name;
        }

        public void Move(int steps, List<BoardSpace> board)
        {
            //when everybody starts on GO
            if (IsFirstMove)
            {
                IsFirstMove = false; // skip Collect $1 when everybody starts on GO
                Position += steps;
            }
            else
            {
                if (Position == 0)
                {
                    StepForward(steps, board);
                    Money += 1; // when player is at GO now, will collect $1 when passing GO
                }
                else
                {
                    // when player is over GO after stepping
                    var isOverGo = Position + steps > board.Count;
                    if (isOverGo)
                    {
                        Money += 1;
                    }

                    StepForward(steps, board);
                }
            }
        }

        private void StepForward(int steps, List<BoardSpace> board)
        {
            Position = (Position + steps) % board.Count;
        }

        public void PayRent(Player owner, int rent)
        {
            Money -= rent;
            owner.Money += rent;
        }
    }
}