# GridFantasy-Engine (Personal Project)

A personal project to learn C# and practice game engine design by building a grid-based tactical RPG.

## 🎯 My Learning Goals

This project was started to practice and understand several key programming concepts:

-   Object-Oriented Programming (OOP) in C#.
-   Implementing complex game logic from scratch.
-   Creating a data-driven design where game content (data) is separate from game rules (logic).
-   Getting comfortable with Git and GitHub for version control.

## 📝 Design Memos & Key Features

Here are some notes on the current design and features I've implemented:

-   **Data-Driven Design:** The static `GameData` class holds all definitions for units, weapons, and patterns. This makes it easy to add new content without changing the core engine code.
-   **Chess-like Mechanics:** The movement and attack system is based on a flexible `Behavior` system that can replicate patterns from chess (King, Knight, etc.).
-   **Pvector Logic:** Implemented a unique "cannon" mechanic that requires jumping over another piece, inspired by Janggi.

## 🚀 How to Run

1.  Clone this repository.
2.  Open the project in Visual Studio or VS Code.
3.  Build and run.

## 🗺️ Next Steps

- [ ] Implement a basic AI for enemy turns.
- [ ] Add a skill system.
- [ ] Design win/loss conditions for the stage.
