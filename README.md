# Mio Cid (unity project)

This repo will contain the source code for the game Mio Cid, a multiplayer ARPG framework.

This project now has an [Itch.io page](https://tortitas.itch.io/mio-cid-v3).

## Resources

- Unity version _2020.3.34f1_
- For multiplayer the game uses a _Node 16.14.2_ server hosted on [another repo](https://github.com/TortitasT/Cid-Server) on this account.
- For connecting to said server the game uses [SocketIO V4 Client](https://assetstore.unity.com/packages/tools/network/socket-io-v3-v4-client-for-unity-standalone-webgl-196557) asset from asset store, shoutout to support for helping me out!
- To make the storyline the game will use ink with it's editor [inky](https://www.inklestudios.com/ink/), a handy and flexible storyline maker with integration in unity so the players can make their own campaigns. Each campaign will have an .ink file for each quest with knots for each npc. Players will load progress of every quest on connection. [Documentation](https://github.com/inkle/ink/blob/master/Documentation/RunningYourInk.md#getting-started-with-the-runtime-api)
- To make the campaigns the players will need a map editor, it will be coded to store tiles on a json and load them on runtime. Included on base game (this project).
- To make the graphics I use a technique from the first two diablo games where I make 3d models (using [picoCAD](https://johanpeitz.itch.io/picocad)) and I import them into blender to animate them or whatever, then we render the frames into images for (currently) 8 directions.
- The game uses [Text Animator for Unity](https://assetstore.unity.com/packages/tools/gui/text-animator-for-unity-158707) to display text on dialogs, it's great!
- To get fonts [OpenFoundry](https://open-foundry.com/fonts)
- Json tool for Unity C# [Newtonsoft-Json](https://github.com/jilleJr/Newtonsoft.Json-for-Unity) [Documentation](https://www.newtonsoft.com/json/help/html/Introduction.htm), to use it import package from git and type _com.unity.nuget.newtonsoft-json@3.0_
- [Kenney assets](https://opengameart.org/users/kenney) are all great so all of them are resources :)

## Credits

- [EB-Garamond](https://github.com/georgd/EB-Garamond) Font
- [Inkle Studios](https://www.inklestudios.com/) Ink language
- [Johan Peitz](https://johanpeitz.itch.io/) picoCAD
- [Kenney](https://kenney.itch.io/) Assets
