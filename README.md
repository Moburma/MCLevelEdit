# MCLevelEdit
A simplistic Level Editor for the Bullfrog Productions game Magic Carpet. It's written in PowerShell 5.

**I won't work on this any more, please see the much better C# editor here:** https://github.com/thobbsinteractive/MCLevelEdit 
This new editor supports actual rendering of the terrain and is far more feature complete than this attempt ever could be.

Currently it only works for the original Magic Carpet, but likely works fine for levels from the Hidden Worlds expansion too. It does not work with Magic Carpet 2 levels (yet).

At the moment this is more of a proof of concept release, it is useful mainly for reverse engineering and editing the existing levels. 

![Example Screenshot](MCLevelEdit1.png)

## New in Version 0.3

* Mana estimation to help work out reasonable mana targets for levels. Press the button to force a recalculation of this when you add more Things.
* MapGen Attributes. You can change the fundamentals of how a level looks with these. Note any changes you make will NOT be reflected within the map image, you will need to make changes by trial and error, sadly.
* Wizard AI Attributes. These are not understood, but determine how skilled a wizard is in-game. Tick the box to toggle seeing these in the Wizard stats window.
* New Map button. This actually loads a file called template.dat with nothing in it. Use this to make new maps based on templates for e.g. common Wizard stats.

## Usage

* You will need the original loose level files. These are NOT included with Magic Carpet Plus, but can be found on the original Magic Carpet CD in the LEVELS directory. Either that, or you can extact them yourself with my [MCDatTab](https://github.com/Moburma/MCDatExtractor) tool.
* You will need to decompress the levels to edit them. They are RNC compressed and there are many tools available to achieve this, [this](https://github.com/lab313ru/rnc_propack_source) tool is recommended
* Once the above is complete, run the script via PowerShell: MCLevelEdit.PS1
* Choose Load to load one of the extracted and uncompressed level files above
* Edit levels as you see fit
* When finished press the save button to save a level file. Note this takes a relatively long time (approx 30 seconds) to save, so be patient! You will see a prompt when complete
* To play levels, you can optionally RNC pack them first, and then add them with the other level files to the LEVELS.DAT/TAB file. To do this, see my other script [here](https://github.com/Moburma/MCDatTab)
* Keep in mind that a large number of the game's levels work by only giving the player the spell inventory they have found at that point, and ignore what is set in the level file. If you set your level as the first one in the game, for example, the player will always start with NO spells, regardless what is set in the level file. To work around this, either save as a later "spell vampire" level, include the spells on the ground at map start, or use the Alpha executable found on the original game CD (non-Magic Carpet Plus) for testing

For information on game entities and attributes, see [here](https://tcrf.net/Notes:Magic_Carpet_(DOS)) and [here](https://github.com/michaelhoward/MagicCarpetFileFormat/blob/master/magic%20carpet%20file%20format.txt).

Things placed in a level will appear in the map overview at the top left of the window. The currently highlighted object in the main dataview will appear orange (can be hard to see), so use that to know which object is where. Things like large buildings, craters, and walls just appear as small dots, so what is seen here is not the same as in-game.

Map Key:<br/>
Red - Creature (enemies but also villagers)<br/>
Yellow - Player start (including computer controlled wizards)<br/>
Green - Scenery (includes trees and standing stones)<br/>
Purple - Spells<br/>
Cyan - Effect (things like explosions but also extra map elements like canyons and walls)<br/>
White - Switch (hidden switches)<br/>

## Limitations/Known issues

* The map image support is very crude and only a few levels have map images pre-created. The rest will appear as black squares
* Removed items still appear on the map image
* No editing of mana total and target yet

## Todo

* Support for editing level mana total and mana target
* Hopefully integration with the [REMC2](https://github.com/thobbsinteractive/magic-carpet-2-hd) project to have proper realtime map view
* Magic Carpet 2 level support - unlikely, check out [REMC2](https://github.com/thobbsinteractive/magic-carpet-2-hd) which now includes a Magic Carpet 2 level editor.
* Make it look nicer
