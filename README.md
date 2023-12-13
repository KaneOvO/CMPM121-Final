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
  - Undo and redo buttons are added to the UI, and the current scene information is stored in a stack named undo when the player performs key actions. If the player clicks the undo button, the data at the top of the current stack will be popped out and copied to the stack named redo. Then use the pop-up da
  - ta to replace the current scene data and update the scene. redo logic is similar.  
- [F1.c] The player must be able to manually save their progress in the game in a way that allows them to load that save and continue play another day. The player must be able to manage multiple save files (allowing save scumming).
  - We added a 'Save Data' button and a 'Save Data' panel. When the player clicks it, the panel is activated. If there is no saved data, it shows a 'No Data' prompt; if there is saved data, it displays according to the corresponding save slot location. There are three save slots: one for auto-save, and two for manual saves - Save Slot 1 and Save Slot 2. The manual save slots can be used for both saving and loading. When the player clicks to save, each land's data in the memory is converted into the SerializableLandCell class. Then, global variables are read, and all the land data arrays, combined with these global variables, are encapsulated into the SerializableLandCellArray class, which is then serialized into JSON data. The deserialization process involves converting these data back into the SerializableLandCellArray class, reading the data, and using it to replace and update the scene data in the game.
- [F1.d] The game must implement an implicit auto-save system to support recovery from unexpected quits.
  - The implementation of the auto-save feature is similar to the manual save described above, but it is not operable by the player. The SaveDataManager automatically saves the game every 10 seconds. At the start of the game, if there is auto-saved data available, the player is prompted whether to start the game using this data.

## Reflection

  We encountered difficulties while creating savadata because we were unsure how to extract and store the cache in a JSON file. This has posed a challenge for us. Additionally, during our development process, we encountered some bugs. Due to calling a null pointer in the game, it resulted in runtime errors during gameplay. Unfortunately, we didn't have more time to dedicate to optimizing the player's gaming experience, so our UI retains the style of F0 and continues as is. Overall, our game development hasn't been very smooth as we spent a significant amount of time on refactoring and adding new features.



# Devlog Entry - [F2]

## How we satisfied the software requirements

### F0+F1

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
  - same as last week.
- [F1.b] The player must be able to undo every major choice (all the way back to the start of play), even from a saved game. They should be able to redo (undo of undo operations) multiple times.
  - Modified some code logic to fix some bugs.
- [F1.c] The player must be able to manually save their progress in the game in a way that allows them to load that save and continue play another day. The player must be able to manage multiple save files (allowing save scumming).
  - Modify stored content to accommodate external DSL.
- [F1.d] Grid cells have sun and water levels. The incoming sun and water for each cell is somehow randomly generated each turn. Sun energy cannot be stored in a cell (it is used immediately or lost) while water moisture can be slowly accumulated over several turns.
  - The timing of saving has been modified and will now be automatically saved before closing the game.



### **External DSL for Scenario Design**

We use the JSON file format to store our external DSL, which defines the game's victory conditions, round limit, and command prompts when the game starts.

```
{
  "scenarios": [
    {
      "name": "intro",
      "settings": {
        "maxTurns": 10,               //
        "humanInstructions": "Grow at least 5 carrots, 8 cabbage, and 1 onion in 10 turns",
        "winConditions": [
          {
            "condition": "Carrot",
            "number": 5
          },
          {
            "condition": "Cabbage",
            "number": 8
          },
          {
            "condition": "Onion",
            "number": 1
          }
        ]
      }
    }
  ]
}

```

In the above JSON code, Settings contains the settings of the current scene of the game. The meaning of maxTurns is the maximum number of rounds limit of the game. If the player still does not complete the victory conditions after exceeding this number of rounds, the game will display a text of "you lose". The meaning of humanInstructions is the prompt at the beginning of the game. It will be called at the beginning of the game and displayed at the top of the player's screen to tell the player the victory conditions. winConditions contains the victory conditions of the game, where each list represents the quantity requirements of a plant. If the player completes all requirements before the turn limit is reached, the game will display a "you win" text.



### **Internal DSL for Plants and Growth** **Conditions**
- Codes for defines a new PlantType in enum and add assets into editor.
--
    ```
    public enum PlantType
    {
        EMPTY,
        CABBAGE,
        CARROT,
        ONION
    }
    ```

- Main Plant class that have the skeleton of our plants.
--
    ```
    public class Plant
    {
        public PlantType plantType { get; set; }
        public int level { get; set; }
        public int consumingWater { get; set; }
        public Func<GrowthContext, bool> GrowthCondition { get; set; }
    
        public Plant(PlantType plantType, int level, int consumingWater, Func<GrowthContext, bool> growthCondition)
        {
            this.plantType = plantType;
            this.level = level;
            this.consumingWater = consumingWater;
            GrowthCondition = growthCondition;
    
            PlantDefinition.RegisterPlant(this);
        }
    
        public bool CheckGrowth(GrowthContext context)
        {
            return GrowthCondition(context) && (context.water > this.consumingWater);
        }
    }
    ```

- Codes that defines the growth condition of your plants in each level, you are able to define any special growth condition you like.
-- 
    ```
    public class GrowthContext
    {
        public float water { get; set; }
        public float sunlight { get; set; }
        public bool leftIsPlanted { get; set; }
        public bool rightIsPlanted { get; set; }
    
        public GrowthContext(float water, float sunlight, bool leftIsPlanted, bool rightIsPlanted)
        {
            this.water = water;
            this.sunlight = sunlight;
            this.leftIsPlanted = leftIsPlanted;
            this.rightIsPlanted = rightIsPlanted;
        }
    }
    
    public static class PlantDefinition
    {
        public static void RegisterPlant(Plant plant)
        {
            if (!Plants.ContainsKey(plant.plantType))
            {
                Plants[plant.plantType] = new List<Plant>();
            }
            Plants[plant.plantType].Add(plant);
        }
    
        public static Dictionary<PlantType, List<Plant>> Plants = new Dictionary<PlantType, List<Plant>>();
    
        public static Plant CarrotLevel0 = new Plant(
            PlantType.CARROT,
            0,
            20,
            ctx => ctx.water >= 20 && ctx.sunlight >= 10
        );
    
        public static Plant CarrotLevel1 = new Plant(
            PlantType.CARROT,
            1,
            40,
            ctx => ctx.water >= 40 && ctx.sunlight >= 20
        );
        #......more
    }
    ```

Here is a example:<br />
Let say if I want to add a special condition that check if left and right land plant the same plant:
- 1st: I will define these growth condition:
--
    ```
    public class GrowthContext
    {
        public float water { get; set; }
        public float sunlight { get; set; }
        public bool leftIsPlanted { get; set; }
        public bool rightIsPlanted { get; set; }
        #Define the variable
        public bool leftPlantedSame { get; set; }
        public bool rightPlantedSame { get; set; }
    
        #Added into the constructor
        public GrowthContext(float water, float sunlight, bool leftIsPlanted, bool rightIsPlanted, bool leftPlantedSame, bool rightPlantedSame)
        {
            this.water = water;
            this.sunlight = sunlight;
            this.leftIsPlanted = leftIsPlanted;
            this.rightIsPlanted = rightIsPlanted;
            #Set it into the variable
            this.leftPlantedSame = leftPlantedSame;
            this.rightPlantedSame = rightPlantedSame;
        }
    }
    ``` 

- 2nd: Change the defination of the plant I want to apply:
-- 
    ```
    public static Plant CabbageLevel1 = new Plant(
            PlantType.CABBAGE,
            1,
            30,
            ##Change the win condition context
            ctx => ctx.leftPlantedSame && ctx.rightPlantedSame
        );
    ```

- 3rd: Finally, when you try to check growth pressing "Next Turn", you passed the information needed into growthContext to check
--
    ```
    public void NextTurn()
        {
            Growable growable = GetComponentInChildren<Growable>();
            if (growable != null)
            {
                int totalColumns = GlobalValue.COLUMN;
                int index = FindID();
                int row = index / totalColumns;
                int column = index % totalColumns; 
    
                LandCell currentCell = PlantManager.landArea.GetLandCell(index);
                PlantType plantType = currentCell.landPlantedType;
                int currentStage = currentCell.currentStage;
    
                bool leftIsPlanted = false;
                bool rightIsPlanted = false;
    
                bool leftPlantedSame = true;
                bool rightPlantedSame = true;
    
                if (column > 0)
                {
                    leftIsPlanted = PlantManager.landArea.GetLandCell(index - 1).isPanted;
                    #Check if the left cell have the same PlantedType
                    leftPlantedSame = PlantManager.landArea.GetLandCell(index - 1).landPlantedType == PlantManager.landArea.GetLandCell(index).landPlantedType;
                }
    
                if (column < totalColumns - GlobalValue.Last_COLUMN_OFFSET)
                {
                    rightIsPlanted = PlantManager.landArea.GetLandCell(index + 1).isPanted;
                    #Check if the right cell have the same PlantedType
                    rightPlantedSame = PlantManager.landArea.GetLandCell(index + 1).landPlantedType == PlantManager.landArea.GetLandCell(index).landPlantedType;
                }
    
                if (PlantDefinition.Plants.TryGetValue(plantType, out var plantStages) && currentStage < plantStages.Count)
                {
                    Plant currentPlant = plantStages[currentStage];
                    #Add the new variable into context to be able to check growth.
                    GrowthContext context = new GrowthContext(currentCell.water, sun, leftIsPlanted, rightIsPlanted, leftPlantedSame, rightPlantedSame);
    
                    if (currentPlant.CheckGrowth(context))
                        #......more
    ```

Overall, our internal DSL is created based on the C# language, designed for quickly defining plants and their growing conditions. When using it, developers first need to add the name of the new plant in "plantType", and then define the growth conditions of the new plant at different stages in "plantDefinition". Currently, the definable growth conditions include the need for water and sunlight, as well as whether there are plants planted on the left or right side of the plant.

During the development process, we noticed that a benefit of using an internal DSL is that developers can utilize certain language features. For example, due to the design of our game, we need to use a dictionary to add these growth conditions so that they can be automatically read by the program internally. Here, we adopted an automatic registration method, which allows the defined growth conditions of a plant to be automatically registered in the dictionary. This eliminates the manual process of adding the defined conditions to the dictionary, thereby increasing the automation level of the program.

## Reflection

During the development of F2, we encountered some troubles. The first is that during the development of the internal DSL, because we used enum when initially defining plant types, this forced us to define the definition of plants separately from the growth conditions. In addition, we found that if we want to write an automated growth code, we need to put the plant's growth conditions in a dictionary, but this dictionary must be added manually by the developer, which does not meet the original idea of our DSL. Finally we found the method of automatic registration, through which the growth conditions will be automatically added to the dictionary after being defined.

During the development of the external DSL, we initially used YAML, but later discovered that C# could not natively support reading YAML files and had to import a third-party library. This forces us to return our attention to JSON. Additionally, new parameters such as victory conditions and turn limits are defined due to external DSLs. We had to reconstruct the parts of our archive again. This is to avoid the bug of using old saves but using new victory conditions.
