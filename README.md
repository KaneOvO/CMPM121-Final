# CMPM121-Final
# Devlog Entry - [11/20]

## Introducing the team

Tools Lead: Zhuo Chen

Engine Lead: Lizhuoyuan Wan

Design Lead: Zexuan Li

Coding Lead: Xing Zhong



## Tools and materials
**Tell us about what engines, libraries, frameworks, and or platforms you intend to use, and give us a tiny bit of detail about why your team chose those.**

We plan to use Unity for our game, and the engines we use include, but are not limited to, the Unity engine. The reason why we choose unity as our game engine is that we are learning to use the unity engine, so we need to use it more to verify the knowledge we have learned about unity. We believe that after the end of this project, our proficiency in using unity will be further improved.



**Tell us programming languages (e.g. TypeScript) and data languages (e.g. JSON) you team expects to use and why you chose them. Presumably you’ll just be using the languages expected by your previously chosen engine/platform.**

Because of we use unity engine, based on this choice, our programming language will be C# language. Our data language will be JSON, because unity has mature support for the JSON data format.



**Tell us about which tools you expect to use in the process of authoring your project. You might name the IDE for writing code, the image editor for creating visual assets, or the 3D editor you will use for building your scene. Again, briefly tell us why you made these choices. Maybe one of your teammates feels especially skilled in that tool or it represents something you all want to learn about.**

We will develop our game using tools such as Unity Hub and VS Code, and the Unity engine will be version 2022.3.10f. The reason we choose unity is that unity has a mature development process and rich support. Once we encounter a problem, we can quickly find a solution.



## Outlook
**What do you anticipate being the hardest or riskiest part of the project?**

We anticipate that the most difficult part of the project will be building our game scene entirely through code, and the functionality of undo and redo.



**What are you hoping to learn by approaching the project with the tools and materials you selected above?**

We hope to use this project to practice the various design patterns we learned in class, become more familiar with the use of the unity engine, and improve our team collaboration capabilities.



# Devlog Entry - [F0]

## How we satisfied the software requirements

- [F0.a] You control a character moving on a 2D grid.
  - Made using a pathfinding algorithm, when the player clicks on a movable target on the map, the player will move to the target location.
- [F0.b] You advance time in the turn-based simulation manually.
  - Click the button in the lower left corner to advance the number of rounds.
- [F0.c] You can reap (gather) or sow (plant) plants on the grid when your character is near them.
  - Drag the seed package onto the land to plant the corresponding plant. After the plants mature, click on a piece of land and then click the sow button in the lower right corner to harvest the plants grown on the current land.
- [F0.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
  - Click on a piece of land to view the sunlight value and water value on the current land. The water value will accumulate, and the sunlight value will be refreshed every round.
- [F0.e] Each plant on the grid has a type (e.g. one of 3 species) and a growth level (e.g. “level 1”, “level 2”, “level 3”).
  - There are three types of plants in the game, carrots, cabbage and onions. Each plant has three stages and corresponding different textures.
- [F0.f] Simple spatial rules govern plant growth based on sun, water, and nearby plants (growth is unlocked by satisfying conditions).
  - A simple condition detection, when the set conditions are met, the plant grows to the next stage.
- [F0.g] A play scenario is completed when some condition is satisfied (e.g. at least X plants at growth level Y or above).
  - When the player has 5 of all three types of plants, the Game over text is displayed.

## Reflection

  During the production process of F0, the biggest problem we encountered was the problem of the unity engine. First of all, when submitting through git, because Unity is set to compile every time the script is edited, this resulted in many merge errors when we submitted. We later tried to use the .gitignore file to reduce the occurrence of this error.   
  Additionally, we initially used tilemaps to edit our maps, but then we discovered that the tilemap was treated as a whole.  In order to treat farmable land as a separate gameobject, we had to restructure our scene and create each farmable land as a separate gameobject. This problem occurs because we are not familiar with the unity engine, so we failed to choose the appropriate development method according to the project needs at the beginning. But as we develop further, this will get better and better.



  # Devlog Entry - [F1]

## How we satisfied the software requirements

- [F0.a] You control a character moving on a 2D grid.
  - same as last week.
- [F0.b] You advance time in the turn-based simulation manually.
  - same as last weeks.
- [F0.c] You can reap (gather) or sow (plant) plants on the grid when your character is near them.
  - same as last week.
- [F0.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
  - same as last week.
- [F0.e] Each plant on the grid has a type (e.g. one of 3 species) and a growth level (e.g. “level 1”, “level 2”, “level 3”).
  - same as last week.
- [F0.f] Simple spatial rules govern plant growth based on sun, water, and nearby plants (growth is unlocked by satisfying conditions).
  - same as last week.
- [F0.g] A play scenario is completed when some condition is satisfied (e.g. at least X plants at growth level Y or above).
  - same as last week.
- [F1.a] The important state of each cell of your game’s grid must be backed by a single contiguous byte array in AoS or SoA format. Your team must statically allocate memory usage for the whole grid.
  - A LandCell class is defined to encapsulate the attributes of each piece of land, and a LandArea class is defined to encapsulate the AOS byte array.
    ![微信图片_20231206141449](https://github.com/KaneOvO/CMPM121-Final/assets/121581341/da6e9c0f-f118-4d4c-ae68-ab0c49993d6d)
- [F1.b] The player must be able to undo every major choice (all the way back to the start of play), even from a saved game. They should be able to redo (undo of undo operations) multiple times.
  - Undo and redo buttons are added to the UI, and the current scene information is stored in a stack named undo when the player performs key actions. If the player clicks the undo button, the data at the top of the current stack will be popped out and copied to the stack named redo. Then use the pop-up data to replace the current scene data and update the scene. redo logic is similar.  
- [F1.c] The player must be able to manually save their progress in the game in a way that allows them to load that save and continue play another day. The player must be able to manage multiple save files (allowing save scumming).
  - We added a 'Save Data' button and a 'Save Data' panel. When the player clicks it, the panel is activated. If there is no saved data, it shows a 'No Data' prompt; if there is saved data, it displays according to the corresponding save slot location. There are three save slots: one for auto-save, and two for manual saves - Save Slot 1 and Save Slot 2. The manual save slots can be used for both saving and loading. When the player clicks to save, each land's data in the memory is converted into the SerializableLandCell class. Then, global variables are read, and all the land data arrays, combined with these global variables, are encapsulated into the SerializableLandCellArray class, which is then serialized into JSON data. The deserialization process involves converting these data back into the SerializableLandCellArray class, reading the data, and using it to replace and update the scene data in the game.
- [F1.d] The game must implement an implicit auto-save system to support recovery from unexpected quits.
  - The implementation of the auto-save feature is similar to the manual save described above, but it is not operable by the player. The SaveDataManager automatically saves the game every 10 seconds. At the start of the game, if there is auto-saved data available, the player is prompted whether to start the game using this data.

## Reflection

  We encountered difficulties while creating savadata because we were unsure how to extract and store the cache in a JSON file. This has posed a challenge for us. Additionally, during our development process, we encountered some bugs. Due to calling a null pointer in the game, it resulted in runtime errors during gameplay. Unfortunately, we didn't have more time to dedicate to optimizing the player's gaming experience, so our UI retains the style of F0 and continues as is. Overall, our game development hasn't been very smooth as we spent a significant amount of time on refactoring and adding new features.
