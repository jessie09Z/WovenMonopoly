# Woven Monopoly


## 📖 Overview
**Woven Monopoly** is a console-based implementation of a Monopoly-style board game. It includes core features such as player management, property transactions, and turn-based gameplay. In Woven Monopoly, when the dice rolls are set ahead of time, the game is deterministic. You can run Program to see the test outcome for **rolls_1.json** and **rolls_2.json** seperately


## 📦 Installation and Running

1. **Clone the repository:**
   ```bash
   git clone https://github.com/jessie09Z/WovenMonopoly.git
   cd dir/WovenMonopoly/bin/Debug

2. **Install Microsoft .NET Framework 3.5**
   ```bash
   This will be auto-installed when running console app. If already Installed, skip this step
   
3. **Running Console App**
   ```bash
   Run WovenMonopoly.exe under WovenMonopoly\bin\Debug

4. **Console Outcomes**
   ```bash
    In rolls 1: Charlotte is bankrupt!
    In rolls 1: The winner is Peter with $40 left!
    In rolls 1: Sweedal has $1 and is on position 7
    In rolls 1: Peter has $40 and is on position 8
    In rolls 1: Billy has $13 and is on position 0
    
    
    
    In rolls 2: Sweedal is bankrupt!
    In rolls 2: The winner is Charlotte with $30 left!
    In rolls 2: Peter has $5 and is on position 4
    In rolls 2: Billy has $20 and is on position 2
    In rolls 2: Charlotte has $30 and is on position 0


---

## Application Design Overview

### 1. Program Entry (`Program.cs`)
- **Purpose:**  
  The entry point of the application, responsible for initializing and running the game.
- **Key Functions:**
  - Initializes the game board and players.
  - Starts the main game loop.

---

### 2. Game Logic (`MonopolyGame.cs`)
- **Purpose:**  
  This class orchestrates the overall game flow, managing turns, player actions, and board interactions.
- **Key Responsibilities:**
  - Handling player turns (rolling dice, moving).
  - Managing transactions such as buying properties and paying rent.
  - Implementing game rules such as passing "Go" and collecting salary.

---

### 3. Player Management (`Player.cs`)
- **Purpose:**  
  Represents individual players in the game.
- **Attributes:**
  - `Name` – Player's name.
  - `Money` – Tracks the player's current balance.
  - `Position` – Current position on the board.
  - `PropertiesOwned` – List of owned properties.
- **Main Methods:**
  - `Move(int steps)` – Moves the player around the board.
  - `BuyProperty(BoardSpace property)` – Handles property purchases.
  - `PayRent(int amount)` – Deducts rent from the player's balance.

---

### 4. Board Spaces (`BoardSpace.cs`)
- **Purpose:**  
  Represents spaces on the game board, such as properties, chance spaces, and special locations.
- **Attributes:**
  - `Name` – Name of the space.
  - `Type` – Property type
  - `Price` – Purchase cost (for properties) or Rent value (if applicable).
  - `Color` – Color of a property.

---

### 5. Unit Tests (`UnitTests.cs`)
- **Purpose:**  
  Contains test cases to validate core functionalities and game rules.
- **Key Areas Tested:**
  - Player movement and property purchase.
  - Rent/Buying payment mechanics.
  - Special spaces handling.

---

## Suggestions for Design Improvement (time limited, so skip the implementation)

### 1. Design Enhancements
- **Use Dependency Injection:**  
  - Introduce dependency injection with Autofac IoC pattern to decouple components, making the game more testable and maintainable.
Example: Inject BoardSpace, Player, and MonopolyGame services instead of directly instantiating them within the game class.

