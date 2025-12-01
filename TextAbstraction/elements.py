"""
Module defining the structural elements of a text document.
"""

from abc import ABC, abstractmethod


class TextElement(ABC):
    """
    Abstract base class for all text elements.
    """

    @abstractmethod
    def render(self) -> str:
        """Return the string representation of the element for the document body."""
        pass


class Heading(TextElement):
    """
    Represents a section heading.

    Attributes:
        text (str): The heading text.
        level (int): The hierarchy level (1 for main title, 2 for section, etc.).
    """

    def __init__(self, text: str, level: int = 1):
        if level < 1:
            raise ValueError("Heading level must be at least 1.")
        self.text = text
        self.level = level

    def render(self) -> str:
        # Renders as Markdown-style heading (e.g., "## Title")
        return f"{'#' * self.level} {self.text}"


class Paragraph(TextElement):
    """
    Represents a standard block of text.

    Attributes:
        content (str): The text content of the paragraph.
    """

    def __init__(self, content: str):
        self.content = content

    def render(self) -> str:
        return self.content


class Link(TextElement):
    """
    Represents a hyperlink.

    Attributes:
        text (str): The clickable text.
        url (str): The destination URL.
    """

    def __init__(self, text: str, url: str):
        self.text = text
        self.url = url

    def render(self) -> str:
        # Renders in standard format: [text](url)
        return f"[{self.text}]({self.url})"


class Image(TextElement):
    """
    Represents an image with alt text.

    Attributes:
        alt_text (str): Description of the image.
        source (str): Path or URL to the image source.
    """

    def __init__(self, alt_text: str, source: str):
        self.alt_text = alt_text
        self.source = source

    def render(self) -> str:
        # Renders as: ![alt_text](source)
        return f"![{self.alt_text}]({self.source})"