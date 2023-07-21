using System.Linq;
using UnityEngine;

namespace PlayFirst
{
    internal sealed class SubmitLater : MonoBehaviour
    {
        private static float pausetime = 0.1f;
        private static bool paused_yet = false;

        private static AudioTimeSyncController audiocontroller;
        private static PauseController pausecontroller;

        private void Awake()
        {
            paused_yet = false;

            CancelButtonViewController.Instance.ShowButton(); // Putting this in Plugin.OnApplicationStart crashes it (No button comes up ever)

            audiocontroller = Resources.FindObjectsOfTypeAll<AudioTimeSyncController>().LastOrDefault();
            pausetime = audiocontroller.songEndTime - 0.25f;
            pausecontroller = Resources.FindObjectsOfTypeAll<PauseController>().LastOrDefault();
        }

        // Auto Pause at very end of map so you can decide
        private void LateUpdate()
        {
            if (PluginConfig.Instance.submitlater_enabled && !paused_yet)
            {
                if (audiocontroller.songTime >= pausetime)
                {
                    paused_yet = true;
                    pausecontroller.Pause();
                }
            }
        }
    }
}