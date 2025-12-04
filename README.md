````markdown
# Optima_OOP

A modular C# console application demonstrating **Layered Architecture**, **SOLID principles**, and **Design Patterns** (Command, Strategy, Composite, Factory).

## 📂 Structure

The solution is divided into two main projects:

* **`GameModel`**: The core application logic.
    * **Core**: Pure domain entities (`Character`, `Item`) and interfaces.
    * **Content**: Game assets (Warriors, Mages, Weapons, Abilities).
    * **Systems**: Logic for Combat and interactions.
    * **Text**: A composite-pattern text editor engine.
    * **Infrastructure**: CLI engine, File I/O, Logging, and Persistence.
* **`Tests`**: Unit tests utilizing **xUnit** and **Moq**.

## 🚀 Getting Started

**Prerequisites:** .NET 10.0 SDK

### Run the App
```bash
cd GameModel
dotnet run
````

*Follow the on-screen prompt to select **Text** or **Characters** mode.*

### Run Tests

```bash
dotnet test
```

## 🎮 CLI Commands

### Character Mode (RPG)

  * `create <char|item>` - Create new entities interactively.
  * `ls <char|item> [--id <name>]` - List all objects or inspect specific ones.
  * `add --char_id <name> --id <item>` - Equip an item to a character.
  * `act <attack|heal|ability> <actor> <target>` - Perform combat actions.
  * `save` / `load` - Persist world state to JSON.

### Text Mode (Editor)

  * `add <heading|paragraph>` - Add content to the document.
  * `print [--whole]` - Display the current section or full document.
  * `cd <path>` / `up` - Navigate the document tree.
  * `rm <name>` - Remove elements.

<!-- end list -->

```
```