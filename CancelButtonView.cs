using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;

namespace PlayFirst
{
    [HotReload(@"CancelButtonView.bsml")]
    internal partial class CancelButtonView : BSMLAutomaticViewController
    {
        [UIComponent("cancelbutton")]
        private TextMeshProUGUI cancelbutton_text;

        [UIAction("disablescore")]
        private void ClickButtonAction()
        {
            if (!Plugin.disable_run && !Plugin.confirmed)
            {
                Plugin.disable_run = true;
                UpdateText();
            }
            else if (Plugin.disable_run && !Plugin.confirmed)
            {
                Plugin.confirmed = true;
                UpdateText();
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("SubmitLater");
                Logger.log.Debug("Score disabled by user");
            }
        }

        internal void UpdateText()
        {
            if (Plugin.disable_run && !Plugin.confirmed)
            {
                cancelbutton_text.text = "<#ff0000> Are you sure?";
            }
            else if (Plugin.disable_run && Plugin.confirmed)
            {
                cancelbutton_text.text = "<#ff0000> Score Disabled";
            }
            else
            {
                cancelbutton_text.text = "<#00ff00> Score Will Submit";
            }
        }
    }
}