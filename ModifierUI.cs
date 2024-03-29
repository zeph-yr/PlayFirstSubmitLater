﻿using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Parser;
using System;
using System.ComponentModel;
using Zenject;

namespace PlayFirst
{
    internal sealed class ModifierUI : IInitializable, IDisposable, INotifyPropertyChanged
    {
        private string bnf_col;
        private string sd_col;
        private string das_col;

        public event PropertyChangedEventHandler PropertyChanged;

        private ModifierUI()
        {
            if (PluginConfig.Instance.betternofail_enabled)
                bnf_col = "<#00ff00>Better NoFail";
            else
                bnf_col = "<#ffffff>Better NoFail";

            if (PluginConfig.Instance.songduration_enabled)
                sd_col = "<#ffff00>Minimum Song Duration";
            else
                sd_col = "<#ffffff>Minimum Song Duration";

            if (PluginConfig.Instance.disableallscores_enabled)
                das_col = "<#ff0000>Disable All Score Submission";
            else
                das_col = "<#ffffff>Disable All Score Submission";
        }

        public void Initialize()
        {
            GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", this);
            Donate.Refresh_Text();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Update_Dynamic)));
        }

        public void Dispose()
        {
            if (GameplaySetup.instance != null)
            {
                GameplaySetup.instance.RemoveTab("PlayFirst");
            }
        }


        [UIValue("bnf_color")]
        private string BNF_Color
        {
            get => bnf_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BNF_Color)));
            }
        }

        [UIValue("sd_color")]
        private string SD_Color
        {
            get => sd_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SD_Color)));
            }
        }

        [UIValue("das_color")]
        private string DAS_Color
        {
            get => das_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DAS_Color)));
            }
        }

        [UIValue("mod_enabled")]
        private bool Mod_Enabled
        {
            get => PluginConfig.Instance.submitlater_enabled;
            set
            {
                PluginConfig.Instance.submitlater_enabled = value;
            }
        }
        [UIAction("set_mod_enabled")]
        private void Set_Mod_Enabled(bool value)
        {
            Mod_Enabled = value;
        }

        [UIValue("betternofail_enabled")]
        private bool BetterNoFail_Enabled
        {
            get => PluginConfig.Instance.betternofail_enabled;
            set
            {
                PluginConfig.Instance.betternofail_enabled = value;
            }
        }
        [UIAction("set_betternofail_enabled")]
        private void Set_BetterNoFail_Enabled(bool value)
        {
            BetterNoFail_Enabled = value;

            if (value)
            {
                bnf_col = "<#00ff00>Better NoFail";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BNF_Color)));
            }
            else
            {
                bnf_col = "<#ffffff>Better NoFail";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BNF_Color)));
            }
        }

        [UIValue("songduration_enabled")]
        private bool SongDuration_Enabled
        {
            get => PluginConfig.Instance.songduration_enabled;
            set
            {
                PluginConfig.Instance.songduration_enabled = value;
            }
        }
        [UIAction("set_songduration_enabled")]
        private void Set_SongDuration_Enabled(bool value)
        {
            SongDuration_Enabled = value;

            if (value)
            {
                sd_col = "<#ffff00>Minimum Song Duration";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SD_Color)));
            }
            else
            {
                sd_col = "<#ffffff>Minimum Song Duration";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SD_Color)));
            }
        }

        [UIValue("min_time")]
        private int Min_Time => PluginConfig.Instance.songduration_min_time;
        [UIValue("max_time")]
        private int Max_Time => PluginConfig.Instance.songduration_max_time;
        [UIComponent("songduration_slider")]
        public SliderSetting SongDuration_Slider;

        [UIValue("songduration_value")]
        private float SongDuration_Value
        {
            get => PluginConfig.Instance.songduration_threshold;
            set
            {
                PluginConfig.Instance.songduration_threshold = value;
            }
        }
        [UIAction("set_songduration_value")]
        private void Set_SongDuration_Value(float value)
        {
            SongDuration_Value = value;
        }

        [UIValue("disableallscores_enabled")]
        private bool DisableAllScores_Enabled
        {
            get => PluginConfig.Instance.disableallscores_enabled;
            set
            {
                PluginConfig.Instance.disableallscores_enabled = value;
            }
        }
        [UIAction("set_disableallscores_enabled")]
        private void Set_DisableAllScores_Enabled(bool value)
        {
            DisableAllScores_Enabled = value;
            if (value)
            {
                das_col = "<#ff0000>Disable All Score Submission";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DAS_Color)));
            }
            else
            {
                das_col = "<#ffffff>Disable All Score Submission";
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DAS_Color)));
            }
        }


        //===============================================================

        [UIValue("open_donate_text")]
        private string Open_Donate_Text => Donate.donate_clickable_text;

        [UIValue("open_donate_hint")]
        private string Open_Donate_Hint => Donate.donate_clickable_hint;

        [UIParams]
        private BSMLParserParams parserParams;

        [UIAction("open_donate_modal")]
        private void Open_Donate_Modal()
        {
            parserParams.EmitEvent("hide_donate_modal");
            Donate.Refresh_Text();
            parserParams.EmitEvent("show_donate_modal");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Modal_Text_Dynamic)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Donate_Modal_Hint_Dynamic)));
        }

        private void Open_Donate_Patreon()
        {
            Donate.Patreon();
        }
        private void Open_Donate_Kofi()
        {
            Donate.Kofi();
        }

        [UIValue("donate_modal_text_static_1")]
        private string Donate_Modal_Text_Static_1 => Donate.donate_modal_text_static_1;

        [UIValue("donate_modal_text_static_2")]
        private string Donate_Modal_Text_Static_2 => Donate.donate_modal_text_static_2;

        [UIValue("donate_modal_text_dynamic")]
        private string Donate_Modal_Text_Dynamic => Donate.donate_modal_text_dynamic;

        [UIValue("donate_modal_hint_dynamic")]
        private string Donate_Modal_Hint_Dynamic => Donate.donate_modal_hint_dynamic;

        [UIValue("donate_update_dynamic")]
        private string Donate_Update_Dynamic => Donate.donate_update_dynamic;
    }
}