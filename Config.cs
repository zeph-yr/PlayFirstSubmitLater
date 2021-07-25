using Newtonsoft.Json;
using System.IO;

namespace PlayFirst
{
    public class PlayFirstConfig
    {
        public bool mod_enabled = true;
        public bool nfprotection_enabled = true;
        public bool neversubmit_enabled = false;

        public PlayFirstConfig()
        {

        }
        [JsonConstructor]
        public PlayFirstConfig(bool mod_enabled, bool nfprotection_enabled, bool neversubmit_enabled)
        {
            this.mod_enabled = mod_enabled;
            this.nfprotection_enabled = nfprotection_enabled;
            this.neversubmit_enabled = neversubmit_enabled;
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
}