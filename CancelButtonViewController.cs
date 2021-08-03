using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using UnityEngine;
using BS_Utils;

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
            //Logger.log.Debug("Create and show Button");

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
                

                // Not sure what this is for tbh...
                //BS_Utils.Utilities.BSEvents.earlyMenuSceneLoadedFresh += BSEvents_earlyMenuSceneLoadedFresh;

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
            // SetVisibility(true);
            cancelbutton_view.UpdateText();
            cancelbutton_screen.gameObject.SetActive(true);            
        }

        private void SongUnpaused()
        {
            //SetVisibility(false);
            cancelbutton_screen.gameObject.SetActive(false);
        }

        // For Restart from Pause Menu
        private void GameSceneLoaded() 
        {
            //SetVisibility(false);
            cancelbutton_screen.gameObject.SetActive(false);
        }

        // For Return to Menu from Pause Menu
        private void BSEvents_menuSceneLoaded()
        {
            //SetVisibility(false);
            //SubmitLater.paused_yet = true; // GameObject might already be destroyed by here
            cancelbutton_screen.gameObject.SetActive(false);
        }
    }
}

//protected CancelButtonViewController()
//{
//BS_Utils.Utilities.BSEvents.earlyMenuSceneLoadedFresh += BSEvents_earlyMenuSceneLoadedFresh;
//}

// Not to sure of the purpose of this
/*private void BSEvents_earlyMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
{
    //SetVisibility(false);
    cancelbutton_screen.gameObject.SetActive(false);

    if (cancelbutton_screen != null)
    {
        GameObject.Destroy(cancelbutton_screen.gameObject);
        cancelbutton_screen = null;
    }
}*/

/*private void SetVisibility(bool visibility)
{

    cancelbutton_view.UpdateText();
    cancelbutton_screen.gameObject.SetActive(visibility);

    // Not sure purpose of this
    if (cancelbutton_screen != null)
    {
        cancelbutton_screen.gameObject.SetActive(visibility);
        if (visibility)
        {
            cancelbutton_view.UpdateText();
        }
    }
}*/