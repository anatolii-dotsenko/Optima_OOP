# 🧙 Game Character System (Python OOP Project)

A small object-oriented game simulation written in Python.  
The system models characters, their abilities, items, and interactions such as attacks, healing, and equipping gear.

---

## 📂 Project Structure

```
game/
│
├── __init__.py
├── character.py     # Base class and subclasses (Warrior, Mage)
├── abilities.py     # Ability class and ability logic
├── items.py         # Item class for equipment and stat bonuses
└── main.py          # Demo script simulating interactions
```

---

## 🚀 Features

- Abstract base class **`Character`** defines shared attributes and behaviors.
- Subclasses **`Warrior`** and **`Mage`** implement unique abilities.
- **`Item`** class allows characters to equip objects that modify stats or grant abilities.
- **`Ability`** class represents powers that characters can use in combat.
- Fully documented with **English docstrings** using standard Python doc-comment format.
- Modular structure (each class in its own module).

---

## 🧩 Example Simulation

```bash
python -m game.main
```

**Output example:**
```
Thorin equipped Flaming Sword.
Elara equipped Amulet of Storms.

--- Battle Begins ---
Thorin attacks Elara for 12 damage! (68 HP left)
Elara uses ability: Lightning Strike!
Thorin takes 20 damage.
Thorin uses *Power Strike*! Deals 30 ignoring armor.
Elara casts *Fireball*! Thorin takes 25 damage.
Elara heals for 10 HP. (78 total)

--- Final Stats ---
Thorin: 75 HP
Elara: 78 HP
```

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
python src/main.py
```

## Examples
- Create a Warrior and a Mage character, simulate combat, and visualize the results.
- Manage inventories and equip items to characters.

## Contributing
Feel free to submit issues or pull requests to improve the project.