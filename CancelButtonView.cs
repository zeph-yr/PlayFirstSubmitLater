using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;

namespace PlayFirst
{
    [HotReload(@"CancelButtonView.bsml")]
    public partial class CancelButtonView : BSMLAutomaticViewController
    {
        internal CancelButtonViewController ParentCoordinator;

        //[UIValue("background")]
        //private string bgcolor;

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
                cancelbutton_text.text = "<#ff0000> Score Disabled";
            }
            else
            {
                cancelbutton_text.text = "<#00ff00> Score Will Submit";
            }
        }

        //void OnMouseOver()
        //{
        //    bgcolor = "#ffff00ff";
        //}
    }
}