"""Module defining items that can modify character stats or grant abilities."""

from .abilities import Ability


class Item:
    """
    Represents an equippable item.

    Attributes:
        name (str): Item name.
        bonus_health (int): Health bonus.
        bonus_attack (int): Attack power bonus.
        bonus_armor (int): Armor bonus.
        granted_ability (Ability): Ability granted by the item (optional).
    """

    def __init__(self, name: str, bonus_health=0, bonus_attack=0, bonus_armor=0, granted_ability=None):
        self.name = name
        self.bonus_health = bonus_health
        self.bonus_attack = bonus_attack
        self.bonus_armor = bonus_armor
        self.granted_ability = granted_ability

    def apply(self, character):
        """Apply item bonuses and abilities to the character."""
        character.health += self.bonus_health
        character.attack_power += self.bonus_attack
        character.armor += self.bonus_armor

        if self.granted_ability:
            character.abilities.append(self.granted_ability)
