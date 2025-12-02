# ⚔️ Optima OOP Combat Simulation

A C# console application designed to demonstrate core Object-Oriented Programming (OOP) principles. The project simulates a turn-based combat system featuring characters with unique stats, abilities, and inventory management.

## 📋 Overview

This project models a game system where characters (Warriors, Mages) interact using a robust combat engine. It emphasizes clean architecture, separation of concerns, and extensibility.

### Key Features
* **OOP Principles:** Demonstrates Inheritance, Polymorphism, Encapsulation, and Composition.
* **Character System:** Abstract base `Character` class with concrete implementations (`Warrior`, `Mage`).
* **Combat Logic:** A dedicated `CombatSystem` handles interactions (attacks, damage calculation) separately from data classes.
* **Inventory & Abilities:** Items can modify stats dynamically and grant new abilities (e.g., a `MagicAmulet` granting a `Fireball` spell).
* **Logging:** Flexible `ICombatLogger` interface for outputting battle events.

## 📂 Project Structure

The solution is organized into logical namespaces and folders:

```text
GameModel/
├── Abilities/          # Ability logic (Spell, Melee attacks)
│   ├── Ability.cs      # Abstract base class
│   ├── Fireball.cs
│   └── PowerStrike.cs
├── Characters/         # Character definitions
│   ├── Character.cs    # Abstract base class
│   ├── Mage.cs
│   └── Warrior.cs
├── Combat/             # Combat engine
│   └── CombatSystem.cs
├── Items/              # Equipment and Inventory
│   ├── Item.cs         # Abstract base class
│   ├── Shield.cs
│   ├── Sword.cs
│   └── MagicAmulet.cs
├── Logging/            # Output handling
│   ├── ICombatLogger.cs
│   └── ConsoleLogger.cs
├── Program.cs          # Entry point (Simulation logic)
└── GameModel.csproj    # Project configuration
```

## 🚀 Getting Started
**Prerequisites**
* .NET SDK: Ensure you have the .NET SDK installed.
* Check with: 
```text 
dotnet --version
```

**Installation**
Clone the repository:
```text
 git clone https://github.com/anatolii-dotsenko/Optima_OOP/tree/main
 ```
Navigate to the project folder:
```text
cd GameModel
dotnet run
```
 This will compile the project and execute the Program.cs entry point, displaying the battle simulation in the console.

 ## 🛠️ Usage Example
 The Program.cs typically initializes the simulation like this:
```text
 // 1. Setup Logger and Combat System
ICombatLogger logger = new ConsoleLogger();
CombatSystem combat = new CombatSystem(logger);

// 2. Create Characters
Warrior hero = new Warrior("Aragorn");
Mage villain = new Mage("Saruman");

// 3. Equip Items
hero.EquipItem(new Sword());
villain.EquipItem(new MagicAmulet());

// 4. Fight!
combat.Attack(hero, villain);
combat.UseAbility(villain, hero, "Fireball");
```