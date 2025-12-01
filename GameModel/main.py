"""Main simulation file demonstrating character interactions."""

from character import Warrior, Mage
from items import Item
from abilities import Ability


def main():
    # Create characters
    warrior = Warrior("Thorin")
    mage = Mage("Elara")

    # Define abilities
    lightning_strike = Ability(
        "Lightning Strike",
        lambda user, target: target.take_damage(20)
    )

    # Create items
    sword = Item("Flaming Sword", bonus_attack=5)
    amulet = Item("Amulet of Storms", granted_ability=lightning_strike)

    # Equip items
    warrior.equip(sword)
    mage.equip(amulet)

    # Start simulation
    print("\n--- Battle Begins ---")
    warrior.attack(mage)
    mage.use_ability("Lightning Strike", warrior)
    warrior.unique_ability(mage)
    mage.unique_ability(warrior)
    mage.heal(10)

    print("\n--- Final Stats ---")
    print(f"{warrior.name}: {warrior.health} HP")
    print(f"{mage.name}: {mage.health} HP")


if __name__ == "__main__":
    main()
