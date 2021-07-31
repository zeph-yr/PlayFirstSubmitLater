using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using UnityEngine;
using BS_Utils;

namespace PlayFirst
{
    class CancelButtonViewController
    {
        private static CancelButtonViewController _instance;
        
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
        protected CancelButtonViewController()
        {
            BS_Utils.Utilities.BSEvents.earlyMenuSceneLoadedFresh += BSEvents_earlyMenuSceneLoadedFresh;
        }

        // Not to sure of the purpose of this
        private void BSEvents_earlyMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            SetVisibility(false);

            if (cancelbutton_screen != null)
            {
                GameObject.Destroy(cancelbutton_screen.gameObject);
                cancelbutton_screen = null;
            }
        }

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
            FloatingScreen screen = FloatingScreen.CreateFloatingScreen(
                new Vector2(50, 20), false,
                new Vector3(1f, 0f, 2f),
                new Quaternion(25f, 330f, 6.5f, 0f));

            GameObject.DontDestroyOnLoad(screen.gameObject);
            return screen;
        }

        public void SetVisibility(bool visibility)
        {
            // Not sure purpose of this
            /*if (cancelbutton_screen != null)
            {
                cancelbutton_screen.gameObject.SetActive(visibility);
                if (visibility)
                {
                    cancelbutton_view.UpdateText();
                }
            }*/
            cancelbutton_view.UpdateText();
            cancelbutton_screen.gameObject.SetActive(visibility);
        }


        private void SongPaused()
        {
            cancelbutton_view.UpdateText();
            SetVisibility(true);
        }

        private void SongUnpaused()
        {
            SetVisibility(false);
        }

        // For Restart from Pause Menu
        private void GameSceneLoaded() 
        {
            SetVisibility(false);
        }

        // For Return to Menu from Pause Menu
        private void BSEvents_menuSceneLoaded()
        {
            SetVisibility(false);
            //SubmitLater.paused_yet = true;
        }
    }
}