using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using UnityEngine;

namespace PlayFirst
{
    class CancelButtonViewController
    {
        public static CancelButtonViewController _instance { get; private set; }
        
        public static CancelButtonViewController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CancelButtonViewController();
                return _instance;
            }
        }

        public FloatingScreen cancelbutton_screen;
        protected CancelButtonView cancelbutton_view;

        public void ShowButton()
        {
            if (cancelbutton_screen == null)
            {
                cancelbutton_screen = CreateFloatingScreen();
                cancelbutton_view = BeatSaberUI.CreateViewController<CancelButtonView>();
                cancelbutton_view.ParentCoordinator = this;
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

        public FloatingScreen CreateFloatingScreen()
        {
            Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);
            rotation *= Quaternion.Euler(Vector3.up * 90);

            FloatingScreen screen = FloatingScreen.CreateFloatingScreen(
                new Vector2(80, 10), false,
                new Vector3(-0.08f, 1.05f, 1.95f),
                rotation);

            // Notes: If no text, this size and position is perfect for the button alone
            // Vector2(34,10)
            // Vector3(-0.08f, 1.05f, 1.95f)

            // Size: length, height?
            // Handle: T/F
            // Position: left/right, up/down, in/out
            // Rotation: x, y, z, w (don't modify these values directly, very complex)

            GameObject.DontDestroyOnLoad(screen.gameObject);
            return screen;
        }

        private void SongPaused()
        {
            cancelbutton_view.UpdateText();
            // Only show view if it's not on ScoreSaber replay scene
            if (!Utils.ScoresaberUtil.IsInReplay())
            {
                cancelbutton_screen.gameObject.SetActive(true);            
            }
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
            //SubmitLater.paused_yet = true;
            cancelbutton_screen.gameObject.SetActive(false);
        }
    }
}