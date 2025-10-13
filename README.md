# 🛡️ GridFantasy

This project is dedicated to creating the turn-based SRPG, **GridFantasy**, and its complete development toolkit.

The goal is to build a fully data-driven system where a game can be created and managed using a dedicated editor (`GfStudio`) without modifying the core engine (`GfEngine`). This project was inspired by creation tools like RPG Maker, aiming to separate the game's "engine" from its "content."

---
## ✨ Key Features

* **`GfEngine`:** The core runtime engine built on .NET that runs the game.
* **`GfStudio`:** A web-based editor built with Blazor WebAssembly for visually managing all game content.
* **Data-Driven Design:** All game elements (characters, items, traits, maps, story) are managed as external **JSON files**.
* **Party Level System:** The entire party shares experience and levels up together (inspired by *Heroes of the Storm*).
* **Deterministic Growth System:** Character growth is predictable, based on a combination of a unit's base growth rate and a player-chosen "Crest" (Move Type).
* **Deep Trait System:**
    * Themed trait choices are presented at set levels.
    * A rarity system with weighted randomness adds excitement.
    * Seeded randomness prevents save/load scumming.

---
## 🏗️ Project Architecture

This project is managed as a single solution (`GridFantasy.sln`) containing three core projects that work together, much like a Lego set.

* **`GfEngine.Shared` (The Bricks):**
    * **Role:** A class library containing all the common "Lego bricks."
    * **Contents:** All data models (`Actor`, `Item`, `Trait`), enums, and core data structures.

* **`GfEngine` (The Player):**
    * **Role:** The runtime engine that acts as the "Lego spaceship." It reads the JSON data files and runs the game.
    * **Technology:** .NET Console Application.

* **`GfStudio` (The Design Studio):**
    * **Role:** The tool used to create all the content, like a "Lego design studio." It creates and edits the JSON data files.
    * **Technology:** Blazor WebAssembly.

**[Data Flow]**
> **GfStudio (Editor) 🎨** ➡️ **Game Data (JSON) 📄** ➡️ **GfEngine (Runtime) ▶️**

---
## 🚀 Future Goals

1.  Complete the core features of the `GfEngine`.
2.  Develop the `GfStudio` editor using Blazor.
3.  Implement a scripting system (like Lua) for advanced event handling.
4.  Create the full "Grid Fantasy" campaign using the completed toolkit.