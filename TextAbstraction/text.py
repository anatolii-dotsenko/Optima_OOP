"""
Module defining the Text container class.
"""

from typing import List, Optional
from elements import TextElement, Heading


class Text:
    """
    A composite class representing a document containing various text elements.

    Attributes:
        _elements (List[TextElement]): An ordered list of elements in the document.
    """

    def __init__(self):
        self._elements: List[TextElement] = []

    def add_element(self, element: TextElement) -> None:
        """Appends a new element to the end of the document."""
        self._elements.append(element)

    def remove_element(self, index: int) -> None:
        """
        Removes an element at a specific index.
        
        Args:
            index (int): The 0-based index of the element to remove.
        """
        if 0 <= index < len(self._elements):
            self._elements.pop(index)
        else:
            print(f"Error: Index {index} is out of bounds.")

    def move_element(self, from_index: int, to_index: int) -> None:
        """
        Moves an element from one position to another.
        """
        if not (0 <= from_index < len(self._elements)) or not (0 <= to_index < len(self._elements)):
            print("Error: Invalid indices for moving element.")
            return

        element = self._elements.pop(from_index)
        self._elements.insert(to_index, element)

    def table_of_contents(self) -> str:
        """
        Generates a table of contents based on Heading elements.
        Uses indentation to represent hierarchy.
        """
        lines = ["Table of Contents:"]
        for element in self._elements:
            if isinstance(element, Heading):
                # Indent 4 spaces per level minus 1 (Level 1 has 0 indent)
                indent = "    " * (element.level - 1)
                lines.append(f"{indent}- {element.text}")
        
        if len(lines) == 1:
            return "Table of Contents: (Empty)"
            
        return "\n".join(lines)

    def __str__(self) -> str:
        """
        Renders the entire document by combining rendered elements.
        Elements are separated by double newlines.
        """
        rendered_blocks = [element.render() for element in self._elements]
        return "\n\n".join(rendered_blocks)