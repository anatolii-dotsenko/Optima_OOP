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
    .NET SDK: Ensure you have the .NET SDK installed (It was written with .NET 10.0 🤷‍♂️).
        `check with: dotnet --version`

**Installation**
    Clone the repository:
    Navigate to the project folder:
        cd GameModel
        dotnet run