README
Team Quicksilver
Milestone3
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
RAIN AI OPTION WAS CHOSEN
- (COMPLETE) Navigation Mesh Rig:  Four navigation rigs were created for differing heights of platforms and differing size of characters.
- (COMPLETE) Navigation Targets: Sets of navigation targets were created for use on their own or in conjunction with a waypoint network.
- (COMPLETE) Waypoint Network Rig:  Two Waypoint Network Rigs were created for use with two of our AIs.
- (COMPLETE) Waypoint Route Rig:  One Waypoint Route Rig was created for use with one of our AIs.
- (COMPLETE) Mechanim Motor:  All AIs used utilized the Mechanim Motor to pass parameters to the Animation Controller.
- (COMPLETE) Mechanim Animator:  All AIs have animation states levereged by using the Set Mechanim Parameter Node in the RAIN AI rig.
- (COMPLETE) Custom RAIN AI Element:  A RAIN AI action was created to determine where a leading shot should be aimed based on the direction of the player movement.

##### Individual Requirements ##### 
## Rahmaan Lodhia ##
- Brute Enemy AI: A large enemy that patrols in the back of the level.  He is larger compared to all other entities in the level, so his navigation is limited to areas he can fit.  He has short vision and attack range.  He is set to chase down and attack a player if he sees them.  Otherwise, he will continue his patrol route.
- (COMPLETE) Navigation:  The Brute utilizes a Waypoint Route Rig to determine his patrol.
- (COMPLETE) Senses:  The Brute utilizes two Visual Sensors (one for detecting the player and for determining if the player is in attack range)
- (COMPLETE) Animation States:  The Brute has a Movement, Idle, and Attack animation state

## William Gu ##
- Melee Enemy AI: This is an average sized enemy who utilizes melee combat to attack the player.  He is quick and erratic.  His patrol patterns are randomly determined.  If he detects the player, he will chase him down and quickly attack him.
- (COMPLETE) Navigation:  The Melee utilizes three Navigation Targets to determine his patrol.
- (COMPLETE) Senses:  The Melee utilizes two Visual Sensors (one for detecting the player and for determining if the player is in attack range).
- (COMPLETE) Animation States:  The Melee has a Movement, Idle, and Attack (one for punching and one for kicking) animation state.
        
## Larry He ##
- Regen Enemy AI:  This is an average sized enemy who utilizes melee combat to attack the player.  This enemy also regenerates after death. (i.e. He trips sometimes and gets back up.)  If he detects the player, he will chase him down and attack him.
- (COMPLETE) Navigation:  The Regen utilizes a Waypoint Network Rig and Navigation Targets to determine his patrol.
- (COMPLETE) Senses:  The Regen utilizes two Visual Sensors (one for detecting the player and for determining if the player is in attack range).
- (COMPLETE) Animation States:  The Regen has a Movement, Idle, two Attacks, and Death animation state.

## Mitch Leff ##
- Ranged Enemy AI:  This is an avergage sized enemy who utilizes ranged combat to attack the player.  This enemy is restricted to movements on the high platforms.  This AI will have long vision and attack range.  When attacking, the AI will use a Custom RAIN AI element to lead its shot depending on how the player moves.  
- (COMPLETE) Navigation:  The Ranged utilizes a Waypoint Network Rig and Navigation Targets to determine his patrol.
- (COMPLETE) Senses:  The Ranged utilizes two Visual Sensors (one for detecting the player and for determining if the player is in attack range).
- (COMPLETE) Animation States:  The Ranged has a Movement, Idle, and Attack animation state.
- (COMPLETE) Custom Action:  The Ranged AI will try to calculate a leading shot and shoot a laser in a location where the player is going.


##### Resources Used by Group ##### 
### From Previous Milestone ###
- Asset: Standard Assets by Unity Technologies
- URL: https://www.assetstore.unity3d.com/en/#!/content/32351
- Usage: The third person controller asset was used for our character controller.  The animations were used, and some elements of the scripting was used as a base for character movement.  Any scripts used were heavily modified to match our goal in character movement, so all scripts used are nothing like the original.

- Asset: FX Mega Pack 2 by Unluck Software
- URL: https://www.assetstore.unity3d.com/en/#!/content/14208
- Usage: Some particle systems were used to create effects for the robot that were used by every group member.

### For Current Milestone ###
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

- Asset: RAIN AI for Unity
- URL: https://www.assetstore.unity3d.com/en/#!/content/23569
- Usage: Used to program all AI in level.


##### Resources Used by Rahmaan ##### 
None besides Group Resources

##### Resources Used by William ##### 
None besides Group Resources


##### Resources Used by Larry ##### 
None besides Group Resources

##### Resources Used by Mitch ##### 
- Asset: Volume Particle Light
- URL: https://www.assetstore.unity3d.com/en/#!/content/10105
- Usage: Used for the Ranged AI's projectile attack.


##### Build Instructions ##### 
Use standard build settings for Web Player. Ensure all four scenes are included in build.

##### Grader Instructions #####
## Group Component ## 
- Controls:
	- Recommended input is a XBOX 360 Controller 
	- Walk/Run: WASD / Leftstick on Gamepad (Character will turn towards the direction of the keypress relative to the camera as he moves)
	- Jump: Spacebar / (Y) Gamepad Button
	- Dash: Left Shift / (B) Gamepad Button
	- Air Dash: Left Shift / (B) Gamepade Button WHILE IN AIR AND SMALL ONLY
	- Attack: Q / (X) Gamepad Button
	- Raise Shield: E / Left Bumper Gamepad Button
	- Shrink/Grow: Left Alt / Right Bumper Gamepad Button
	- Crouch: C / Leftstick click in (This is toggle button.  Character will remain crouched until button is pushed again)
	- Activate Ragdoll: K / Rightstick click in (This is a toggle button.  Chracter will remain a Ragdoll until button is pushed again)

- Viewing Requirements:
- Starting from the initial location, the first Enemy AI can be seen.  This is the melee AI (it is a white ninja).  This AI will move randomly to one of three navigation targets near the beginning of the level.  If it see a player, it will attempt to attack it as long as it is in range.  It's attack range is very small, so he is easy to escape.  If it loses sight of the player, it will resume its patrol.
- After the melee AI, the regen AI will be patroling on a Waypoint Network in the middle of the level.  This AI is green.  It will occasionally trip in its movements or attacks.  Like the melee AI, it will attempt to chase and attack the player.  Escaping it is easy.  Outrun it or dash past it, and it will resume its patrol path.
- The next AI is the ranged AI.  She will be patrolling the higher platforms past the first bridge of the level.  She moves in a Waypoint Network Rig.  Her vision is very large, and she will attack the player from afar with her laser attack.  Her lasers will shoot at the players location or predict the next location the player is going depending on his velocity.  While moving within her vision, the laser should appear to go directly in front of where the player is trying to move.  She cannot move while attacking, so sneaking past her attack range will force her back into her patrol.
- The last AI is the large purple Brute AI.  This large AI can only navigate through areas it can physcially fit.  He is on a Waypoint Patrol route.  When it sees a player, he will chase him down and try to attack.  In addition, if the player leaves his sight, if possible, he will attempt to navigate towards the players last location to see if the player is still there to resume the chase.  Otherwise, it will continue down its patrol route.


##### Unity Scene #####
The main file used is quicksilver_m3.unity.

##### URL ######
http://www.prism.gatech.edu/~rlodhia3/CS6457/Milestone3/Milestone3_Player
