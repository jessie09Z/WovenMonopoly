namespace WovenMonopoly
{
    public class BoardSpace
    {
        public BoardSpace(string name, string type, int price, string colour)
        {
            Name = name;
            Type = type;
            Price = price;
            Colour = colour;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string Colour { get; set; }
    }
}