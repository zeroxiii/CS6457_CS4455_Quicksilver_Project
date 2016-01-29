README
Team Quicksilver
Milestone4
CS 6457

##### Team Members ##### 
Name: Rahmaan Lodhia
Email: rahmaanl@gatech.edu
Prism: rlodhia3

Name: William Gu
Email: wgu36@gatech.edu
Prism: wgu36

Name: Larry He
Email: larry.he@gatech.edu
Prism: lhe44

Name: Mitch Leff
Email: mal8690@gmail.com
Prism: mleff3

##### Group Requirements ##### 
- (COMPLETE) Introduction Menu Scene: Game starts with an interactive menu, where the player can control the character and move him toward doors for selecting the various game modes.  The scene contains the Game Title, Group Name, and Menu Options.
- (COMPLETE) Background Environment: The Background of the title screen is completely interactive.
- (COMPLETE) Menu Item Design:  While the title scene has interactive elements, the Unity UI system was used to make a pause menu and level select menu.  These menus were desgined with padding and anchors in mind for an aesthetic and ease of view appeal.  The font chosen was Crystal for all menu texts.  The buttons on the menu items are all custom images modified to fit correctly on the screen.  In addition, the buttons were animated for both highlightin and pressed animations.  All pop-up menus are navigable by the keyboard / gamepad and mouse.
- (COMPLETE) Credits: The credits are a standard vertical scroll up with a black background.  In addition, several images were added of the team logo and member pictures to add to the aesthetics.
- (COMPLETE) Particle Systems:  Our game already had several particle systems provided by the FX Mega Pack.  Two original systems were also created for the purposes of this milestone: an energy projectile particle system and a damage-indication smoke fire particle system.  These two particle systems fulfilled many of the requested requirements.
- (COMPLETE) Particle System #1: Energy Bullet PS
	- (COMPLETE) Custom Material:  This particle system utilized several meshes and textures to create the effect seen.
	- (COMPLETE) Subemitters:  This particle system has three subemitters.  One for an outer wave.  One for a trail for the bullet.  One that happens on colission with an object where it explodes into electricity.
	- (COMPLETE) Size Over Time: Size and Color over time were mostly used in most of the systems in this PS
	- (COMPLETE) Triggered Event:  This particle system is trigged on an animation event when the character shoots his gun.  The system persists until it runs out or collides with a surface.
- (COMPLETE) Particle System #2: Damage Smoke PS
	- (COMPLETE) Custom Material: This particle system utilized 2-D textures for its effects.
	- (COMPLETE) Triggered and Persistent PS:  This particle system is attached to enemies and activates when their health is below half.  This system will loop and persist until the enemy dies.  This creates the effect of the enemy being heavily damaged as smoke and fire are coming off its damaged body.
	- (COMPLETE) Velocity Over Time:  This particle system leverages the velocity over time option for most of its affects.
	- (COMPLETE) Size Over Time: This particle system also used the size over time and color over time options.


##### Resources Used by Group ##### 
### From Previous Milestone ###
- Asset: Standard Assets by Unity Technologies
- URL: https://www.assetstore.unity3d.com/en/#!/content/32351
- Usage: The third person controller asset was used for our character controller.  The animations were used, and some elements of the scripting was used as a base for character movement.  Any scripts used were heavily modified to match our goal in character movement, so all scripts used are nothing like the original.

- Asset: FX Mega Pack 2 by Unluck Software
- URL: https://www.assetstore.unity3d.com/en/#!/content/14208
- Usage: Some particle systems were used to create effects for the robot that were used by every group member.  Some of the textures in this pack were also used to create custom particle systems.

- Asset: Tile Tool by Unluck Software
- URL: https://www.assetstore.unity3d.com/en/#!/content/19904
- Usage: The objects and tiles used in this asset were used to create the group level.

- Asset: Free Sci-Fi Textures bu Luca Eberhart
- URL: https://www.assetstore.unity3d.com/en/#!/content/9933
- Usage: The tiles used in this asset were used to create the group level.

- Asset: Sci-Fi Door by 3DMondra
- URL: https://www.assetstore.unity3d.com/en/#!/content/21813
- Usage: The objects used in this asset were used to create the group level.

- Asset: Warriors Pack Bundle 1 Free by Explosive
- URL: https://www.assetstore.unity3d.com/en/#!/content/36405
- Usage: All Enemy AI models were taken from this model pack.  Each one is used by a group member for their individual AI.

- Asset: RAIN AI for Unity by Rival Theory
- URL: https://www.assetstore.unity3d.com/en/#!/content/23569
- Usage: Used to program all AI in level.

- Asset: Volume Particle Light by Dark Cube Games
- URL: https://www.assetstore.unity3d.com/en/#!/content/10105
- Usage: Used for the Ranged AI's projectile attack.

- Asset: Maze element Ice Golem by Andres Olivella
- URL: https://www.assetstore.unity3d.com/en/#!/content/3746
- Usage: Death and hit animations used for AI.

- Asset: Universal Sound FX by Imphenzia
- URL: https://www.assetstore.unity3d.com/en/#!/content/17256
- Usage: Used for most of the sound effects in game.

### Added to Current Milestone ###
- Asset: Introduction to New UI by 3D Buzz
- URL: http://www.3dbuzz.com/training/view/modern-ui-dev-in-unity-46
- Usage: Custom button images were used for UI menus, and tutorial aided in their creation

- Asset: Crystal Font by Felipe Munoz
- URL: http://openfontlibrary.org/en/font/crystal
- License URL: http://creativecommons.org/licenses/by/3.0/legalcode
- Usage: Used as main font throughout game

- Asset: Day 69, Day 60, Day 23 by Mark Sparling
- URL: http://www.marksparling.com/
- Usage: These royalty-free songs were used for the in-game background music

- Asset: 2D Platformer + Free 2D Assets Pack by Brackeys
- URL: http://www.brackeys.com/preview/2d-platformer-course/
- Usage: Fading script was used for screen transitions and tutorial implementation was loosely followed in incorporating transition functionality in game


##### Build Instructions ##### 
Use standard build settings for Web Player. Ensure all three scenes (quicksilver_title.unity, quicksilver_m4.unity, and quicksilver_credits.unity) are included in build.

##### Grader Instructions #####
## Group Component ## 
- Controls:
	- Recommended input is a XBOX 360 Controller 
	- Walk/Run: WASD / Leftstick on Gamepad (Character will turn towards the direction of the keypress relative to the camera as he moves)
	- Jump: Spacebar / (A) Gamepad Button
	- Dash: Left Shift / (B) Gamepad Button
	- Air Dash: Left Shift / (B) Gamepade Button WHILE IN AIR AND SMALL ONLY
	- Attack: Q / (X) Gamepad Button
	- Shoot: R / (Y) Gamepad Button
	- Raise Shield: E / Left Bumper Gamepad Button
	- Shrink/Grow: Left Alt / Right Bumper Gamepad Button
	- Crouch: C / Leftstick click in (This is toggle button.  Character will remain crouched until button is pushed again)
	- Activate Ragdoll: K / Rightstick click in (This is a toggle button.  Chracter will remain a Ragdoll until button is pushed again)
	- Start: Escape / Start Gamepad Button.  Opens the pause menu or used for other functions in game.

- Viewing Requirements:
On game load, you should see the title scene.  The name of the game and the team will appear on the bottom as a HUD overlay.  The characer should be able to be controlled by the player.  

First, go to the Credits and walk in front of it.  It should immediately load the credits scene.  Note that the credits will begin scrolling up vertically. All the requried information will be present and there will be images to help add flavor to the credits.  Waiting until the credits end will load the game back into the title screen.  Otherwise, pressing start will also return the player to the title screen.

Second, once back in the title screen, walk towards the New Game door.  Stepping in front of it will immediately load the main level of the game.  The player can also reach the main level of the game by going to the Level Select door.  This door will bring up an overlay menu with options for Level 1, Level 2, or Main Menu.  Selecting Level 1 or 2 will load the same main level.  Pressing Main Menu will load the title screen again.

Third, once the main level is loaded, the player can now begin the game.  Pressing Start at this point will open up the Pause Menu.  This menu will have options to resume, restart level, or go back to the main menu.  Once more, feel free to experiment with the different options.

Fourth, during the main level, pressing shoot will release the energy bullet particle system.  Note that it contains several different systems, and on collision, it will burst into an explosion of electricity.

Fifth, find two of the wandering enemeies in the stage (either the large purple brute at the end or the white ninja in the beginning).  Using the Shoot button, fight the enemies with the energy projectile.  The enemies currently both take two hits to die.  After the first hit, note that the enemies will start smoking and have flames sprout.  This is the second particle system.  Note how it persists in the enemies damaged state and uses the various particle system qualities requested by the assignment.  Shooting the enemy again will cause it to die.  On death the particle system will turn off.


##### Unity Scene #####
The main file used is quicksilver_title.unity.

##### URL ######
http://www.prism.gatech.edu/~rlodhia3/CS6457/Milestone4/Milestone4_Player
