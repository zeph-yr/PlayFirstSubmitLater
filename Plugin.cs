using HarmonyLib;
using IPA;
using System;
using System.Linq;
using UnityEngine;
using BS_Utils;
using System.Reflection;

namespace PlayFirst
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public const string HarmonyId = "com.Zephyr.PlayFirst";
        public static Harmony harmony = new Harmony(HarmonyId);

        public static bool disable_this_run = false;
        public static CancelScore cancelscore;


        [Init]
        public void Init(IPA.Logging.Logger logger)
        {
            Logger.log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Logger.log.Debug("PlayFirst On Start");

            Config.Read();

            BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
            BS_Utils.Utilities.BSEvents.energyReachedZero += BSEvents_energyReachedZero;
            BS_Utils.Utilities.BSEvents.menuSceneLoaded += BSEvents_menuSceneLoaded;

            //BS_Utils.Utilities.BSEvents.menuSceneActive += BSEvents_menuSceneActive;
            //BS_Utils.Utilities.BSEvents.gameSceneActive += BSEvents_gameSceneActive;

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", ModifierUI.instance);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

            harmony.PatchAll(Assembly.GetExecutingAssembly());

            GameObject cancelscore = new GameObject("CancelScore");
            cancelscore.AddComponent<CancelScore>();
            GameObject.DontDestroyOnLoad(cancelscore);
        }

        private void BSEvents_menuSceneLoaded()
        {
            if (CancelScore.audiocontroller != null)
            {
                CancelScore.audiocontroller = null;
                Logger.log.Debug("audio controll back to null");
            }
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            Config.Write();
        }

        private void BSEvents_gameSceneLoaded()
        {
            Logger.log.Debug("In Map");

            if (Config.UserConfig.neversubmit_enabled)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("All Submission Disabled");
                Logger.log.Debug("All submission disabled");
            }

            else if (Config.UserConfig.mod_enabled)
            {
                Logger.log.Debug("Submit Later enabled");

                CancelScore.paused_yet = false;
                CancelScore.songcontroller = Resources.FindObjectsOfTypeAll<SongController>().FirstOrDefault();

                if (CancelScore.songcontroller != null)
                {
                    Logger.log.Debug("songcontroller found!!!!");
                }
                //CancelScore.audiocontroller = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().First();
                /*
                if (cancelscore.songcontroller != null)
                {
                    Logger.log.Debug(CancelScore.songcontroller.ToString());
                }

                if (cancelscore.audiocontroller!= null)
                {
                    Logger.log.Debug(CancelScore.audiocontroller.songTime.ToString());
                }*/
            }

            Logger.log.Debug("End GameSceneLoaded");
        }

        private void BSEvents_energyReachedZero()
        {
            Logger.log.Debug("Map Failed");

            // No need to check if NF is on, same as just disabling submission whenever player fails LOL
            if (Config.UserConfig.mod_enabled && Config.UserConfig.nfprotection_enabled)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("PFSL NF Protection");
                Logger.log.Debug("NF Protection kicked in");
            }
        }




        [OnExit]
        public void OnApplicationQuit()
        {
            Config.Write();
        }
    }
}

//###########################################################################################
/*private void BSEvents_menuSceneActive()
{
    Logger.log.Debug("MenuScene Active");
    Logger.log.Debug(disable_this_run.ToString());

    if (Config.UserConfig.mod_enabled)
    {
        Logger.log.Debug("Setting up Result UI");
        ResultUI.instance.Setup();
        Logger.log.Debug("Done setting up Result UI");
    }

    Logger.log.Debug("MenuScene After Result UI");
    Logger.log.Debug(disable_this_run.ToString());

    if (disable_this_run)
    {
        BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("PFSL Cancelled");
        Logger.log.Debug("MenuScene disable score done");
    }

    // Reset for next run
    disable_this_run = false;
    Logger.log.Debug(disable_this_run.ToString());
    Logger.log.Debug("MenuScene done");
}*/