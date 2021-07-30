using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using BS_Utils;

namespace PlayFirst
{
    [HotReload(@"CancelButtonView.bsml")]
    public partial class CancelButtonView : BSMLAutomaticViewController
    {
        internal CancelButtonViewController ParentCoordinator;

        [UIComponent("cancelbutton")]
        private TextMeshProUGUI cancelbutton_text;

        [UIAction("disablescore")]
        protected void ClickButtonAction()
        {
            BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Score Cancelled");
            Plugin.disable_run = true;
            UpdateText();
        }

        public void UpdateText()
        {
            if (Plugin.disable_run)
            {
                cancelbutton_text.text = "Score Disabled";
                //cancelbutton_text.color = UnityEngine.Color.red; // Doesn't work
            }
            else
            {
                cancelbutton_text.text = "Score Will Submit";
                //cancelbutton_text.color = UnityEngine.Color.green;
            }
        }
    }
}