# Optima_OOP

A modular C# console application demonstrating **Layered Architecture**, **SOLID principles**, and **Design Patterns** (Strategy, Command, State, Facade, Composite, Observer).

## 📂 Functionality

* **RPG Mode**: Character creation, combat simulation, and state persistence (Memento).
* **Text Editor**: Structured document manipulation (Composite).
* **File Manager**: CLI-based file system navigation and editing (State).

## 🚀 Getting Started

**Requirement:** .NET 10.0 SDK

### Run Application
Navigate to the `GameModel` directory:

```bash
cd GameModel

# Interactive Mode (Select RPG or Text via menu)
dotnet run

# Launch directly into Text Editor
dotnet run -- --text

# Launch File Manager Mode
dotnet run -- --file-manager