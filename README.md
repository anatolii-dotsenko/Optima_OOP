# Optima_OOP

A modular C# console application showcasing **Layered Architecture**, **SOLID**, and classic **Design Patterns** (Command, Strategy, Composite, Factory).

## 📂 Project Overview

The solution consists of two core projects:

### **`GameModel`**
This is the application’s backbone and includes:

- **Core** — Domain entities and contracts (`Character`, `Item`, interfaces).
- **Content** — Game-specific classes (Warriors, Mages, Weapons, Abilities).
- **Systems** — Combat logic and interaction mechanics.
- **Text** — A composite-based text document engine.
- **Infrastructure** — CLI engine, persistence layer, file I/O, and logging.

### **`Tests`**
Automated test suite built with **xUnit** and **Moq**.

## 🚀 Getting Started

**Requirement:** .NET 10.0 SDK

### Launch the Application
```bash
cd GameModel
dotnet run
```

Choose either **Text mode** or **Character mode** from the CLI menu.

### Execute Tests
```bash
dotnet test
```

## 🎮 CLI Reference

### Character Mode (RPG Toolkit)

- `create <char|item>` — Interactively create characters or items.  
- `ls <char|item> [--id <name>]` — List entities or inspect one by ID.  
- `add --char_id <name> --id <item>` — Equip an item to a character.  
- `act <attack|heal|ability> <actor> <target>` — Execute combat actions.  
- `save` / `load` — Store or restore the game state (JSON).

### Text Mode (Document Editor)

- `add <heading|paragraph>` — Insert text elements.  
- `print [--whole]` — Show the current section or the full document.  
- `cd <path>` / `up` — Navigate through the document structure.  
- `rm <name>` — Remove a node from the document tree.

