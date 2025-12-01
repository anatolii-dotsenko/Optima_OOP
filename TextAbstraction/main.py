"""
Main script demonstrating the Text Abstraction system.
"""

from text import Text
from elements import Heading, Paragraph, Link, Image


def main():
    # 1. Create the document container
    article = Text()

    # 2. Add various elements
    article.add_element(Heading("Object-Oriented Programming", level=1))
    
    article.add_element(Paragraph(
        "OOP is a programming paradigm based on the concept of 'objects', "
        "which can contain data and code."
    ))

    article.add_element(Heading("Core Concepts", level=2))
    
    article.add_element(Paragraph(
        "There are four main pillars of OOP:"
    ))
    
    # Adding a wrong element to demonstrate removal/reordering later
    wrong_paragraph = Paragraph("This paragraph is out of place and should be moved or removed.")
    article.add_element(wrong_paragraph)

    article.add_element(Heading("Encapsulation", level=3))
    article.add_element(Paragraph("Bundling data and methods that work on that data within one unit."))

    article.add_element(Heading("Inheritance", level=3))
    article.add_element(Paragraph("Deriving new classes from existing ones."))
    
    article.add_element(Image("UML Diagram", "https://example.com/uml.png"))
    article.add_element(Link("Learn more on Wikipedia", "https://en.wikipedia.org/wiki/Object-oriented_programming"))

    # 3. Modify structure (Demonstrate requirements)
    print(">>> Original Table of Contents:")
    print(article.table_of_contents())
    print("-" * 40)

    # Move the "Inheritance" section (index 6 and 7) up before "Encapsulation" (index 4)
    # Note: Logic in a real app might be more complex to move whole blocks, 
    # here we just move individual elements for demo.
    
    print(">>> Removing the wrong paragraph...")
    # The wrong paragraph is at index 4 (0:H1, 1:P, 2:H2, 3:P, 4:Wrong)
    article.remove_element(4)

    # 4. Final Output
    print("\n" + "="*20 + " RENDERED ARTICLE " + "="*20 + "\n")
    print(article)
    
    print("\n" + "="*20 + " FINAL CONTENTS " + "="*20 + "\n")
    print(article.table_of_contents())


if __name__ == "__main__":
    main()