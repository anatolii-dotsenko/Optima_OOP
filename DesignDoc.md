Okay, I will merge the information, format it into a cohesive Markdown Design Document, and check it against the requirements provided.

### Check against Requirements:

1.  **Structure Description (Design Document):**
    * *Introduction & Architecture:* Covered in Sections 1 & 2.
    * *Detailed CLI Description:* Covered in Section 3 (Design Patterns & Data Flow).
2.  **Console Interface Implementation (Topics):**
    * *Topic 1 (Character Catalog):*
        * *List characters:* Covered by `ls` command (Browsing).
        * *General Info:* Covered by `ls --id` (Browsing).
        * *Create Character:* Covered by `create char` (Creation).
        * *Save Data:* Covered by `save` command (Persistence).
    * *Topic 2 (Text Game):*
        * *Choose Characters:* Implied by `ls` and interaction commands.
        * *Start Game:* The application starts in RPG mode by default or via argument.
        * *Game Loop Actions:* Covered by `act` command (Combat Simulation).
        * *Save State:* Covered by `save` command (Persistence).
    * *Topic 3 (Custom - Text Editor/File Manager):*
        * *Operations:* Covered by file navigation (`ls`, `cd`, `open`) and editing (`add`, `rm`, `save`).
        * *Info Display:* Covered by `print` command.

The content covers all necessary points. Here is the final formatted Design Document.

***

# Design Document: Optima OOP

## 1. Introduction

**Optima OOP** is a modular console application designed to demonstrate clean architecture principles and advanced object-oriented design patterns. It serves as a dual-purpose platform, functioning both as a Role-Playing Game (RPG) simulation environment and a structured Text File Editor. This duality showcases the system's capability for context switching, modularity, and extensibility without compromising core logic.

## 2. System Architecture

The application adheres to a strict **Layered Architecture**, promoting separation of concerns and loose coupling between components.

### Main Layers

1.  **Core Layer (Business Logic)**
    * **Responsibility:** Encapsulates the fundamental business rules and domain entities.
    * **Components:** Domain entities (`Character`, `Item`, `Ability`), Value Objects (`Stats`), and Domain Services.
    * **Dependencies:** Defines interfaces (contracts) for infrastructure but has *no* external dependencies.

2.  **Infrastructure Layer**
    * **Responsibility:** Implements concrete mechanisms for external interaction and data handling.
    * **Components:**
        * **CLI:** Handles command parsing, input strategies, and rendering logic.
        * **IO/Persistence:** Manages filesystem access (`RealFileSystem`) and data serialization via JSON.
        * **Network:** HTTP clients for communicating with external APIs (`GenshinApiService`).

3.  **Systems Layer**
    * **Responsibility:** Orchestrates interactions between domain objects to fulfill complex business processes.
    * **Components:** `CombatSystem` (manages battle logic), `BattleManager`.

4.  **Content Layer**
    * **Responsibility:** Provides specific game assets and configurations.
    * **Components:** Concrete implementations of entities (`Warrior`, `Mage`, `Fireball`, `Sword`).

---

## 3. Console Interface (CLI) Implementation

The CLI module is the primary presentation layer. It is architected to be highly extensible, allowing for seamless switching between different operational modes (RPG vs. Text Editor).

### 3.1 Design Patterns

#### **Facade**
The `Cli` class acts as the single entry point for the user interface. It abstracts away the complexities of the internal subsystem (argument parsing, strategy selection, rendering) and provides a simple interface for the application root (`RunLoop()`, `ExecCommand()`).

#### **Strategy**
To support multiple distinct modes of operation, the **Strategy** pattern is employed:
* **Interface:** `ICommandStrategy` defines the contract for executing commands.
* **Implementations:**
    * `RpgStrategy`: Handles game-related commands (`act`, `equip`, `ls`, `create`).
    * `TextStrategy`: Handles text-editing commands (`add`, `print`, `cd`, `rm`).
This allows the application to dynamically swap its entire behavior set at runtime or startup based on user intent.

#### **Command**
Every user action is encapsulated into its own class implementing the `ICommand` interface (`Keyword`, `Description`, `Execute`).
* **Benefits:** Decouples the object that invokes the operation from the one that knows how to perform it. It simplifies the addition of new commands (Open/Closed Principle) and standardizes argument handling.
* **Examples:** `CreateCommand`, `SaveCommand`, `NetCommand`.

#### **Double Dispatch / Visitor (Rendering)**
Rendering logic is decoupled from domain data using a variation of the Visitor pattern (Double Dispatch):
* **Mechanism:** Domain objects implement `IRenderable<T>` and accept a renderer via `UseRenderer(IRenderer<T> renderer)`. The object then passes its data (DTO) back to the renderer.
* **Benefit:** The `Character` class does not need to know about `System.Console`. The `ConsoleRenderer` handles all formatting (colors, indentation, tables), making the system adaptable to other outputs (e.g., File, GUI) in the future.

### 3.2 Data Flow

1.  **Input:** User enters a command string (e.g., `act attack Thorin Orc`).
2.  **Parser:** The `ArgParser` component breaks the string into the command key (`act`), arguments, and flags.
3.  **Routing:** The `Cli` facade forwards the parsed data to the currently active `ICommandStrategy`.
4.  **Execution:** The strategy resolves the appropriate `ICommand` implementation and calls `Execute()`.
5.  **Logic:** The command interacts with the **Core** or **Systems** layer (e.g., calling `CombatSystem.Attack`).
6.  **Output:** The resulting state or feedback is passed to the `ConsoleRenderer` to be displayed to the user.

---

## 4. Functionality (User Stories)

The application supports two distinct functional domains based on the active Strategy.

### Topic 1: Character Catalog (RPG Mode)

* **Browsing:**
    * Users can list all entities (characters and items) in the current game session using the `ls` command.
    * Detailed attributes (Health, Attack Power, Armor, Abilities) can be inspected using `ls --id <Name>`.
* **Creation:**
    * The `create <type>` command launches an interactive wizard that guides the user through creating new Characters (Warrior/Mage) or Items.
* **Equipment:**
    * Users can modify a character's state by equipping items from the pool using `add --char_id <Name> --id <ItemName>`. This dynamically alters stats via the Decorator-like behavior in `Character.GetStats()`.
* **Combat Simulation:**
    * The `act <action> <actor> <target>` command triggers the `CombatSystem`. It calculates damage based on stats and abilities, logging the results to the console via the Observer pattern.
* **Integration:**
    * The `net import <name>` command connects to the external **Genshin Impact API** to fetch real-world character data and map it to internal game entities.
* **Persistence:**
    * The entire world state is serialized to JSON using `save` and restored using `load`. The **Memento** pattern is used to ensure internal object encapsulation is not violated during this process.

### Topic 2: File Manager & Text Editor (Text Mode)

* **File System Navigation:**
    * **Directory Mode:** Users can view the contents of the current directory using `ls` and navigate the file system hierarchy using `cd <path>`.
    * **File Access:** Users can open text files using `open <filename>` to enter a read-only View Mode.
* **Structured Editing:**
    * **Document Tree:** When editing, the file content is parsed into a structured Composite tree (`Root` -> `Heading` -> `Paragraph`).
    * **Content Manipulation:** Users can append new structural elements using `add heading` or `add paragraph`, and remove existing ones using `rm`.
    * **Tree Navigation:** Users can traverse the internal document structure using `cd` (down) and `up` to modify specific sections nested within headings.
* **Persistence:**
    * Changes made to the document structure in memory can be written back to the physical file on the disk using the `save` command.