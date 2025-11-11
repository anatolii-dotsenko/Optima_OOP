"""Module defining the base Character class and its subclasses."""

from abc import ABC, abstractmethod
from .abilities import Ability
from .items import Item


class Character(ABC):
    """
    Base class for all characters in the game.

    Attributes:
        name (str): Character name.
        health (int): Current health points.
        armor (int): Current armor points.
        attack_power (int): Base attack power.
        inventory (list[Item]): List of equipped items.
        abilities (list[Ability]): List of abilities the character can use.
    """

    def __init__(self, name: str, health: int, armor: int, attack_power: int):
        self.name = name
        self.health = health
        self.armor = armor
        self.attack_power = attack_power
        self.inventory: list[Item] = []
        self.abilities: list[Ability] = []

    def is_alive(self) -> bool:
        """Check if the character is alive."""
        return self.health > 0

    def attack(self, other: "Character"):
        """
        Attack another character.

        Args:
            other (Character): The target of the attack.
        """
        if not self.is_alive():
            print(f"{self.name} is dead and cannot attack.")
            return

        damage = max(0, self.attack_power - other.armor)
        other.health -= damage
        print(f"{self.name} attacks {other.name} for {damage} damage! ({other.health} HP left)")

    def heal(self, amount: int):
        """Heal the character by a given amount."""
        self.health += amount
        print(f"{self.name} heals for {amount} HP. ({self.health} total)")

    def defend(self, amount: int):
        """Temporarily increase armor."""
        self.armor += amount
        print(f"{self.name} raises armor by {amount}. (Armor = {self.armor})")

    def equip(self, item: Item):
        """
        Equip an item that modifies stats or grants an ability.
        """
        self.inventory.append(item)
        item.apply(self)
        print(f"{self.name} equipped {item.name}.")

    def use_ability(self, ability_name: str, target: "Character"):
        """
        Use an ability on a target.
        """
        for ability in self.abilities:
            if ability.name == ability_name:
                ability.use(self, target)
                return
        print(f"{self.name} does not have the ability '{ability_name}'.")

    @abstractmethod
    def unique_ability(self, target: "Character"):
        """Each subclass must implement a unique ability."""
        pass


class Warrior(Character):
    """Strong melee fighter with high armor."""

    def __init__(self, name: str):
        super().__init__(name, health=120, armor=10, attack_power=15)

    def unique_ability(self, target: "Character"):
        """Powerful strike ignoring armor."""
        damage = self.attack_power * 2
        target.health -= damage
        print(f"{self.name} uses *Power Strike*! Deals {damage} ignoring armor.")


class Mage(Character):
    """Magic user with low armor but strong spells."""

    def __init__(self, name: str):
        super().__init__(name, health=80, armor=3, attack_power=10)

    def unique_ability(self, target: "Character"):
        """Cast a fireball that burns the enemy."""
        damage = 25
        target.health -= damage
        print(f"{self.name} casts *Fireball*! {target.name} takes {damage} damage.")
