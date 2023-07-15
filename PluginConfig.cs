using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace PlayFirst
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual bool mod_enabled { get; set; } = true;
        public virtual bool betternofail_enabled { get; set; } = true;
        public virtual bool disableallscores_enabled { get; set; } = false;

        public virtual bool songduration_enabled { get; set; } = false;
        public virtual float songduration_threshold { get; set; } = 60f;
        public virtual int songduration_min_time { get; set; } = 0;
        public virtual int songduration_max_time { get; set; } = 120;


        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        public virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        public virtual void CopyFrom(PluginConfig other)
        {

        }
    }
}
