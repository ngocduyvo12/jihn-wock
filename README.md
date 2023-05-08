Github: https://github.com/ngocduyvo12/jihn-wock

Welcome to Jihn Wock, a pixelated 2D action game inspired by the John Wick movie series. In this game, you take control of a biker armed with a gun and navigate through beautifully designed 2D environments filled with enemies.

Dependencies:
These packages need to be installed with package manage:
- Input System
- Newtonsoft Json
- TextMeshPro
- UnityUI
- Visual Scripting

File Contents:

Build folder: Built version of the game. Simple launch Jihn Wock.exe to play the game 

assets folder: contains all the game assets, including character sprites and background images.
- Animation folder contains all of the animations for the player character, enemy characters and boss
- scripts folder: contains all the game scripts, including the player movement script, enemy behavior script, and boss behavior script.
    - FadeScript is for fading in and out the splash images or you can say the transitions when the game is over or when the next level is starting.
    - HammerScript is for the boss and his hammer so that it can detect the collisions for the player and deals with its damage.
    - ObjectDestroyer is for destroying a game object like bullet after some time so that they don't be always in the game and make the game slow.
    ParallaxEffect is for giving the parallaxing or moving effect to the backgrounds.
- The Prefabs folder contains all of the prefabs that are being used in the game.

- scenes folder: contains all the game scenes, including the game levels, and boss levels.
    - To start from beginning using the source code, you will have to open the level1 scene in the scenes folder.

Design Decisions:

I made certain design decisions to make Jihn Wock an exciting and challenging game for players. The pixelated art style and the use of a biker as the main character was inspired by the John Wick movie series. 
The decision to include a gun for the player character and med kits dropped by enemies was made to increase the game's action and challenge. 
The use of a boss at the end of every level adds a climactic ending to each level and increases the game's overall difficulty.

Controls:

A & D or Left & Right Arrows: Move player left and right
Spacebar: Jump
Additional Information:

To start the game, double-click on the "Jihn Wock" executable file.
The game consists of multiple levels, each with a unique environment.
If the player's health reaches zero, the game ends, and the player must restart from the beginning of the level.
The player's health can be restored by picking up med kits dropped by enemies.

I hope you enjoy playing Jihn Wock and have fun defeating all the enemies and bosses!
