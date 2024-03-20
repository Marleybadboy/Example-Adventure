# Example-Adventure

## About
Example Adventure is a 2D Roguelike game. The player's goal is to survive as long as possible in the arena. The player has various attack variants at his disposal and in order to survive in random places there appear finds. Unlike other games, the player creates opponents in a special editor by scaling the difficulty level. The more opponents he creates, the more they will appear in the arena!

### World
The game is divided into main menus and arena scenes.

#### Arenas

The arenas are loaded asynchronously using Addressable Assets which affects the size and smoothness of loading each arena. 
This also makes it easier to add more arenas which are selected randomly.

<p align="center">
  <img src="Visual_Readme/Screenshot 2024-03-20 20-52-19.png" alt="Main Menu" width="300"/>
  <img src="Visual_Readme/Screenshot 2024-03-20 20-53-11.png" alt="Arena Example" width="300"/>
</p>


### :zombie: Enemy Editor 
A special editor for creating opponents. The editor uses ScriptableObjects as a database and reads stored values and restores previously created opponents. 

Editable elements in opponents:
1. strength
2. speed
3. skin selection
4. type of attack - uses the `IEnemyAttack`  interface which allows for easy implementation of attack types of the attractor
5. Deleting already created opponents

<p align="center">
  <img src="Visual_Readme/Screenshot 2024-03-20 21-19-59.png" alt="Enemy Editor" width="300"/>
</p>

> [!NOTE]
> There is a default opponent in the game that cannot be removed

### Save System
The game uses a simple system of saving gameplay. We can save the gameplay at any time in the pause menu. The gameplay save is in `json` format and is located in `persistentDataPath`.

<p align="center">
  <img src="Visual_Readme/Screenshot 2024-03-20 21-40-08.png" alt="Pause" width="300"/>
</p>

Note that a player can have one save in total. And it holds health and ammunition data also it should be used with caution such fromsoftwer solution. :superhero::bone:
