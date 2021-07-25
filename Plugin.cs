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
        public static bool disable_this_run = false;

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

            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", ModifierUI.instance);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            Config.Write();
        }

        private void BSEvents_gameSceneLoaded()
        {
            /*
            bool WillOverride = BS_Utils.Plugin.LevelData.IsSet && !BS_Utils.Gameplay.Gamemode.IsIsolatedLevel
                && Config.UserConfig.enabled && BS_Utils.Plugin.LevelData.Mode == BS_Utils.Gameplay.Mode.Standard && BS_Utils.Plugin.LevelData.GameplayCoreSceneSetupData.practiceSettings == null;
            if(WillOverride && false) // false is from "!Config.User.Config.dontForceNJS"
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("JDFixer");
            */

            Logger.log.Debug("In Map");

            if (Config.UserConfig.mod_enabled && Config.UserConfig.neversubmit_enabled)
            {
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("PFSL All Submissions Disabled");
                Logger.log.Debug("All submission disabled");
            }
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
