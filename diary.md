-26/09/2024
Start of the Journey! I have been working on the project for two weeks already. I have created two objects so far, player and the bullets that they fire. Each object has it's relevant script file to determine the behaviour. Currently we have a player which is able to move in a 2D dimension, and can shoot a prefab(bullet). Prefabs are objects that we can instantiate when necessary. Some objects like the player are unique and will not have multiple instances. A bullet could have multiple instances thus we need to create a prefab.

-27/09/2024
Collision logic added. Enemy is destroyed if it is either collided with the player or a bullet. The player now have a life variable. We decrease it by 1 on enemy collision. There is now a spawn manager. It spawns enemies while the enemy is alive at random intervals and random locations. Player death stops enemy spawn.

-28/09/2024
The game should not be clunky. Overall pace increased. I have also added some pre-made stripes and audios for test purposes. Basic objects have been visualized. We are shooting spaceships!

-01/10/2024
Added triple shot laser. The power up can be picked up to obtain triple shot for 10 seconds. We also added an animation for it.

-02/10/2024
I have added speed boost and shield powerups. I had to design a powerup script to manage all three powerups. I used if statements with tags to differ them. Speed and triple shot work perfectly as intended but the shield spawns at the player location without following the player. 

-03/10/2024
I have worked on my OOP skills today. Optimized my spawnman and powerup files primarily. LOC is reduces by a lot. Made some bugfixes as well.

-06/10/2024
I learned a great deal about the Unity library for C#. I have fully implemented a shield powerup with animations that spawns and follow the player til it is destroyed. Cleaned my code a bit as well.

-07/10/2024
I have started working on the UI today. For now i have only added a score and live counter. Made some bugfixes on the spawnmanager as well.

-08/10/2024
When the player dies we have gameover and restart text. We can restart the scene(the game) by pressing r.

-09/10/2024
I have learned a lot about UI today. I have created a menu using a pre-made asset and added a newgame button to it. I have created a second scene for the menu. When we click the button, the game scene is loaded. We now have an almost fully functional basic game. I have also added an asteroid that we shoot to begin the spawning. It is finally time to start working on the animations and visual effects.

-10/10/2024
I have added animations for; enemy explosion, player hurt, shield, thrusters, asteroid explosion. I have also started working on the graphics. I have been playing around with effects like gloom and color grading. The game looks a lot better now. I will start working on the audio as well.

-14/10/2024
I have worked on the sound effects. We use a source component and set clips to manage our sounds. We can also create objects purely to hold sound effects, those objects can be instantiated to play the sounds. I am almost done with this test environment. I am not pushing the sounds and sprites to git as I only use them for test purposes, and will not be used in my final game. 

-15/10/2024
Added some extra features. I will use this game to keep working on the UI and multiplayer function.

-16/10/2024 
I broke my game today! I am done with this shooter space game so I decided to test some extra fatures on it and now the game is quite bugged and nearly unplayable. But i have learned a lot in the process. I did build a working singleplayer version of it tho. I will now start working on enemy ai and cinematography for the final game on a different environment. I will be using the code that i have made till now, not directly but partly, when needed. 

-17/10/2024
I have been working on my git repo today. The structure is much more clear now.