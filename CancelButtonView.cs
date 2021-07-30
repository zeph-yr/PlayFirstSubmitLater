using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
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
            //Plugin.disable_run = true;
            UpdateText();
        }

        public void UpdateText()
        {
            if (true)
            {
                cancelbutton_text.text = "Score Disabled";
            }
            else
            {
                cancelbutton_text.text = "Score Will Submit";
            }
        }
    }
}