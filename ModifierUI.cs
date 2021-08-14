﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using HMUI;
using UnityEngine;

namespace PlayFirst
{
    public class ModifierUI : NotifiableSingleton<ModifierUI>
    {
        private string nf_col;
        private string disable_col;

        public ModifierUI()
        {
            if (Config.UserConfig.nfprotection_enabled)
                nf_col = "<#00ff00>NoFail Protection";
            else
                nf_col = "<#ffffff>NoFail Protection";

            if (Config.UserConfig.neversubmit_enabled)
                disable_col = "<#ff0000>Disable ALL Score Submission";
            else
                disable_col = "<#ffffff>Disable ALL Score Submission";
        }
        
        [UIValue("nf_color")]
        public  string NF_Color

        {
            get => nf_col;
            set
            {
                NotifyPropertyChanged();
            }
        }

        [UIValue("disable_color")]
        public string Disable_Color
        {
            get => disable_col;
            set
            {
                NotifyPropertyChanged();
            }
        }

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

            if (value)
            {
                nf_col = "<#00ff00>NoFail Protection";
                NF_Color = "changed";
            }
                
            else
            {
                nf_col = "<#ffffff>NoFail Protection";
                NF_Color = "changed";
            }
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
            if (value)
            {
                disable_col = "<#ff0000>Disable ALL Score Submission";
                Disable_Color = "changed";
            }
               
            else
            {
                disable_col = "<#ffffff>Disable ALL Score Submission";
                Disable_Color = "changed";
            }
        }
    }
}