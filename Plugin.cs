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
        public const string HarmonyId = "com.Zephyr.BeatSaber.PlayFirst";
        public static Harmony harmony = new Harmony(HarmonyId);

        public static bool disable_run = false;
        public static GameObject submitlater;
       

        [Init]
        public void Init(IPA.Logging.Logger logger)
        {
            Logger.log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            //Logger.log.Debug("PlayFirst On Start");

            Config.Read();

            BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
            BS_Utils.Utilities.BSEvents.energyReachedZero += BSEvents_energyReachedZero;

            //BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh


            BS_Utils.Utilities.BSEvents.LevelFinished += BSEvents_LevelFinished;

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", ModifierUI.instance);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

            harmony.PatchAll(Assembly.GetExecutingAssembly());

            //CancelButtonViewController.Instance.ShowButton(); // This doesn't work: Always results in error

            //GameObject cancelscore = new GameObject("CancelScore");
            //cancelscore.AddComponent<CancelScore>();
            //GameObject.DontDestroyOnLoad(cancelscore);
        }

        // Destroy game object when reaching menu (finished is not enough because what if you quit to menu without finishing?
        private void BSEvents_LevelFinished(object sender, BS_Utils.Utilities.LevelFinishedEventArgs e)
        {
            if (submitlater != null)
            {
                GameObject.Destroy(submitlater);
            }
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            Config.Write();
        }

        private void BSEvents_gameSceneLoaded()
        {
            //Logger.log.Debug("In Map");

            disable_run = false; // Pause Menu state. Always set to false at start of any map.
            //CancelButtonViewController.Instance.SetVisibility(false); // For Restart from Pause Menu

            if (Config.UserConfig.neversubmit_enabled)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("All Submission Disabled");
                disable_run = true; // Pause Menu state
                
                //Logger.log.Debug("All submission disabled");
            }

            // If all disabled, don't bother with this :)
            else if (Config.UserConfig.mod_enabled)
            {
                //Logger.log.Debug("Submit Later enabled");

                submitlater = new GameObject("SubmitLater");
                submitlater.AddComponent<SubmitLater>();
                GameObject.DontDestroyOnLoad(submitlater);

                SubmitLater.paused_yet = false;
                SubmitLater.songcontroller = Resources.FindObjectsOfTypeAll<SongController>().FirstOrDefault();

                //Debug:
                //if (SubmitLater.songcontroller != null)
                //{
                //    Logger.log.Debug("songcontroller found!!!!");
                //}
            }

            //Logger.log.Debug("End GameSceneLoaded");
        }

        private void BSEvents_energyReachedZero()
        {
            //Logger.log.Debug("Map Failed");

            // No need to check if NF is on: Same as just disabling submission whenever player fails LOL
            if (Config.UserConfig.nfprotection_enabled)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("NF Protection");
                disable_run = true;
                
                //Logger.log.Debug("NF Protection kicked in");
            }
        }


        [OnExit]
        public void OnApplicationQuit()
        {
            Config.Write();
        }
    }
}