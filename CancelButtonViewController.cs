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

        private void BSEvents_earlyMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {

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
                //BS_Utils.Utilities.BSEvents.lateMenuSceneLoadedFresh += BSEvents_lateMenuSceneLoadedFresh;
            }

            cancelbutton_screen.gameObject.SetActive(true);
            cancelbutton_view.UpdateText();
            cancelbutton_screen.gameObject.SetActive(false);
        }


        public FloatingScreen CreateFloatingScreen()
        {
            FloatingScreen screen = FloatingScreen.CreateFloatingScreen(
                new Vector2(20, 10), false,
                new Vector3(-1f, 0.6f, 2f),
                new Quaternion(25f, 330f, 6.5f, 180f));

            GameObject.DontDestroyOnLoad(screen.gameObject);
            return screen;
        }

        public void SetVisibility(bool visibility)
        {
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

        // Return to Menu from Pause Menu
        private void BSEvents_lateMenuSceneLoadedFresh(ScenesTransitionSetupDataSO obj)
        {
            SetVisibility(false);
        }
    }
}