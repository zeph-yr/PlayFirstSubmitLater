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

        
        public void Update()
        {
            //Logger.log.Debug("In Update!");

            if (audiocontroller != null && songcontroller != null)
            {
                //Logger.log.Debug("Current:" + audiocontroller.songTime.ToString());

                if (Config.UserConfig.mod_enabled)
                {
                    if (audiocontroller.songTime >= pausetime && !paused_yet)
                    {
                        //Logger.log.Debug("#####################");
                        songcontroller.PauseSong();
                        //Logger.log.Debug("Song Paused");

                        CancelButtonViewController.Instance.ShowButton();
                        paused_yet = true;
                    }

                    // Debug:
                    //else if (audiocontroller.songTime >= pausetime + 0.2f)
                    //{
                    //    Logger.log.Debug("#############################################################");
                    //}
                }
            }
        }
    }
}