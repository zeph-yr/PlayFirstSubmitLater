using IPA.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlayFirst
{
    internal class BeatLeaderInterop
    {
        public static bool IsBeatLeaderReplay()
        {
            PluginMetadata BeatLeader = PluginManager.GetPluginFromId("BeatLeader");
            if (BeatLeader != null)
            {
                Type ReplayerLauncher = BeatLeader.Assembly.GetType("BeatLeader.Replayer.ReplayerLauncher");
                if (ReplayerLauncher == null)
                    return false;

                PropertyInfo IsStartedAsReplay = ReplayerLauncher.GetProperty("IsStartedAsReplay", BindingFlags.Static | BindingFlags.Public);
                if (IsStartedAsReplay == null)
                    return false;

                return (bool)IsStartedAsReplay.GetValue(null);
            }
            return false;
        }
    }
}
