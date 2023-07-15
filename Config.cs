/*using Newtonsoft.Json;
using System.IO;

namespace PlayFirst
{
    public class PlayFirstConfig
    {
        public bool mod_enabled = true;
        public bool nfprotection_enabled = true;
        public bool neversubmit_enabled = false;

        public bool trollmap_enabled = false;
        public float trollmap_threshold = 60f;
        public int trollmap_min_time = 0;
        public int trollmap_max_time = 120;

        public PlayFirstConfig()
        {

        }
        [JsonConstructor]
        public PlayFirstConfig(bool mod_enabled, bool nfprotection_enabled, bool neversubmit_enabled,
            bool trollmap_enabled, float trollmap_threshold, int trollmap_min_time, int trollmap_max_time)

        {
            this.mod_enabled = mod_enabled;
            this.nfprotection_enabled = nfprotection_enabled;
            this.neversubmit_enabled = neversubmit_enabled;

            this.trollmap_enabled = trollmap_enabled;
            this.trollmap_threshold = trollmap_threshold;
            this.trollmap_min_time = trollmap_min_time;
            this.trollmap_max_time = trollmap_max_time;
        }
    }

    public class Config
    {
        public static PlayFirstConfig UserConfig { get; private set; }
        public static string ConfigPath { get; private set; } = Path.Combine(IPA.Utilities.UnityGame.UserDataPath, "PlayFirst.json");

        public static void Read()
        {
            if (!File.Exists(ConfigPath))
            {
                UserConfig = new PlayFirstConfig();
                Write();
            }
            else
            {
                UserConfig = JsonConvert.DeserializeObject<PlayFirstConfig>(File.ReadAllText(ConfigPath));
            }
        }

        public static void Write()
        {
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(UserConfig, Formatting.Indented));
        }
    }
}*/