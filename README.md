# GDIM 31 Final: 2D
## Devlogs

### Milestone 1
Gaozan Ye
A bug during Milestone 1 was to do with the checklist not being updated when an object was kicked. As the object to be kicked would move away appropriately with a press of the Space key, I assumed the other functions involved in the process of kicking were fine. So I traced all relevant methods, that is, from KickAttack to Kickable.Kick(), GameController.Kicked(), HUD.KickedItem() and ChecklistItemUI.FoundItem(). It appeared that many of these functions contained no code at all, and in particular there was no list of all required items.
Therefore, in the GameController Start() function I made a List of GameObjects containing all objects which could be kicked to complete the level, and supplied this to the HUD with SetRequiredItems(), which set the appropriate ChecklistItemUI for each. I then altered FoundItem() to change the sprite from an empty box to a checked box. I checked that each of the pizza and donuts would cause only its relevant box to be ticked. Overall, it has been useful to trace all methods called in this process, rather than fiddling pointlessly with aspects of the player's ability to kick objects.
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