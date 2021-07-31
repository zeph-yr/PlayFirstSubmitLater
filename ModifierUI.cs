using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using HMUI;
using UnityEngine;

namespace PlayFirst
{
    public class ModifierUI : NotifiableSingleton<ModifierUI>
    {
        [UIValue("mod_enabled")]
        public bool Mod_Enabled
        {
            get => Config.UserConfig.mod_enabled;
            set
            {
                Config.UserConfig.mod_enabled = value;
            }
        }
        [UIAction("set_mod_enabled")]
        void Set_Mod_Enabled(bool value)
        {
            Mod_Enabled = value;
        }

        [UIValue("nfprotection_enabled")]
        public bool Nf_Enabled
        {
            get => Config.UserConfig.nfprotection_enabled;
            set
            {
                Config.UserConfig.nfprotection_enabled = value;
            }
        }
        [UIAction("set_nfprotection_enabled")]
        void Set_Nf_Enabled(bool value)
        {
            Nf_Enabled = value;
        }

        [UIValue("neversubmit_enabled")]
        public bool Neversubmit_Enabled
        {
            get => Config.UserConfig.neversubmit_enabled;
            set
            {
                Config.UserConfig.neversubmit_enabled = value;
            }
        }
        [UIAction("set_neversubmit_enabled")]
        void Set_Never_Enabled(bool value)
        {
            Neversubmit_Enabled = value;
        }
    }
}