using BeatSaberMarkupLanguage;
using System.Linq;
using BS_Utils;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;
using TMPro;
using System;
using Newtonsoft.Json.Linq;
using HMUI;
using UnityEngine.UI;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Attributes;

namespace PlayFirst
{
    public class PauseUI : NotifiableSingleton<PauseUI>
    {
        [UIComponent("checkbox")]
        public Toggle checkbox;
        [UIValue("checkbox-val")]
        public bool Check
        {
            get => Config.UserConfig.mod_enabled;
            set
            {
                Config.UserConfig.mod_enabled = value;
            }
        }
        [UIAction("checkbox-click")]
        void checkboxclick(bool value)
        {
            Check = value;
        }

        //<page-button interactable="~upInteractable" id="upButton" direction='Up' on-click="up-pressed" pref-width='10'></page-button>
		//<page-button interactable = "~downInteractable" id="downButton" direction='Down' on-click="down-pressed" pref-width='10'></page-button>

        /*
        [UIComponent("upButton")]
        public PageButton upButton;

        private bool upInteractable = true;
        [UIValue("upInteractable")]
        public bool UpInteractable
        {
            get => upInteractable;
            set
            {
                upButton.GetComponent<Button>().interactable = value;
                upInteractable = value;
                NotifyPropertyChanged();
            }
        }

        [UIAction("up-pressed")]
        public void UpPressed()
        {
            Logger.log.Debug("Up Pressed");
            //Plugin.disable_this_run = true;
            BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Run Cancelled");
            //Logger.log.Debug(Plugin.disable_this_run.ToString());
        }

        [UIComponent("downButton")]
        public PageButton downButton;

        private bool downInteractable = true;
        [UIValue("downInteractable")]
        public bool DownInteractable
        {
            get => downInteractable;
            set
            {
                downButton.GetComponent<Button>().interactable = value;
                downInteractable = value;
                NotifyPropertyChanged();
            }
        }

        [UIAction("down-pressed")]
        public void DownPressed()
        {
            Logger.log.Debug("Down Pressed");
            BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Run Cancelled");
        }
        */

        private void GetVotesForMap()
        {
            Logger.log.Debug("In ResultsView_didActivateEvent.GetVotesForMap");
            /*if (!(_lastSong is CustomPreviewBeatmapLevel) || _lastSong == null)
            {
                downButton.gameObject.SetActive(false);
                upButton.gameObject.SetActive(false);
                //voteText.text = "";
                //voteTitle.gameObject.SetActive(false);
                return;
            }*/
            //downButton.gameObject.SetActive(true);
            //upButton.gameObject.SetActive(true);
            //voteTitle.gameObject.SetActive(true);
            //voteText.text = "Loading...";
            //StartCoroutine(GetRatingForSong(_lastSong));
        }

        /*internal IEnumerator DelayedColorButtons()
        {
            Logger.log.Debug("In ResultsView_didActivateEvent.DelayedColorButtons");
            yield return new WaitUntil(() => upButton.gameObject.activeInHierarchy);
            ImageView upArrow = upButton.GetComponentInChildren<ImageView>();
            ImageView downArrow = downButton.GetComponentInChildren<ImageView>();
            if (upArrow != null && downArrow != null)
            //if (upArrow != null)
            {
                upArrow.color = new Color(0.341f, 0.839f, 0.341f);
                downArrow.color = new Color(0.984f, 0.282f, 0.305f);
            }
        }*/

        /*private void ResultsView_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            Logger.log.Debug("In PauseUI.ResultsView_didActivateEvent");
            StartCoroutine(DelayedColorButtons());
            GetVotesForMap();
        }*/

        internal void Setup()
        {
            Logger.log.Debug("In PauseUI.Setup");

            var pauseMenu = Resources.FindObjectsOfTypeAll<PauseMenuManager>().FirstOrDefault();
            if (pauseMenu != null)
            {
                Logger.log.Debug("Pause Menu Found!!!");
            }

            //BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "PlayFirst.pauseUI.bsml"), pauseMenu.gameObject, this);
            //checkbox.gameObject.SetActive(true);
            Logger.log.Debug("PauseUI Created");

            //resultsView.didActivateEvent += ResultsView_didActivateEvent;
        }
    }
}
