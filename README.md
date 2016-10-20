#LibreCrewAssignment
* A KSP mod to help with crew assignments in the VAB/SPH.  Based on the last MIT-licensed version of BetterCrewAssignment by Snark.*

## Features

* It makes better automatic default choices for crew assignments (e.g. "labs need scientists" or "drills need engineers").
* It remembers your assignments (note: be sure to "save" ships to ensure that LCA remembers properly), so that the next time you launch that ship, it will try to do the same thing.  (No more discovering, *after* you're in orbit, that your gosh-darn rescue ship filled up all the slots and you've got nowhere to put the stranded kerbal!)
* You can customize the default behavior with ModuleManager config.
* No user interface.

### Criteria

* Make sure there's a pilot on board, if you don't have any SAS-capable probe cores.
* Staffs science labs with scientists.
* If you have any non-rerunnable science experiments on board, make sure there's at least one scientist.
* If you have any parts that need an engineer (ISRU, drills), make sure there's at least one engineer.
* Try to pick the highest-level crewmembers available. (Except for pilots; if you have an SAS-capable probe core and your pilots are all lower-level than the core, it picks the *lowest* pilot available.)
* Tries to fill all command pods; doesn't try to fill passenger cabins.
* If you do manual assignments in the crew tab and then save the ship, it remembers your choices the next time you load the ship.  Empty slots will be left empty.  It will try to assign specific kerbals by name (e.g. "Jeb goes in slot 0 of this command pod"), and if that crewmember is unavailable, will try to assign another kerbal of the same profession (e.g. "I want Jeb, but he's on a mission already, so I'll use this pilot here.")

###Default choices
Default choices are controlled by [ModuleManager](http://forum.kerbalspaceprogram.com/index.php?/topic/50533-105-module-manager-2613-november-9th-with-more-sha-and-less-bug-upgrade/) config in a file that comes with the mod (see "How to customize", below).  There are two flavors of default choices, *assignments* and *requirements*.

**Assignments** are default choices for crew slots in specific crewable modules.  The default config that comes with the mod assigns scientists to science labs, and all crewmembers to command pods. The default config deliberately leaves passenger cabins empty, though you can tweak this by adding your own config if you like.

**Requirements** are added to parts that are not themselves crewable, but which need a particular type of kerbal to operate them.  If your vessel has any parts that specify requirements, then the mod will try to ensure that at least one of the specified kerbal type is present in the crew. The default config that comes with the mod adds a "scientist" requirement to all non-rerunnable science experiments (Mystery Goo, Science Jr.), and an "engineer" requirement to ISRU units and ore drills.

###Player choices
When you load a new ship, or add a new part, then everything is controlled by the default behavior and assignments will be updated dynamically as you switch stuff around on your ship. It can do this because you haven't *observed* the assignments and it's therefore free to shuffle assignments around without invalidating any of your choices.

However, the moment you switch to the "crew" tab in the editor and see what the assignments are, it then nails all the assignments in place.  (It's a [Heisenbergian](https://en.wikipedia.org/wiki/Observer_effect_%28physics%29) sort of thing.)  Basically, what it's doing is assuming that the moment you *see* the assignments, they become your conscious choices rather than something the program assigned.

Once you see the assignments (and make any changes of your own), those get persisted to the ship, and will be saved when you hit the "save" button.  Such specific choices are assumed to be for a specific kerbal, or for a kerbal of that profession if the kerbal isn't available.

###How to customize
Since all crew assignments/requirements are controlled by [**Module Manager**](http://forum.kerbalspaceprogram.com/index.php?/topic/50533-105-module-manager-2613-november-9th-with-more-sha-and-less-bug-upgrade/) configs,  you can add your own .cfg file to change the behavior to whatever you like.

If you'd like to customize the behavior, the following references may be helpful:

* **LibreCrewAssignment.cfg:** This is the Module Manager config that is installed with the mod. It includes detailed comments explaining how it works, so that you can write your own config.
* **Module Manager documentation:**  You can find helpful information [**here**](https://github.com/sarbian/ModuleManager/wiki/Module-Manager-Syntax) and [**here**](https://github.com/sarbian/ModuleManager/wiki/Module-Manager-Handbook).

##Dependencies

LibreCrewAssignment depends on [**Module Manager**](http://forum.kerbalspaceprogram.com/index.php?/topic/50533-105-module-manager-2613-november-9th-with-more-sha-and-less-bug-upgrade/).

##Download and install

Ordinarily, I'd direct you someplace to get a zip file to unzip into GameData, but BetterCrewAssignment is still in active development.  If you really care enough to use LCA over BCA, grab the source from [**GitHub**](https://github.com/Kerbas-ad-astra/LibreCrewAssignment) and compile it with your favorite C# development tools, then plop the DLL and LibreCrewAssignment.cfg in a "LibreCrewAssignment" folder in GameData.

##Version history and changelog

* 02016 Oct 11 -- Snark changes the BCA license from MIT to CC-BY-NC-ND, inspiring the creation of LCA.

##Roadmap

Keep LCA up-to-date with KSP updates.  Make a snazzy logo.

A fully-compiled release will happen when Snark gets hit by a bus, and not before.

##Credits

Thanks are owed to Snark for creating BetterCrewAssignment and inspiring LibreCrewAssignment's existence.

##License

LibreCrewAssignment is copyright 2016 Kerbas_ad_astra and released under the GNU GPL v3 (or any later version).  If you make a fork and redistribute it (unless it's intended to be merged with the master or if I'm handing over central control to someone else), you must give it a different name and folder in addition to the other anti-user-confusion provisions of the GPL (see sections 5a and 7).  All other rights reserved.