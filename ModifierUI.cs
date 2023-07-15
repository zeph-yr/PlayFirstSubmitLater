using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using System;
using System.ComponentModel;
using Zenject;

namespace PlayFirst
{
    public class ModifierUI : IInitializable, IDisposable, INotifyPropertyChanged
    {
        private string bnf_col;
        private string sd_col;
        private string das_col;

        public event PropertyChangedEventHandler PropertyChanged;

        public ModifierUI()
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
            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", this);
        }

        public void Dispose()
        {
            if (BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance != null)
            {
                BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.RemoveTab("PlayFirst");
            }
        }


        [UIValue("bnf_color")]
        public string BNF_Color
        {
            get => bnf_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BNF_Color)));
            }
        }

        [UIValue("sd_color")]
        public string SD_Color
        {
            get => sd_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SD_Color)));
            }
        }

        [UIValue("das_color")]
        public string DAS_Color
        {
            get => das_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DAS_Color)));
            }
        }

        [UIValue("mod_enabled")]
        public bool Mod_Enabled
        {
            get => PluginConfig.Instance.mod_enabled;
            set
            {
                PluginConfig.Instance.mod_enabled = value;
            }
        }
        [UIAction("set_mod_enabled")]
        void Set_Mod_Enabled(bool value)
        {
            Mod_Enabled = value;
        }

        [UIValue("betternofail_enabled")]
        public bool BetterNoFail_Enabled
        {
            get => PluginConfig.Instance.betternofail_enabled;
            set
            {
                PluginConfig.Instance.betternofail_enabled = value;
            }
        }
        [UIAction("set_betternofail_enabled")]
        void Set_BetterNoFail_Enabled(bool value)
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
        public bool SongDuration_Enabled
        {
            get => PluginConfig.Instance.songduration_enabled;
            set
            {
                PluginConfig.Instance.songduration_enabled = value;
            }
        }
        [UIAction("set_songduration_enabled")]
        void Set_SongDuration_Enabled(bool value)
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
        public float SongDuration_Value
        {
            get => PluginConfig.Instance.songduration_threshold;
            set
            {
                PluginConfig.Instance.songduration_threshold = value;
            }
        }
        [UIAction("set_songduration_value")]
        public void Set_SongDuration_Value(float value)
        {
            SongDuration_Value = value;
        }

        [UIValue("disableallscores_enabled")]
        public bool DisableAllScores_Enabled
        {
            get => PluginConfig.Instance.disableallscores_enabled;
            set
            {
                PluginConfig.Instance.disableallscores_enabled = value;
            }
        }
        [UIAction("set_disableallscores_enabled")]
        void Set_DisableAllScores_Enabled(bool value)
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
    }
}