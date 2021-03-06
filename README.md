# Rocket-League-Map-Loader
A custom map manager program for rocket league.

NOTE: Needs to run as administrator to be able to update Hamachi firewall settings.

Future improvements:
- There is currently no multithreading implemented so downloads and other long processes will freeze the UI.
- Add map filtering and searching
- Community maps is currently relying on Lethamyr's website (Lethamyr.com). Currently there are no community maps uploaded but when there are, they will hopefully follow the same structure as Leths maps and not break my program when trying to load them haha.

To use:
1) Run through the initial setup.
- Find your rocket league directory path.
- Install Hamachi, BakkesMod and Rocket plugin for multiplayer custom map functionality.

2) Wait for maps to populate in main menu.
- Once loaded, you can click "Download" on a map to download it.
- On a downloaded map, click "Load" to load it ready for use in rocket league. Only one custom map can be loaded at a time as this overwrites Labs_Underpass_P.upk.

3) Open Hamachi and BakkesMod

4) Once a map is loaded, go to "File" > "Force restart Rocket League" to restart the game.

5) Set up a Hamachi network and get your friends to join. 
- Run through Hamachi Diagnostics ("Help" > "Diagnostics") to check for any connection errors.
- Also try pinging the other people in your network for a response.

6) If Hamachi is working, go into Rocket League
- Press F2 to open up BakkesMod, then go to "Plugins" > "Rocket Plugin" > "Open rocket plugin GUI".

7) If hosting, select "Underpass" from the arena selection then press the "Host" button.
- Leave IP as 127.0.0.1 (localhost)
- Leave port as 7777.
- IMPORTANT!!! Don't choose a team until everyone is in the game.

8) If not hosting, on the right hand side...
- Change IP to the hoster's IPV4 address from Hamachi. 
- Leave port as 7777.
- DO NOT check the custom maps box.
- Click the "Join" button.
