using System.Linq;
using UnityEngine;

namespace PlayFirst
{
    public class SubmitLater : MonoBehaviour
    {
        public static float pausetime = 0.1f;
        public static bool paused_yet = false;

        public static AudioTimeSyncController audiocontroller;
        public static SongController songcontroller;
        //public static PauseMenuManager pausemenu;
        public static PauseController pausecontroller;

        public void Awake()
        {
            SubmitLater.paused_yet = false;

            CancelButtonViewController.Instance.ShowButton(); // Putting this in Plugin.OnApplicationStart crashes it (No button comes up ever)

            audiocontroller = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().LastOrDefault();
            pausetime = audiocontroller.songEndTime - 0.25f;
            pausecontroller = Resources.FindObjectsOfTypeAll<PauseController>().LastOrDefault();

            // Notes: Don't need these anymore. Calling PauseController.Pause() is the game's pause functionality
            //songcontroller = Resources.FindObjectsOfTypeAll<SongController>().LastOrDefault();
            //pausemenu = Resources.FindObjectsOfTypeAll<PauseMenuManager>().LastOrDefault();
            //pausemenu.didPressContinueButtonEvent += Pausemenu_didPressContinueButtonEvent;
        }

        // Notes: Using PauseMenuManager.ShowMenu() with this makes it very hard to click in VR (it's not apparent in FPFC)
        /*private void Pausemenu_didPressContinueButtonEvent()
        {
            CancelButtonViewController.Instance.cancelbutton_screen.gameObject.SetActive(false);
            songcontroller.ResumeSong();
        }*/

        // Auto Pause at very end of map so you can decide
        public void LateUpdate()
        {
            //Logger.log.Debug("In Update!");

            //if (audiocontroller != null && songcontroller != null)
            //{
                //Logger.log.Debug("Current:" + audiocontroller.songTime.ToString());

            if (Config.UserConfig.mod_enabled && !paused_yet)
            {
                if (audiocontroller.songTime >= pausetime)
                {
                    paused_yet = true;
                    pausecontroller.Pause();

                    // Notes: PauseSong in SongController pauses map but isn't the whole "Pause" functionality
                    // Doesn't bring up menu, continue button won't work either.
                    // Call PauseController.Pause() instead:

                    //songcontroller.PauseSong();
                    //pausemenu.ShowMenu();
                    //CancelButtonViewController.Instance.cancelbutton_screen.gameObject.SetActive(true);

                    Logger.log.Debug("Paused at end of map");
                }
                //else
                //    Logger.log.Debug("In Song: Timing");
            }
                //else
                //    Logger.log.Debug("Not timing"); // Tested: it's not running in menu after object destroyed :)
            //}
        }

        /*public void OnDestroy()
        {
            //pausemenu.didPressContinueButtonEvent -= Pausemenu_didPressContinueButtonEvent;
            //GameObject.Destroy(pausemenu);
            GameObject.Destroy(audiocontroller);
            GameObject.Destroy(pausecontroller);
            //GameObject.Destroy(songcontroller);
        }*/
    }
}

// Notes / TODO:
// Done: Maps with blocks outside the map. is that songlength or song end time?
// Done: Best pausetime
// Multiplayer: disable all score submissions
// Campaign: disable everything, save resources