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
        public static PauseMenuManager pausemenu;

        public void Awake()
        {
            SubmitLater.paused_yet = false;

            CancelButtonViewController.Instance.ShowButton(); // Putting this in Plugin.OnApplicationStart crashes it (No button comes up ever)

            audiocontroller = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().LastOrDefault();
            pausetime = audiocontroller.songEndTime - 0.5f;

            songcontroller = Resources.FindObjectsOfTypeAll<SongController>().LastOrDefault();

            pausemenu = Resources.FindObjectsOfTypeAll<PauseMenuManager>().LastOrDefault();
            pausemenu.didPressContinueButtonEvent += Pausemenu_didPressContinueButtonEvent;
        }

        private void Pausemenu_didPressContinueButtonEvent()
        {
            CancelButtonViewController.Instance.cancelbutton_screen.gameObject.SetActive(false);
            songcontroller.ResumeSong();
        }

        // Auto Pause at very end of map so you can decide
        public void Update()
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
                        songcontroller.PauseSong();
                        pausemenu.ShowMenu();
                        CancelButtonViewController.Instance.cancelbutton_screen.gameObject.SetActive(true);

                    //Logger.log.Debug("Song Paused");
                }
                    //else
                    //    Logger.log.Debug("In Song: Timing");
                }
                //else
                //    Logger.log.Debug("Not timing"); // Tested: it's not running in menu after object destroyed :)
            //}
        }

        public void OnDestroy()
        {
            pausemenu.didPressContinueButtonEvent -= Pausemenu_didPressContinueButtonEvent;
            GameObject.Destroy(pausemenu);
            GameObject.Destroy(audiocontroller);
            GameObject.Destroy(songcontroller);
        }
    }
}

// Notes / TODO:
// Maps with blocks outside the map. is that songlength or song end time?
// Best pausetime
// Move button to right place
// Button color
// Hover hints (if releasing), sign it.
// Multiplayer: disable all score submissions?
// Campaign: disable everything, save resources
// If disable all score turned on, Turn menu RED
// Turn menu green if NF protection on

//Why does clicking continue do nothing?