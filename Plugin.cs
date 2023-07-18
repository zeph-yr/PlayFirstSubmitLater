using IPA;
using IPA.Config;
using IPA.Config.Stores;
using PlayFirst.Installers;
using SiraUtil.Zenject;
using System.Linq;
using UnityEngine;

namespace PlayFirst
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static bool disable_run = false;
        internal static bool confirmed = false;
        internal static GameObject submitlater;
        internal static AudioTimeSyncController tm_audiocontroller;


        [Init]
        public void Init(IPA.Logging.Logger logger, Config conf, Zenjector zenjector)
        {
            Logger.log = logger;
            PluginConfig.Instance = conf.Generated<PluginConfig>();

            zenjector.Install<PlayFirstMenuInstaller>(Location.Menu);
        }

        [OnStart]
        public void OnApplicationStart()
        {
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += BSEvents_gameSceneLoaded;
            BS_Utils.Utilities.BSEvents.energyReachedZero += BSEvents_energyReachedZero;
            BS_Utils.Utilities.BSEvents.menuSceneLoaded += BSEvents_menuSceneLoaded;

            // Notes: Creating this here crashes BS Utils and causes other errors
            //submitlater = new GameObject("SubmitLater");
            //submitlater.AddComponent<SubmitLater>();
            //GameObject.DontDestroyOnLoad(submitlater);

            Donate.Refresh_Text();
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

        private void BSEvents_gameSceneLoaded()
        {
            //Logger.log.Debug("In Map");

            // Pause Menu state. Always set to false at start of any map.
            disable_run = false;
            confirmed = false;

            // Allowed for all modes: Standard, Party, MP, Campaign
            if (PluginConfig.Instance.disableallscores_enabled) 
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("All Scores");
                disable_run = true; // Pause Menu state
                confirmed = true;

                Logger.log.Debug("All submission disabled");
                return;
            }

            // Allowed for Solo and MP only
            if (PluginConfig.Instance.songduration_enabled && 
               (BS_Utils.Plugin.LevelData.Mode == BS_Utils.Gameplay.Mode.Standard || BS_Utils.Plugin.LevelData.Mode == BS_Utils.Gameplay.Mode.Multiplayer))
            {
                tm_audiocontroller = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().LastOrDefault();

                if (tm_audiocontroller.songEndTime <= PluginConfig.Instance.songduration_threshold)
                {
                    BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Song Duration");
                    disable_run = true; // Pause Menu state
                    confirmed = true;

                    Logger.log.Debug("Short map duration");
                    return;
                }
            }

            // Allow only for Standard
            // Disable for MP: Unsure about pausing behaviour in MP
            // Disable for Campaign: Probably annoying if user habitually leaves this toggled on
            // Disable for Party: Probably no use case
            // If all score disabled, don't bother with this :)
            if (PluginConfig.Instance.mod_enabled && BS_Utils.Plugin.LevelData.Mode == BS_Utils.Gameplay.Mode.Standard) 
            {
                //Logger.log.Debug("Submit Later enabled");

                submitlater = new GameObject("SubmitLater");
                submitlater.AddComponent<SubmitLater>();
                GameObject.DontDestroyOnLoad(submitlater);

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
            if (PluginConfig.Instance.betternofail_enabled && BS_Utils.Plugin.LevelData.Mode != BS_Utils.Gameplay.Mode.Mission)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Better NoFail");
                disable_run = true; // Pause Menu state
                confirmed = true;
                
                Logger.log.Debug("Map failed. BetterNoFail kicked in");
            }
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            if (submitlater != null)
            {
                GameObject.Destroy(submitlater);
            }
        }
    }
}