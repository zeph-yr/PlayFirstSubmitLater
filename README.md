# PlayFirst(SubmitLater)
**Allows for automated elective score submission, including preventing submission of failed NF scores. For BS 1.16.4+**

Ever received trolly requests that you regret entertaining and now you have crap all over your ScoreSaber? Or is 14star OV forever dragging down your rank acc because your friends ran the map in MP (back in the day) while you went to eat? Now you can stop it from happening again! 
This mod means you can play all the shitmaps and practice high stars with reckless abandon. No longer will you scream when finally pass OV but it was in practice mode or when you didn't manage to click Pause fast enough to quit a bad map.

## New Features for v2.0.0+ (1.29.0+ Only)
- Option to always show and use Pause Menu UI, independent of any toggles
- Option to reposition Pause Menu UI

## How To Use
- **Pause Menu UI:** Option to turn off submission from the Pause Menu. Button shows whether your current run will be submitted. Click twice (safety feature) to disable score for your current run. Your decision for the run is final so choose wisely :)
  - **Pre-v2.0.0:** To enable UI, toggle ON `Submit Later` and go into a map at least once (not required to keep toggled thereafter).
  - **v2.0.0+:** Set `always_show_on_pause` to `true` in `UserData/PlayFirst.json`, or toggle ON `Submit Later`.
- **Submit Later:** Automatically pauses at the end of the map so you can make your decision. _You can disable score and still keep your BeatSavior/Slice Visualizer/etc data if you click continue and finish the map._ Automatic pausing is only for Solo/Party Mode.
  - **Note:** If your map has blocks in the last fractions of a second, Submit Later will still pause if toggled ON. This will never be an issue for Rank, for shitmaps use your judgement :)
- **Better NoFail:** Stops submission if you fail while playing on NF. Not enabled in Campaigns as some missions require NF.
- **Minimum Song Duration:** Automatically turns off submission if a map is shorter than the duration you've set in seconds.
- **Disable All Score Submission:** No scores will be set. For any mode.
- **The toggles "stack".** You can enable any combination so use your judgement.
###
- Place PlayFirst.dll in Plugins folder. Disabled scores will not be set locally or submitted to online leaderboards.
- Requires BSIPA, BSML, BS_Utils. v2.0.0+ requires SiraUtil **in addition**.
- The config for v2.0.0+ is incompatible with pre-v2.0.0 versions. Delete `UserData/PlayFirst.json` and allow the mod to generate a new one.
  
## To reposition the Pause Menu UI:
- Set `moveable_panel` to `true` in  `UserData/PlayFirst.json` and restart game. Reposition the menu by dragging the glowing cube on the right side.
- To lock the menu in place and prevent accidental repositioning: Close game and set `moveable_panel` back to `false`.
- In the event the menu becomes stuck in some place irretrievable, it can be reset to the default position: Set `reset_panel` to `true` and restart the game. To reposition the menu again and save custom positions: Close game, set `reset_panel` back to `false` and restart game.
- _Manually editing the position and rotation values is highly discouraged. Should you still choose to go down this path and you ruin the config, delete the file and restart the game._

###
![screenshot](https://github.com/zeph-yr/PlayFirstSubmitLater/blob/BS_1.31.0/Screenshots/1.1.0_menu_2.png)
![screenshot](https://github.com/zeph-yr/PlayFirstSubmitLater/blob/main/menu_1a_small.png)

## About
This is my second mod and written mostly from scratch. If you enjoy this, please feel free to let me know :) I appreciate your feedback!
