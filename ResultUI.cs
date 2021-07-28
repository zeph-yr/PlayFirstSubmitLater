using BeatSaberMarkupLanguage;
using System.Linq;
using BS_Utils;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using UnityEngine;
using TMPro;
using System;
//using UnityEngine.Networking;
//using PlayFirstSubmitLater.Utilities;
using Newtonsoft.Json.Linq;
//using Steamworks;
using HMUI;
using UnityEngine.UI;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Attributes;

namespace PlayFirst
{
    public class ResultUI : NotifiableSingleton<ResultUI>
    {
        /*
        [Serializable]
        private struct Payload
        {
            public string steamID;
            public string ticket;
            public int direction;
        }
        */

        //internal IBeatmapLevel _lastSong;
        //private OpenVRHelper openVRHelper;
        //private bool _firstVote;
        //private Song _lastBeatSaverSong;
        //private string userAgent = $"BeatSaverVoting/{Assembly.GetExecutingAssembly().GetName().Version}";

        //[UIComponent("voteTitle")]
        //public TextMeshProUGUI voteTitle;
        //[UIComponent("voteText")]
        //public TextMeshProUGUI voteText;
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
            Plugin.disable_this_run = true;
            BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Menu");
            Logger.log.Debug(Plugin.disable_this_run.ToString());
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
        }

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
            downButton.gameObject.SetActive(true);
            upButton.gameObject.SetActive(true);
            //voteTitle.gameObject.SetActive(true);
            //voteText.text = "Loading...";
            //StartCoroutine(GetRatingForSong(_lastSong));
        }

        internal IEnumerator DelayedColorButtons()
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
        }

        private void ResultsView_didActivateEvent(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            Logger.log.Debug("In ResultUI.ResultsView_didActivateEvent");
            StartCoroutine(DelayedColorButtons());
            GetVotesForMap();
        }

        internal void Setup()
        {
            Logger.log.Debug("In ResultUI.Setup");

            var resultsView = Resources.FindObjectsOfTypeAll<ResultsViewController>().FirstOrDefault();
            if (!resultsView)
            {
                Logger.log.Debug("Not a Results Menu...");
                return;
            }

            BSMLParser.instance.Parse(BeatSaberMarkupLanguage.Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "PlayFirst.resultUI.bsml"), resultsView.gameObject, this);
            Logger.log.Debug("ResultUI Created");

            resultsView.didActivateEvent += ResultsView_didActivateEvent;
        }
    }
}
