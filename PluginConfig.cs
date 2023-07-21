using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using UnityEngine;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace PlayFirst
{
    internal class PluginConfig
    {
        internal static PluginConfig Instance { get; set; }

        internal virtual bool submitlater_enabled { get; set; } = true;
        internal virtual bool betternofail_enabled { get; set; } = true;
        internal virtual bool disableallscores_enabled { get; set; } = false;

        internal virtual bool songduration_enabled { get; set; } = false;
        internal virtual float songduration_threshold { get; set; } = 60f;
        internal virtual int songduration_min_time { get; set; } = 0;
        internal virtual int songduration_max_time { get; set; } = 120;

        internal virtual Vector3 position { get; set; } = CancelButtonViewController.Instance.position;
        internal virtual Quaternion rotation { get; set; } = CancelButtonViewController.Instance.rotation;
        internal virtual bool always_show_on_pause { get; set; } = true;
        internal virtual bool moveable_panel { get; set; } = false;
        internal virtual bool reset_panel { get; set; } = false;


        protected virtual void OnReload()
        {
            // Do stuff after config is read from disk.
        }

        /// <summary>
        /// Call this to force BSIPA to update the config file. This is also called by BSIPA if it detects the file was modified.
        /// </summary>
        protected virtual void Changed()
        {
            // Do stuff when the config is changed.
        }

        /// <summary>
        /// Call this to have BSIPA copy the values from <paramref name="other"/> into this config.
        /// </summary>
        protected virtual void CopyFrom(PluginConfig other)
        {
            // This instance's members populated from other
        }
    }
}
