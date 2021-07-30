using HarmonyLib;

namespace PlayFirst
{
    [HarmonyPatch(typeof(AudioTimeSyncController))]
    [HarmonyPatch("StartSong")]
    class AudioTimeSyncControllerPatch
    {
        /*static void Prefix(AudioTimeSyncController __instance)
        {
            Logger.log.Debug("In Patch Prefix");

            Logger.log.Debug("Song Length: " + __instance.songLength.ToString());
            Logger.log.Debug("Song EndTime: " + __instance.songEndTime.ToString());
            Logger.log.Debug("Song Time: " + __instance.songTime.ToString());

            CancelScore.audiocontroller = __instance;
            //CancelScore.songendtime = __instance.songEndTime;
            //CancelScore.songlength = __instance.songLength;

            Logger.log.Debug("End Patch Prefix");
        }*/

        static void Postfix(AudioTimeSyncController __instance)
        {
            Logger.log.Debug("In Patch Postfix");

            CancelScore.audiocontroller = __instance;
            Logger.log.Debug("Song Length: " + __instance.songLength.ToString());
            Logger.log.Debug("Song EndTime: " + __instance.songEndTime.ToString());
            Logger.log.Debug("Song Time: " + __instance.songTime.ToString());

            CancelScore.pausetime = __instance.songEndTime - 1f;
            Logger.log.Debug(CancelScore.pausetime.ToString());

            Logger.log.Debug("End Patch Postfix");
        }

        /*static void Postfix(AudioTimeSyncController __instance)
        {

            if (__instance.GetField< )
            {
                AutoPauseStealthController.ScoreController.enabled = false;
                AutoPauseStealthController.SongController.PauseSong();
                Logger.log?.Debug($"AutoPauseStealthController.StabilityPeriodActive is true " +
                                  $"=> Pausing game right after AudioTimeSyncControllerPatch::StartSong()");
            }

            return;
        }*/
    }

    /*[HarmonyPatch(typeof(SongController)), "Init"]
    class SongControllerPatch
    {
        static void Postfix(SongController __instance) //Apparently you cant use Postfix
        {
            Logger.log.Debug("In SongController Patch");
            //CancelScore.songcontroller = __instance;
            Logger.log.Debug(__instance.ToString());

        }
    }*/

    [HarmonyPatch(typeof(PauseMenuManager))]
    [HarmonyPatch("ShowMenu")]
    class PauseMenuManagerPatch
    {
        static void Postfix(PauseMenuManager __instance)
        {
            /*if (!AutoPauseStealthController.IsMultiplayer)
            {
                AutoPauseStealthController.instance.OnPauseShowMenu();
            }
            return;*/
        }
    }
}