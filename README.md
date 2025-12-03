# ⚔️ Optima OOP Combat Simulation

A C# console application designed to demonstrate core Object-Oriented Programming (OOP) principles and Design Patterns. The project simulates a turn-based combat system featuring characters with unique stats, abilities, and inventory management, alongside a structural text generation engine.

## 📋 Overview

This project models a game system where characters (Warriors, Mages) interact using a robust combat engine. It emphasizes clean architecture, separation of concerns, and extensibility. Additionally, it includes a document generation module to showcase structural design patterns.

### Key Features
* **OOP Principles:** Demonstrates Inheritance, Polymorphism, Encapsulation, and Composition.
* **Design Patterns:** * **Composite:** Used in the `Text` namespace to treat individual text elements (`Leaf`) and groups of elements (`Container`) uniformly.
  * **Builder/Factory:** The `TextFactory` simplifies the creation of complex hierarchical document structures.
  * **Strategy:** Implemented via `ICombatLogger` for interchangeable logging behaviors.
* **Character System:** Abstract base `Character` class with concrete implementations (`Warrior`, `Mage`).
* **Combat Logic:** A dedicated `CombatSystem` handles interactions (attacks, damage calculation) separately from data classes.
* **Inventory & Abilities:** Items can modify stats dynamically and grant new abilities (e.g., a `MagicAmulet` granting a `Fireball` spell).

## 📂 Project Structure

The solution is organized into logical namespaces and directories:

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
├── Text/               # Text Generation (Composite & Builder Patterns)
│   ├── IText.cs        # Component interface
│   ├── Container.cs    # Composite node
│   ├── Leaf.cs         # Leaf node
│   ├── TextFactory.cs  # Builder for document structures
│   └── ...
├── Game.cs             # Entry point (Simulation logic)
└── GameModel.csproj    # Project configuration
```

## 🚀 Getting Started
**Prerequisites**
* Ensure you have the .NET SDK installed (target is 10.0). Check with:
```text 
dotnet --version
```

**Installation**
* Clone the repository:
```text
 git clone https://github.com/anatolii-dotsenko/Optima_OOP/tree/main
 ```
* Navigate to the project directory:
```text
cd GameModel
dotnet run
```
 This will compile the project and execute the Game.cs entry point, displaying the battle simulation in the console.