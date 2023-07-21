using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using UnityEngine;

namespace PlayFirst
{
    internal sealed class CancelButtonViewController
    {
        internal static CancelButtonViewController _instance { get; private set; }
        
        internal static CancelButtonViewController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CancelButtonViewController();
                return _instance;
            }
        }

        private FloatingScreen cancelbutton_screen;
        private CancelButtonView cancelbutton_view;

        internal readonly Vector3 position = new Vector3(-0.08f, 1.05f, 1.95f);
        internal readonly Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f) * Quaternion.Euler(Vector3.up * 90);

        internal void ShowButton()
        {
            if (cancelbutton_screen == null)
            {
                cancelbutton_screen = CreateFloatingScreen();
                cancelbutton_view = BeatSaberUI.CreateViewController<CancelButtonView>();
                cancelbutton_screen.SetRootViewController(cancelbutton_view, HMUI.ViewController.AnimationType.None);

                BS_Utils.Utilities.BSEvents.songPaused += SongPaused;
                BS_Utils.Utilities.BSEvents.songUnpaused += SongUnpaused;
                BS_Utils.Utilities.BSEvents.gameSceneLoaded += GameSceneLoaded;
                BS_Utils.Utilities.BSEvents.menuSceneLoaded += BSEvents_menuSceneLoaded;

                // Notes: These ones don't work to hide the Button
                //BS_Utils.Utilities.BSEvents.menuSceneLoadedFresh += BSEvents_menuSceneLoadedFresh;
                //BS_Utils.Utilities.BSEvents.menuSceneActive += BSEvents_menuSceneActive;
                //BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh += BSEvents_lateMenuSceneLoadedFresh;
            }

            cancelbutton_screen.gameObject.SetActive(true); // Must be true before update text
            cancelbutton_view.UpdateText();
            cancelbutton_screen.gameObject.SetActive(false);
        }

        private FloatingScreen CreateFloatingScreen()
        {
            FloatingScreen screen = FloatingScreen.CreateFloatingScreen(new Vector2(80, 10), PluginConfig.Instance.moveable_panel, position, rotation);

            // Notes: If no text, this size and position is perfect for the button alone
            // Vector2(34,10)
            // Vector3(-0.08f, 1.05f, 1.95f)

            // Size: length, height?
            // Handle: T/F
            // Position: left/right, up/down, in/out
            // Rotation: x, y, z, w (don't modify these values directly, very complex)

            if (PluginConfig.Instance.moveable_panel)
            {
                screen.HandleSide = FloatingScreen.Side.Right;
                screen.ShowHandle = true;
                screen.HighlightHandle = true;
                screen.handle.transform.localScale = Vector3.one * 2.0f;
                screen.handle.transform.localPosition = new Vector3(40.0f, 0.0f, 0.0f);
                screen.HandleReleased += Screen_HandleReleased;
            }

            screen.ScreenPosition = PluginConfig.Instance.position;
            screen.ScreenRotation = PluginConfig.Instance.rotation;

            if (PluginConfig.Instance.reset_panel)
            {
                screen.ScreenPosition = position;
                screen.ScreenRotation = rotation;

                PluginConfig.Instance.position = position;
                PluginConfig.Instance.rotation = rotation;
            }

            GameObject.DontDestroyOnLoad(screen.gameObject);
            return screen;
        }

        private void Screen_HandleReleased(object sender, FloatingScreenHandleEventArgs e)
        {
            PluginConfig.Instance.position = cancelbutton_screen.ScreenPosition;
            PluginConfig.Instance.rotation = cancelbutton_screen.ScreenRotation;
        }

        private void SongPaused()
        {
            cancelbutton_view.UpdateText();
            cancelbutton_screen.gameObject.SetActive(true);            
        }

        private void SongUnpaused()
        {
            cancelbutton_screen.gameObject.SetActive(false);
        }

        // For Restart from Pause Menu
        private void GameSceneLoaded() 
        {
            cancelbutton_screen.gameObject.SetActive(false);
        }

        // For Return to Menu from Pause Menu
        private void BSEvents_menuSceneLoaded()
        {
            cancelbutton_screen.gameObject.SetActive(false);
        }
    }
}