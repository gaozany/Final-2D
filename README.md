# GDIM 31 Final: 2D
## Devlogs
### W9:
Gaozan Ye, he/him
Table Number 8, work with Anzhe Huang and Wei Chen.
I decided to do a 2D game for the final.
As a group we came up with a diagram to represent various aspects of our game.
Diagram: [\[insert Google Drawing\]](https://docs.google.com/drawings/d/1286u662xloXkmZQefktn-Hat1Bzrf9WbO-rhysgP6_E/edit?usp=sharing). 
I was responsible for parts of the Environment and all of Kickable Items.
These included the various objects to act as floors and other standing structures: ground, platform, Tilemap Renderer, Tilemap Collider 2D and static Rigidbody 2D.
The various objects which could be kicked: KickableItem script, Sprite Renderer, Rigidbody 2D, Box Collider 2D, type of item and how it would be kicked.
I included arrows from Player to the various kickable objects to indicate the application of a force, from each object to the GameManager to indicate returning its type, and from the various objects in the Environment to indicate support of and collision with Player and other objects.
We all then determined methods of communication between the remaining objects: Player, GameManager, UI, Environment and Kickable Items.

for my final milestone1
I opened the provided 2D Unity starter project and checked the Main scene. The scene already includes the Player, GameController, Canvas, and EventSystem. I also confirmed that the game timer is set to 30 seconds.
I started building the environment by creating a Grid and a rectangular Tilemap. The Tilemap will be used to create the ground and platforms for the 2D platforming level. I add a Tilemap Collider 2D so the player can stand and jump on these surfaces.

### Milestone 1
TODO.

### Milestone 2
TODO.

## Playtesting Reports
### Milestone 1
#### Report 1
TODO.

#### Report 2
TODO.

### Milestone 2
#### Report 1
TODO.

#### Report 2
TODO.

## Open-Source Assets
- [Interior sprites](https://rcpstd.itch.io/interior-tileset-asset-pack-16x16)
- [Ninja cat sprites](https://opengameart.org/content/cat-fighter-sprite-sheet)
- [Food sprites](https://ghostpixxells.itch.io/pixelfood)
- [More food and item sprites](https://ipixl.itch.io/pixel-art-items-part-1)