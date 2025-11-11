"""Module defining abilities that characters can use."""

class Ability:
    """
    Represents a reusable skill or power.

    Attributes:
        name (str): The name of the ability.
        effect (callable): Function defining the ability's effect.
    """

    def __init__(self, name: str, effect):
        self.name = name
        self.effect = effect

    def use(self, user, target):
        """Apply the ability's effect."""
        print(f"{user.name} uses ability: {self.name}!")
        self.effect(user, target)
