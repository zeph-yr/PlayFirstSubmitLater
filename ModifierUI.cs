using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using System;
using System.ComponentModel;
using Zenject;

namespace PlayFirst
{
    public class ModifierUI : IInitializable, IDisposable, INotifyPropertyChanged
    {
        private string nf_col;
        private string tm_col;
        private string disable_col;

        public event PropertyChangedEventHandler PropertyChanged;

        public ModifierUI()
        {
            if (PluginConfig.Instance.nfprotection_enabled)
                nf_col = "<#00ff00>Better NoFail";
            else
                nf_col = "<#ffffff>Better NoFail";

            if (PluginConfig.Instance.songduration_enabled)
                tm_col = "<#ffff00>Minimum Song Duration";
            else
                tm_col = "<#ffffff>Minimum Song Duration";

            if (PluginConfig.Instance.neversubmit_enabled)
                disable_col = "<#ff0000>Disable All Score Submission";
            else
                disable_col = "<#ffffff>Disable All Score Submission";
        }

        public void Initialize()
        {
            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.AddTab("PlayFirst", "PlayFirst.modifierUI.bsml", this);
        }

        public void Dispose()
        {
            BeatSaberMarkupLanguage.GameplaySetup.GameplaySetup.instance.RemoveTab("PlayFirst");
        }


        [UIValue("nf_color")]
        public string NF_Color
        {
            get => nf_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NF_Color)));
            }
        }

        [UIValue("tm_color")]
        public string TM_Color
        {
            get => tm_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TM_Color)));
            }
        }

        [UIValue("disable_color")]
        public string Disable_Color
        {
            get => disable_col;
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Disable_Color)));
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


        [UIValue("trollmap_enabled")]
        public bool Trollmap_Enabled
        {
            get => PluginConfig.Instance.songduration_enabled;
            set
            {
                PluginConfig.Instance.songduration_enabled = value;
            }
        }
        [UIAction("set_trollmap_enabled")]
        void Set_Trollmap_Enabled(bool value)
        {
            Trollmap_Enabled = value;

            if (value)
            {
                tm_col = "<#ffff00>Minimum Song Duration";
                TM_Color = "changed";
            }

            else
            {
                tm_col = "<#ffffff>Minimum Song Duration";
                TM_Color = "changed";
            }
        }

        [UIValue("min_time")]
        private int Min_Time => PluginConfig.Instance.songduration_min_time;
        [UIValue("max_time")]
        private int Max_Time => PluginConfig.Instance.songduration_max_time;

        [UIComponent("trollmap_slider")]
        public SliderSetting Trollmap_Slider;
        [UIValue("trollmap_value")]
        public float Trollmap_Value
        {
            get => PluginConfig.Instance.songduration_threshold;
            set
            {
                PluginConfig.Instance.songduration_threshold = value;
            }
        }
        [UIAction("set_trollmap_value")]
        public void Set_Trollmap_Value(float value)
        {
            Trollmap_Value = value;
        }


        [UIValue("nfprotection_enabled")]
        public bool Nf_Enabled
        {
            get => PluginConfig.Instance.nfprotection_enabled;
            set
            {
                PluginConfig.Instance.nfprotection_enabled = value;
            }
        }
        [UIAction("set_nfprotection_enabled")]
        void Set_Nf_Enabled(bool value)
        {
            Nf_Enabled = value;

            if (value)
            {
                nf_col = "<#00ff00>Better NoFail";
                NF_Color = "changed";
            }

            else
            {
                nf_col = "<#ffffff>Better NoFail";
                NF_Color = "changed";
            }
        }


        [UIValue("neversubmit_enabled")]
        public bool Neversubmit_Enabled
        {
            get => PluginConfig.Instance.neversubmit_enabled;
            set
            {
                PluginConfig.Instance.neversubmit_enabled = value;
            }
        }
        [UIAction("set_neversubmit_enabled")]
        void Set_Never_Enabled(bool value)
        {
            Neversubmit_Enabled = value;
            if (value)
            {
                disable_col = "<#ff0000>Disable All Score Submission";
                Disable_Color = "changed";
            }

            else
            {
                disable_col = "<#ffffff>Disable All Score Submission";
                Disable_Color = "changed";
            }
        }
    }
}