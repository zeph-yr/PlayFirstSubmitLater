using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using UnityEngine;
using BS_Utils.Utilities;


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
            BSEvents.earlyMenuSceneLoadedFresh += BSEvents_earlyMenuSceneLoadedFresh;
        }

        public void Cleanup()
        {
            BSEvents.earlyMenuSceneLoadedFresh -= BSEvents_earlyMenuSceneLoadedFresh;
            BSEvents.menuSceneActive -= OnSongExited;
            BSEvents.gameSceneActive -= OnSongStarted;
            BSEvents.songPaused -= OnGamePause;
            BSEvents.songUnpaused -= OnGameResume;

            if (cancelbutton_screen != null)
            {
                GameObject.Destroy(cancelbutton_screen.gameObject);
                cancelbutton_screen = null;
            }
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
            Logger.log.Debug("ShowButton");

            if (cancelbutton_screen == null)
            {
                cancelbutton_screen = CreateFloatingScreen();
                cancelbutton_view = BeatSaberUI.CreateViewController<CancelButtonView>();
                cancelbutton_view.ParentCoordinator = this;
                cancelbutton_screen.SetRootViewController(cancelbutton_view, HMUI.ViewController.AnimationType.None);
                AttachEvents();
            }
            cancelbutton_screen.gameObject.SetActive(true);
            cancelbutton_view.UpdateText();
        }

        public FloatingScreen CreateFloatingScreen()
        {
            FloatingScreen screen = FloatingScreen.CreateFloatingScreen(
                new Vector2(20, 10), false,
                new Vector3(-1f, 0.6f, 2f),
                new Quaternion(0.1f, 0.1f, 0.1f, 0.1f));

            screen.HandleReleased -= OnRelease;
            screen.HandleReleased += OnRelease;

            GameObject.DontDestroyOnLoad(screen.gameObject);
            return screen;
        }

        private void AttachEvents()
        {
            BSEvents.menuSceneActive += OnSongExited;
            BSEvents.gameSceneActive += OnSongStarted;
            BSEvents.songPaused += OnGamePause;
            BSEvents.songUnpaused += OnGameResume;
        }

        private void OnRelease(object _, FloatingScreenHandleEventArgs posRot)
        {
            Vector3 newPos = posRot.Position;
            Quaternion newRot = posRot.Rotation;

            //PluginConfig.Instance.ScreenPos = newPos;
            //PluginConfig.Instance.ScreenRot = newRot;
        }

        private void SetVisibility(bool visibility)
        {
            if (cancelbutton_screen != null)
            {
                cancelbutton_screen.gameObject.SetActive(visibility);
                if (visibility)
                {
                    cancelbutton_view.UpdateText();
                }
            }
        }

        private void OnSongExited()
        {
            SetVisibility(true);
        }
        private void OnSongStarted()
        {
            SetVisibility(false);
        }
        private void OnGamePause()
        {
            SetVisibility(true);
        }
        private void OnGameResume()
        {
            SetVisibility(false);
        }

    }
}