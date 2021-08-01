using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;

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
            if (!Plugin.disable_run && !Plugin.confirmed)
            {
                Plugin.disable_run = true;
                UpdateText();
            }

            else if (Plugin.disable_run && !Plugin.confirmed)
            {
                Plugin.confirmed = true;
                UpdateText();
                BS_Utils.Gameplay.ScoreSubmission.DisableSubmission("Choice");
            }
        }

        public void UpdateText()
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


        //private string button_state;
        /*void OnMouseOver()
        {
            if (Plugin.disable_run)
                cancelbutton_text.text = "<#ff0000> Are you sure?";
        }

        void OnMouseExit()
        {
            UpdateText();
        }*/
    }
}