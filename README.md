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
├── Core/                       # Core abstractions (Stable dependencies)
│   ├── Entities/               # Base classes (Character, Item, Ability)
│   ├── Stats/                  # Stat system (StatModifier, CharacterStats)
│   └── Interfaces/             # System-wide contracts (ILogger, ICommand)
├── Combat/                     # Combat Subsystem (The "Engine")
│   ├── Actions/                # Action definitions (CombatAction)
│   ├── Results/                # DTOs (AttackResult, HealResult)
│   ├── BattleManager.cs        # Flow Controller (Turn logic)
│   └── CombatSystem.cs         # Rules Engine (Calculations)
├── Content/                    # Concrete Game Data (Volatile implementations)
│   ├── Abilities/              # (Fireball, PowerStrike)
│   ├── Characters/             # (Warrior, Mage)
│   └── Items/                  # (Sword, Shield, Amulet)
├── Infrastructure/             # External concerns
│   ├── Logging/                # Loggers (Console, File, Composite)
│   └── Commands/               # CLI Command System (Registry, Base Commands)
├── Presentation/               # Output formatting & UI
│   ├── Text/                   # Text Generation (Composite Pattern)
│   └── Formatters/             # Message Formatters
└── GameEngine.cs               # Main Orchestrator (Facade)
```

## 🚀 Getting Started
**Ensure you have the .NET SDK installed (target is 10.0). Check with:**
```text 
dotnet --version
```

**Clone the repository**
```text
 git clone https://github.com/anatolii-dotsenko/Optima_OOP/tree/main
 ```
**Navigate to the project directory and run:**
```text
cd GameModel
dotnet run
```
 This will compile the project and execute the Game.cs entry point, displaying the battle simulation in the console.