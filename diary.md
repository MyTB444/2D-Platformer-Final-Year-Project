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

-18/10/2024
I have initialized a project with a pre made public environment to further work on effects. I will be learning how to create complex scenes(sub mesh, occlusion, light mapping etc.), cinematography and AI. I will not push everything on git as some files are massive. I will only push the exact files that I am working on.

-21/10/2024
This week I will be working on the visual aspects. Today, I have been working on some premade assets to test different materials and lightings. I have learned quite a lot about material design(occlusion, albedo panel...). I have learned how to create real world materials like glass, gold etc.. I have also been working on lighting. We apparently can use probes to move the lighting data from one source to the whole environment. This provides an easier baking process. I have also worked on reflections, which is not only dependant on the light but also the reflection probes that we create around the objects that we want to reflect. We use post processing to define the specific look from our main camera, which is usualy what the player sees. I will now test point and click functioning and enemy AI.

-22/102024
I have been working on point and click controls and camera manipulation. I have implemented point and click movement to an object. The camera angle changes and follows the player as they travel trough the area. Also, I have been woking on animation logic. I use a boolean called walk to swap between idle and walking animations. We set it to true as the player is moving, and set it back to false when we reach the destination.

-23/10/2024
I have implemented 3 AI based objects. Using navmesh agents and for loops, i have implemented a unique movement pattern for them. I will be working extensively on their AI. They will take action based on player movement. 

-24/10/2024
GuardAI script now have modular behaviour for every object. I can now create new guards and add as many move points as i like. The script is designed around handling any number of moving points, even if there is non.

-28/10/2024
Today, I focused on reading. I have learned and will be learning quite a lot about game development. I believe pondering about how to clearly phrase my experiences, is a good idea. 

-30/10/2024
I am working on my modular scripting, i will be using it quite a lot to implement multiple enemy AI later on. I am also working on line of sight. Enemies will take action based on player movement.

-31/10/2024
I have optimized my AI scripts today. Controlling the animations/sounds of objects can get very complicated as more behaviors are added. Today, i have implemented a much better movement pattern for multiple guards and they now can be distracted by the player by instantiating an object. They stop their regular movement routine and walk directly to the instantiated object without having their movement animation bugged. These AI scripts will be very usefull once i start creating the enemies for my main game.

-04/11/2024
I have been doing some reading on animations and sounds. I have implemented a singleton class to manage all the audio for the game. I could be using the exact script in the maingame. I also added multiple animations for a single object and learned how to control the logic for multiple animation triggers. I am almost done with my learning environment and will start designing the visuals for the dungeon game.

-05/11/2024
It is finally time to build my game. I have many example scripts and objects which I will be using and inspiring from. The principal behaviors such as player movement and enemy ai/spawn are ready. The first thing to do is to find assets and create animations to have visuals to work with. 

-06/11/2024
I have found some simple pixel assets that I can work on. I will customize those assets to create my own unique maps and characters also to create unique animations. For now i have creatd a demo map with some little items, and a simple character. I will start implementing the essential behaviors.

-07/11/2024
I have created a demo map with 2 objects. Also, a simple character called jack. I will be approaching everything modularly. In this demo map, i will implement all the behaviors on jack, behaviors which I will be able to use on different characters. The environmental behaviors should be modular as well. Meaning, adding new maps should not require any additional hard coding. October was a speed learning month for me. It is finally time to start utilizing the skills that I have acquired to build the game that I dream of! 

-11/11/2024
I am still working on the sprites that i will be using. Today i have added jumping behavior for the player. I have also created idle, running and jumping animations for Jack.

-12/11/2024
Massive progress today! Jack can not run, jump, roll and swing his sword with good looking animations. I have also added a hitbox logic for his attack. We have goblins spawning every 5 seconds. Jack can destroy these goblins with his sword. The goblins do not have any behavior yet. I am doing my best to make sure nothing is hard coded. Also, the camera is now following Jack!

-13/11/2024
I am still working on script modularity. I will be adding few character and enemies to this game, having separate scripts for everysingle one is a terrible idea. I need good player/enemy scripts that will be inherited by personalized classes. Today, i have improved my player script and added follow ai on the enemies.

-14/11/2024
Enemies can now jump and they have animations. Intead of destroying them immediately we have a death animation. They fall off the platform when killed.

-18/11/2024
I have improved enemy script modularity. Also, enemies now stop moving at a certain distance to jack which will be unique for each enemy, they wait for a second when they stop. They will be attacking in this time line.

-19/11/2024
Improved animations for enemies and jack. Enemies now attack when they get close to the player. The attack duration and distance are changable float number which will be unique for each type of enemy. Jack attack hitbox is now much better.

-20/11/2024
Added attack hitbox for goblins with logic. Now both player hitbox and goblin hitbox push each other when hit. Also, improved both enemy and player movement and interactions. We also destroy enemies if they are out of the screen to avoid unnecessary load.
-21/11/2024
I have spent this whole day working on the report.
-22/11/2024
Jack can dive now! I have started creating my maps. There will be 9 maps in total in the final game, i have designed 3 of them and implemented 1. I have also added climbing functionality for jack, with an animation. I will start adding more enemies and create a playable demo.
-23/11/2024
All the maps for the demo are now fully implemented. Enemy scripts are much better, we have now a goblin archer. It does now move, only shoots arrows when jack is at the same height. We also have some spikes on the ground in one of our maps. Also, the overall project structure is much better now. The scene looks very clean.
-24/11/2024
I have finished the animations for the archers. All the enemy and player variables are set and optimized. Demo map is fully implemented and archers are located. We have an objective now! The player can open a gate after they destroy two specific objects, get a key and bring the key on the gate. The aim of the demo is to escape the map. Basically, I now have a playable demo game. 
-25/11/2024
I have made a Main menu with start and exit button. I also have UI to display player hp using hearth signs. We have a text to instruct how to restart the game and exit when the player dies or finishes the demo.
-26/11/2024
I have added control information to main menu. We can now hard restart the game before finishing, and go back to main menu. I am improving my animations and scripts as well.
-27/11/2024
I am optimizing my code, improving gameplay, fixing small buggs and working on my report.
-02/12/2024
This weekend, I have added a time tracker to count as the player score. I am spending most of my time working on my report and getting ready for the presentation.
-03/12/2024
Added arrows to indicate where the player should go. Jack starting point is also changed. We also now have an ending box for jack.
-04/12/2024
I am making small adjustmens to my code. I am focused on the presentation and the report.
-05/12/2024 
Added many enviromental animations, the game looks feels way better now. Also fixed an old bug where enemies would start two movement coroutines and attack twice.
-06/12/2024
Today I ve worked extensively on the overall game physics. Customized layering and collision. The gameplay feels much better now. I have also added some animations for the main menu.
-07/12/2024
I have recorded some audios for the game. The characters have sound effects we also have an ongoing background wind sound.
-10/12/2024
I am improving my source code.
-11/12/2024 
I am adding more documentation and finalizing the demo.
-09/01/2025
I have decided to change my map structure completely. I will now use tilemapping instead. I had to do this before designing further maps.
-10/01/2025
I have been thinking deeply about the quality of my code during the summer. :D  I am improving my AI code modularity and logic. Also polishing the sizing issues occured after swapping to tilemap.
-13/02/2025
Enemies now have a field of view. Archers attack based on their fow. They also have a distance variable based on jack location. They can now attack in an angle, and always attack towards jack. The background is updated, map is expanded.
-15/02/2025
We now have a goblinminiboss. It has two separate attacks. The fighting arena inside the new map is also implemented. Overall enemy AI is improved.
-16/02/2025
I now have a detailed boss fight. I also want one of my levels to be movement based. I have started working on the cloud map and added many traps around.I will implement an inventory system and add double jump boots as the reward of this map.