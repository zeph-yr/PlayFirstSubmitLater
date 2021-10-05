using IPA;
using System.Linq;
using UnityEngine;

namespace PlayFirst
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        public static bool disable_run = false;
        public static bool confirmed = false;
        public static GameObject submitlater;
        public static AudioTimeSyncController tm_audiocontroller;


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

            //Harmony harmony = new Harmony("com.Zephyr.BeatSaber.PlayFirst");
            //harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

            BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
            BS_Utils.Utilities.BSEvents.energyReachedZero += BSEvents_energyReachedZero;
            BS_Utils.Utilities.BSEvents.menuSceneLoaded += BSEvents_menuSceneLoaded;

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", ModifierUI.instance);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;

            // Notes: Creating this here crashes BS Utils and causes other errors
            //submitlater = new GameObject("SubmitLater");
            //submitlater.AddComponent<SubmitLater>();
            //GameObject.DontDestroyOnLoad(submitlater);
        }

        // Destroy GameObject when back to menu so it's not running every frame of the menu
        private void BSEvents_menuSceneLoaded() // Check if pause menu counts as menuscene loaded
        {
            //Logger.log.Debug("In Menu");

            if (submitlater != null)
            {
                GameObject.Destroy(submitlater);
                //Logger.log.Debug("Game Object Destroyed");
            }
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            Config.Write();
        }

        private void BSEvents_gameSceneLoaded()
        {
            //Logger.log.Debug("In Map");

            // Pause Menu state. Always set to false at start of any map.
            disable_run = false;
            confirmed = false;

            // Allowed for all modes: Standard, Party, MP, Campaign
            if (Config.UserConfig.neversubmit_enabled) 
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("All Scores");
                disable_run = true; // Pause Menu state
                confirmed = true;

                Logger.log.Debug("All submission disabled");
                return;
            }

            if (Config.UserConfig.trollmap_enabled)
            {
                tm_audiocontroller = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().LastOrDefault();

                if (tm_audiocontroller.songEndTime <= Config.UserConfig.trollmap_threshold)
                {
                    BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Song Duration");
                    disable_run = true; // Pause Menu state
                    confirmed = true;

                    Logger.log.Debug("Short map disabled");
                    return;
                }
            }

            // Allow only for Standard
            // Disable for MP: Unsure about pausing behaviour in MP
            // Disable for Campaign: Probably annoying if user habitually leaves this toggled on
            // Disable for Party: Probably no use case
            // If all score disabled, don't bother with this :)
            if (Config.UserConfig.mod_enabled && BS_Utils.Plugin.LevelData.Mode == BS_Utils.Gameplay.Mode.Standard) 
            {
                //Logger.log.Debug("Submit Later enabled");

                submitlater = new GameObject("SubmitLater");
                submitlater.AddComponent<SubmitLater>();
                GameObject.DontDestroyOnLoad(submitlater);

                // Moved this to SubmitLater
                //SubmitLater.paused_yet = false;
                //SubmitLater.songcontroller = Resources.FindObjectsOfTypeAll<SongController>().FirstOrDefault();

                //Debug:
                //if (SubmitLater.songcontroller != null)
                //{
                //    Logger.log.Debug("songcontroller found!!!!");
                //}
                Logger.log.Debug("SubmitLater enabled");
            }
            //Logger.log.Debug("End GameSceneLoaded");
        }

        private void BSEvents_energyReachedZero()
        {
            //Logger.log.Debug("Map Failed");

            // Allow for all modes: Standard, Party, MP
            // Disable for Campaign: Some missions might have NF as a modifier
            // No need to check if NF is on: Same as just disabling submission whenever player fails LOL
            if (Config.UserConfig.nfprotection_enabled && BS_Utils.Plugin.LevelData.Mode != BS_Utils.Gameplay.Mode.Mission)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("NF Protection");
                disable_run = true;
                confirmed = true;
                
                Logger.log.Debug("Map failed. NF Protection kicked in");
            }
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Config.Write();
        }
    }
}