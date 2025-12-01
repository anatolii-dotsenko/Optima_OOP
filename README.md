# 🐍 Optima OOP Project

A collection of object-oriented programming (OOP) simulations demonstrating core principles such as Inheritance, Polymorphism, Composition, and Abstraction.

The project is divided into two independent modules:
1. **Game Model** — A combat simulation with characters, items, and abilities.
2. **Text Abstraction** — A document structure system using composite elements.

---

## 📂 Project Structure

```text
Optima_OOP/
│
├── GameModel/           # Module 1: Game Character System
│   ├── character.py     # Base Character class and subclasses (Warrior, Mage)
│   ├── abilities.py     # Ability logic
│   ├── items.py         # Item class for equipment
│   └── main.py          # Demo script for battle simulation
│
├── TextAbstraction/     # Module 2: Document Builder System
│   ├── text.py          # Container class (Composite)
│   ├── elements.py      # Abstract base class and concrete text elements
│   └── main.py          # Demo script for document generation
│
├── requirements.txt     # Project dependencies
└── README.md            # Project documentation
```

---

## 🧰 Setup and Run

### 1️⃣ Install Python
Ensure you have **Python 3.10+** installed:
```bash
python --version
```

### 2️⃣ Clone or copy the project files
```bash
git clone https://github.com/anatolii-dotsenko/Optima_OOP.git
```

### 3️⃣ Install dependencies
```bash
pip install -r requirements.txt
```

## Usage
To run the game, execute the following command:
```
python module/main.py
```



## 🎮 Module 1: Game Character System. Features

- Abstract base class **`Character`** defines shared attributes and behaviors.
- Subclasses **`Warrior`** and **`Mage`** implement unique abilities.
- **`Item`** class allows characters to equip objects that modify stats or grant abilities.
- **`Ability`** class represents powers that characters can use in combat.
- Fully documented with **English docstrings** using standard Python doc-comment format.
- Modular structure (each class in its own module).

---

## 🧱 Class Overview

### `Character` (Abstract)
Represents a base character with common stats and methods:
- `attack(other)`
- `heal(amount)`
- `defend(amount)`
- `equip(item)`
- `use_ability(name, target)`
- `unique_ability(target)` (must be implemented by subclasses)

### `Warrior`
- High armor and attack.
- Unique ability: **Power Strike** (ignores armor).

### `Mage`
- Low armor, strong spells.
- Unique ability: **Fireball** (burns the enemy).

### `Item`
- Grants stat bonuses (`bonus_health`, `bonus_attack`, `bonus_armor`).
- Can optionally grant an `Ability`.

### `Ability`
- Represents a reusable power with a callable `effect`.

---

## 📝 Module 2: Text Abstraction System

A system for modeling and manipulating structured text documents. It focuses on **Composition** and **Polymorphism**.

### Features
- **Composite Pattern**: The `Text` class acts as a container for various elements.
- **Polymorphic Rendering**: Each element (`Heading`, `Paragraph`, etc.) knows how to render itself differently.
- **Dynamic Structure**: Add, remove, or reorder elements at runtime.
- **Table of Contents**: Automatically generates a hierarchy based on headings.

### Class Overview

#### `Text`
Main container that holds a list of elements.
- `add_element(element)`: Appends an element.
- `remove_element(index)`: Removes an element by position.
- `move_element(from, to)`: Reorders elements.
- `table_of_contents()`: Generates a TOC from headings.

#### `TextElement` (Abstract)
Base class for all document parts requiring a `render()` method.

#### `Heading`
Renders Markdown-style headers (e.g., `# Title`).

#### `Paragraph`
Standard text block wrapper.

#### `Link` / `Image`
Specialized elements for media and navigation.

---

## 🛠 Concepts Applied
- **OOP Principles**: Encapsulation, Inheritance, Polymorphism, Abstraction.
- **Design Patterns**: Composite (Text System), Strategy (Abilities).
- **Type Hinting**: Full Python typing support for better code quality.