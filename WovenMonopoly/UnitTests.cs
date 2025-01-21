using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace WovenMonopoly
{
    [TestFixture]
    public class UnitTests
    {
        private MonopolyGame _game;

        [SetUp]
        public void Init()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            var board = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\board.json");
            var rolls = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                @"Data\rolls_1.json");
            _game = new MonopolyGame(board, rolls);
        }
        
        [Test]
        public void BoardSpace_Should_Initialize_Correctly()
        {
            var space = new BoardSpace("Park Place", "Property", 350, "Red");
            Assert.AreEqual("Park Place", space.Name);
            Assert.AreEqual("Property", space.Type);
            Assert.AreEqual(350, space.Price);
            Assert.AreEqual("Red", space.Colour);
        }

        [Test]
        public void Player_Should_Move_Correctly()
        {
            var player = new Player("Alice");
            player.Move(5, _game.Board);
            Assert.AreEqual(5, player.Position);
        }

        [Test]
        public void Each_Player_Starts_With_16()
        {
            var player = new Player("Alice");
            Assert.AreEqual(16, player.Money);
        }

        [Test]
        public void Everybody_Starts_on_GO()
        {
            var player = new Player("Alice");
            Assert.AreEqual(0, player.Position);
        }

        [Test]
        public void Player_Should_Handle_Passing_Go_when_first_passing_without_money_but_next_round_with_money()
        {
            var player = new Player("Bob");

            player.Move(5, _game.Board);
            Assert.AreEqual(5, player.Position);
            Assert.AreEqual(16, player.Money);
            player.Move(5, _game.Board);
            Assert.AreEqual(1, player.Position);
            Assert.AreEqual(17, player.Money);
        }

        [Test]
        public void Player_Should_Buy_Property_When_Land()
        {
            var player = new Player("Charlie");
            Assert.AreEqual(16, player.Money);
            Assert.AreEqual(0, _game.PropertyOwners.Count);
            _game.HandleProperty(player, _game.Board[1]);
            Assert.AreEqual(15, player.Money);
            Assert.AreEqual(_game.Board[1].Name, player.OwnedProperties.SingleOrDefault());
            Assert.AreEqual(1, _game.PropertyOwners.Count);
            _game.PropertyOwners.TryGetValue(_game.Board[1].Name, out var owner1);
            Assert.AreEqual(player.Name, owner1?.Name);
            //when bought, will skip
            _game.HandleProperty(player, _game.Board[1]);
            Assert.AreEqual(15, player.Money);
            Assert.AreEqual(1, _game.PropertyOwners.Count);
            _game.PropertyOwners.TryGetValue(_game.Board[1].Name, out var owner2);
            Assert.AreEqual(player.Name, owner2?.Name);
        }
        
        [Test]
        public void Player_Must_Pay_Rent_to_Owner_If_Same_Color_Double_Paid()
        {
            //If you land on an owned property, you must pay rent to the owner
            var owner = new Player("Charlie");
            var tenant = new Player("Bob");
            
            _game.HandleProperty(owner, _game.Board[1]);
            _game.HandleProperty(tenant, _game.Board[1]);
            Assert.AreEqual(16, owner.Money);
            Assert.AreEqual(15, tenant.Money);
            
            Assert.AreEqual(1, _game.PropertyOwners.Count);
            _game.PropertyOwners.TryGetValue(_game.Board[1].Name, out var owner1);
            Assert.AreEqual(owner.Name, owner1?.Name);
            Assert.AreEqual(_game.Board[1].Name, owner.OwnedProperties.SingleOrDefault());
            
            //If the same owner owns all property of the same colour, the rent is doubled
            _game.HandleProperty(owner, _game.Board[2]);
            Assert.AreEqual(15, owner.Money);
            _game.HandleProperty(tenant, _game.Board[1]);
            Assert.AreEqual(17, owner.Money);
            Assert.AreEqual(13, tenant.Money);
        }
    }
}