using TMPro;
using System.Linq;
using UnityEngine;
using IPA.Utilities;
using System.Collections;
using BeatSaberMarkupLanguage;
using HarmonyLib;
using System.Reflection;
using HMUI;
using UnityEngine.UI;

namespace PlayFirst
{
    public class SubmitLater : MonoBehaviour
    {
        public static float pausetime = 0.1f;
        public static bool paused_yet = false;

        public static AudioTimeSyncController audiocontroller;
        public static SongController songcontroller;

        public void Awake()
        {
            // Putting this in Plugin.OnApplicationStart crashes it (No button comes up ever)
            CancelButtonViewController.Instance.ShowButton();
        }
        
        // Auto Pause at very end of map so you can decide
        public void Update()
        {
            //Logger.log.Debug("In Update!");

            if (audiocontroller != null && songcontroller != null)
            {
                //Logger.log.Debug("Current:" + audiocontroller.songTime.ToString());

                if (Config.UserConfig.mod_enabled && !paused_yet)
                {
                    if (audiocontroller.songTime >= pausetime)
                    {
                        //Logger.log.Debug("#####################");
                        songcontroller.PauseSong();
                        paused_yet = true;

                        //Logger.log.Debug("Song Paused");
                    }
                    //else
                    //    Logger.log.Debug("In Song: Timing");
                }
                //else
                //    Logger.log.Debug("Not timing"); // Tested: it's not running in menu after object destroyed :)
            }
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