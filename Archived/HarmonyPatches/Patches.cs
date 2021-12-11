/*using HarmonyLib;

namespace PlayFirst
{
    [HarmonyPatch(typeof(AudioTimeSyncController))]
    [HarmonyPatch("StartSong")]
    class AudioTimeSyncControllerPatch
    {
        static void Postfix(AudioTimeSyncController __instance)
        {
            if (Plugin.submitlater != null)
            {
                //Logger.log.Debug("In Patch Postfix");

                SubmitLater.audiocontroller = __instance;
                //Logger.log.Debug("Song Length: " + __instance.songLength.ToString());
                //Logger.log.Debug("Song EndTime: " + __instance.songEndTime.ToString());
                //Logger.log.Debug("Song Time: " + __instance.songTime.ToString());

                SubmitLater.pausetime = __instance.songEndTime - 0.5f;
                //Logger.log.Debug(SubmitLater.pausetime.ToString());
            }
            //Logger.log.Debug("End Patch Postfix");
        }

    }
}*/

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